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
    [Cmdlet(VerbsCommon.New, Constants.Share, DefaultParameterSetName = NewParameterSet),
     OutputType(typeof(PSDataBoxEdgeShare))]
    public class DataBoxEdgeShareNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";
        private const string NFSParameterSet = "NFSParameterSet";
        private const string SMBParameterSet = "SMBParameterSet";

        [Parameter(Mandatory = true, 
            HelpMessage = "Share will be created under this ResourceGroupName", 
            ParameterSetName = NewParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Share will be created under this ResourceGroupName", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Share will be created under this ResourceGroupName", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Share will be created on the device with Resource name as DeviceName", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Share will be created on the device with Resource name as DeviceName", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "Provide existing StorageAccountCredential's Resource Name", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Provide existing StorageAccountCredential's Resource Name", ParameterSetName = SMBParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Provide existing StorageAccountCredential's Resource Name", ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string SACName { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "Set for NFS Share access types", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set for SMB Share access types", ParameterSetName = SMBParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Defaults to SMB Share Access type", ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Set Share access type as SMB", ParameterSetName = SMBParameterSet)]
        public SwitchParameter SMB { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "provide an existing username for SMB Share types", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Username { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Provide user access right for the Username", ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string UserAccessRight { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Set Share access type as NFS", ParameterSetName = NFSParameterSet)]
        public SwitchParameter NFS { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Provide ClientId for the NFS", ParameterSetName = NFSParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ClientId { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Provide Read/Write Access for clientId", ParameterSetName = NFSParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ClientAccessRight { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Set Data Format ex: PageBlob, BlobBlob", ParameterSetName = NewParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set Data Format ex: PageBlob, BlobBlob", ParameterSetName = NFSParameterSet)]
        [Parameter(Mandatory = true, HelpMessage = "Set Data Format ex: PageBlob, BlobBlob", ParameterSetName = SMBParameterSet)]
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