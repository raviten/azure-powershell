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

using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Share
{
    [Cmdlet(VerbsCommon.Set, Constants.Share, DefaultParameterSetName = SetParameterSet),
     OutputType(typeof(PSDataBoxEdgeShare))]
    public class DataBoxEdgeShareSetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string SetParameterSet = "SetParameterSet";
        private const string NFSParameterSet = "NFSParameterSet";
        private const string SMBParameterSet = "SMBParameterSet";

        [Parameter(Mandatory = true,
            HelpMessage = "Share will be created under this ResourceGroupName",
            ParameterSetName = SetParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Share will be created under this ResourceGroupName",
            ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Share will be created under this ResourceGroupName",
            ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetParameterSet)]
        [Parameter(Mandatory = true,
            HelpMessage = "Share will be created on the device with Resource name as DeviceName",
            ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true,
            HelpMessage = "Share will be created on the device with Resource name as DeviceName",
            ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Get Share with Resource name as Name")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "provide an existing Username for SMB Share types",
            ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Username { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Provide user access right for the Username",
            ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        public string UserAccessRight { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Provide ClientId for the NFS", ParameterSetName = NFSParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ClientId { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Provide Read/Write Access for clientId",
            ParameterSetName = NFSParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ClientAccessRight { get; set; }

        public override void ExecuteCmdlet()
        {
            var results = new List<PSDataBoxEdgeShare>();
            var share = SharesOperationsExtensions.Get(
                DataBoxEdgeManagementClient.Shares,
                this.DeviceName,
                this.Name,
                this.ResourceGroupName);


            if (ClientId != null)
            {
                share.ClientAccessRights = new List<ClientAccessRight>();
                share.ClientAccessRights.Add(
                    new ClientAccessRight(
                        this.ClientId,
                        this.ClientAccessRight
                    )
                );
            }
            else
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


            results.Add(new PSDataBoxEdgeShare(share));
            WriteObject(results, true);
        }
    }
}