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
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;

namespace Microsoft.Azure.Commands.DataBoxEdge.Common
{
    [Cmdlet(VerbsCommon.Set, Constants.Share, DefaultParameterSetName = SetParameterSet),
     OutputType(typeof(PSDataBoxEdgeShare))]
    public class DataBoxEdgeShareSetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string SetParameterSet = "SetParameterSet";
        private const string NFSParameterSet = "NFSParameterSet";
        private const string SMBParameterSet = "SMBParameterSet";

        [Parameter(Mandatory = true, 
            HelpMessage = "By Default it creates SMB share in the provided ResourceGroupName", 
            ParameterSetName = SetParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Creates SMB share by passing -SMB for this ResourceGroup", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Creates NFS share by passing -NFS for this ResourceGroup", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share types", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share types", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share types", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share types", ParameterSetName = SMBParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share types", ParameterSetName = SetParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string SACName { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share types", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share types", ParameterSetName = SMBParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share types", ParameterSetName = SetParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Set for SMB Share types", ParameterSetName = SMBParameterSet)]
        public SwitchParameter SMB { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "Username for SMB Share types", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Username { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Collect notice log type.", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string UserAccessRight { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Collect notice log type.", ParameterSetName = NFSParameterSet)]
        public SwitchParameter NFS { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Collect notice log type.", ParameterSetName = NFSParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ClientId { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Collect notice log type.", ParameterSetName = NFSParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ClientAccessRight { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "Collect notice log type.", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Collect notice log type.", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string DataFormat { get; set; }


        private Share initShareObject(
            string shareType)
        {
            return new Share("Online",
                "Enabled",
                shareType,
                dataPolicy: "Cloud");
        }

        public override void ExecuteCmdlet()
        {
            var results = new List<PSDataBoxEdgeShare>();
            var sac = StorageAccountCredentialsOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.StorageAccountCredentials,
                this.DeviceName,
                this.SACName,
                this.ResourceGroupName);

            var share = this.initShareObject(NFS.IsPresent ? "NFS" : "SMB");
            share.AzureContainerInfo = new AzureContainerInfo(sac.Id, this.Name, this.DataFormat);
            if (NFS.IsPresent)
            {
                share.ClientAccessRights = new List<ClientAccessRight>();
                share.ClientAccessRights.Add(
                    new ClientAccessRight(
                        this.ClientId,
                        this.ClientAccessRight
                    )
                );
            }
            else if (SMB.IsPresent)
            {
                var user = UsersOperationsExtensions.Get(
                    this.DataBoxEdgeManagementClient.Users,
                    this.DeviceName,
                    this.Username,
                    this.ResourceGroupName
                );
                share.UserAccessRights = new List<UserAccessRight>();
                share.UserAccessRights.Add(
                    new UserAccessRight(
                        user.Id,
                        this.UserAccessRight
                    )
                );
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