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
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.Share;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeShare;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Share
{
    [Cmdlet(VerbsCommon.Set, Constants.Share, DefaultParameterSetName = SmbParameterSet),
     OutputType(typeof(PSDataBoxEdgeShare))]
    public class DataBoxEdgebandWidthSetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string SmbParameterSet = "SmbParameterSet";
        private const string NfsParameterSet = "NfsParameterSet";

        private const string UpdateByResourceIdSmbParameterSet = "UpdateByResourceIdSmbParameterSet";
        private const string UpdateByResourceIdNfsParameterSet = "UpdateByResourceIdNfsParameterSet";

        private const string UpdateByInputObjectSmbParameterSet = "UpdateByInputObjectSmbParameterSet";
        private const string UpdateByInputObjectNfsParameterSet = "UpdateByInputObjectNfsParameterSet";

        [Parameter(
            Mandatory = true,
            ParameterSetName = UpdateByResourceIdSmbParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = Constants.ResourceIdHelpMessage)]
        [Parameter(
            Mandatory = true,
            ParameterSetName = UpdateByResourceIdNfsParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = Constants.ResourceIdHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ParameterSetName = UpdateByInputObjectSmbParameterSet,
            HelpMessage = Constants.InputObjectHelpMessage
        )]
        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ParameterSetName = UpdateByInputObjectNfsParameterSet,
            HelpMessage = Constants.InputObjectHelpMessage
        )]
        [ValidateNotNull]
        public PSResourceModel InputObject { get; set; }


        [Parameter(Mandatory = true,
            ParameterSetName = NfsParameterSet,
            HelpMessage = Constants.ResourceGroupNameHelpMessage,
            Position = 0)]
        [Parameter(Mandatory = true,
            ParameterSetName = SmbParameterSet,
            HelpMessage = Constants.ResourceGroupNameHelpMessage,
            Position = 0)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true,
            ParameterSetName = NfsParameterSet,
            HelpMessage = Constants.DeviceNameHelpMessage,
            Position = 1)]
        [Parameter(Mandatory = true,
            ParameterSetName = SmbParameterSet,
            HelpMessage = Constants.DeviceNameHelpMessage,
            Position = 1)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true,
            ParameterSetName = NfsParameterSet,
            HelpMessage = Constants.NameHelpMessage,
            Position = 2)]
        [Parameter(Mandatory = true,
            ParameterSetName = SmbParameterSet,
            HelpMessage = Constants.NameHelpMessage,
            Position = 2)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true,
            ParameterSetName = SmbParameterSet,
            HelpMessage = HelpMessageShare.SetUserAccessRightsHelpMessage)]
        [Parameter(Mandatory = true,
            ParameterSetName = UpdateByResourceIdSmbParameterSet,
            HelpMessage = HelpMessageShare.SetUserAccessRightsHelpMessage)]
        [Parameter(Mandatory = true,
            ParameterSetName = UpdateByInputObjectSmbParameterSet,
            HelpMessage = HelpMessageShare.SetUserAccessRightsHelpMessage)]
        [ValidateNotNullOrEmpty]
        public Hashtable SetUserAccessRights { get; set; }

        [Parameter(Mandatory = true,
            ParameterSetName = NfsParameterSet,
            HelpMessage = HelpMessageShare.SetClientAccessRightsHelpMessage)]
        [Parameter(Mandatory = true,
            ParameterSetName = UpdateByResourceIdNfsParameterSet,
            HelpMessage = HelpMessageShare.SetClientAccessRightsHelpMessage)]
        [Parameter(Mandatory = true,
            ParameterSetName = UpdateByInputObjectNfsParameterSet,
            HelpMessage = HelpMessageShare.SetClientAccessRightsHelpMessage)]
        [ValidateNotNullOrEmpty]
        public Hashtable SetClientAccessRights { get; set; }

        [Parameter(Mandatory = false, HelpMessage = Constants.AsJobHelpMessage)]
        public SwitchParameter AsJob { get; set; }

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

        private ResourceModel GetResourceModel()
        {
            return SharesOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.Shares,
                this.DeviceName,
                this.Name,
                this.ResourceGroupName);
        }

        public override void ExecuteCmdlet()
        {
            var results = new List<PSDataBoxEdgeShare>();
            var share = this.GetResourceModel();

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

            share = SharesOperationsExtensions.CreateOrUpdate(
                DataBoxEdgeManagementClient.Shares,
                this.DeviceName,
                this.Name,
                share,
                this.ResourceGroupName);
            results.Add(new PSDataBoxEdgeShare(share));
            WriteObject(results, true);
        }
    }
}
