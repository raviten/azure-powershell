// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeShare;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Share
{
    [Cmdlet(VerbsCommon.New, Constants.Share, DefaultParameterSetName = NewParameterSet),
     OutputType(typeof(PSResourceModel))]
    public class DataBoxEdgebandWidthNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";
        private const string NfsParameterSet = "NfsParameterSet";
        private const string SmbParameterSet = "SmbParameterSet";

        [Parameter(Mandatory = true,
            HelpMessage = Constants.ResourceGroupNameHelpMessage,
            Position = 0)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = Constants.DeviceNameHelpMessage,
            Position = 1)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = Constants.NameHelpMessage,
            Position = 2)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = HelpMessageShare.StorageAccountCredentialHelpMessage,
            Position = 3)]
        [ValidateNotNullOrEmpty]
        public string StorageAccountCredentialName { get; set; }

        [Parameter(Mandatory = true,
            ParameterSetName = SmbParameterSet,
            HelpMessage = HelpMessageShare.AccessProtocolHelpMessage)]
        public string AccessProtocol { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = SmbParameterSet,
            HelpMessage = HelpMessageShare.SetUserAccessRightsHelpMessage)]
        [ValidateNotNullOrEmpty]
        public Hashtable SetUserAccessRights { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = NfsParameterSet,
            HelpMessage = HelpMessageShare.SetClientAccessRightsHelpMessage)]
        [ValidateNotNullOrEmpty]
        public Hashtable SetClientAccessRights { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageShare.DataFormatHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string DataFormat { get; set; }


        private Management.EdgeGateway.Models.Share initShareObject(
            string accessProtocol)
        {
            return new Management.EdgeGateway.Models.Share("Online",
                "Enabled",
                accessProtocol,
                dataPolicy: "Cloud");
        }

        private string GetUserId(string username)
        {
            var user = UsersOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.Users,
                this.DeviceName,
                username,
                this.ResourceGroupName
            );
            return user.Id;
        }

        public override void ExecuteCmdlet()
        {
            var results = new List<PSResourceModel>();
            var sac = StorageAccountCredentialsOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.StorageAccountCredentials,
                this.DeviceName,
                this.StorageAccountCredentialName,
                this.ResourceGroupName);
            var share = this.initShareObject(this.AccessProtocol);

            if (this.IsParameterBound(c => c.SetClientAccessRights))
            {

                share.ClientAccessRights = new List<ClientAccessRight>();
                foreach (KeyValuePair<string, string> clientAccessRight in this.SetClientAccessRights)
                {

                    share.ClientAccessRights.Add(
                        new ClientAccessRight(
                            clientAccessRight.Key,
                            clientAccessRight.Value
                        )
                    );
                }

            }

            if (this.IsParameterBound(c => c.SetUserAccessRights))
            {
                share.UserAccessRights = new List<UserAccessRight>();
                foreach (KeyValuePair<string, string> userAccessRight in this.SetUserAccessRights)
                {
                    share.UserAccessRights.Add(
                        new UserAccessRight(
                            GetUserId(username: userAccessRight.Key),
                            userAccessRight.Value
                        ));
                }
            }

            share.AzureContainerInfo = new AzureContainerInfo(sac.Id, this.Name, this.DataFormat);
            share = SharesOperationsExtensions.CreateOrUpdate(
                DataBoxEdgeManagementClient.Shares,
                this.DeviceName,
                this.Name,
                share,
                this.ResourceGroupName);
            results.Add(new PSResourceModel(share));
            WriteObject(results, true);
        }
    }
}
