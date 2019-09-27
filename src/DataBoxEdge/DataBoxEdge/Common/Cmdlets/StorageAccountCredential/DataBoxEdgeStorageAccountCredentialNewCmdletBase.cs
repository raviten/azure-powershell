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
using System.Security;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using Microsoft.WindowsAzure.Commands.Common;
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.StorageAccountCredential;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.StorageAccountCredential
{
    [Cmdlet(VerbsCommon.New, Constants.Sac, DefaultParameterSetName = NewParameterSet),
     OutputType(typeof(PSDataBoxEdgeStorageAccountCredential))]
    public class DataBoxEdgeStorageAccountCredentialNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";
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
        [ResourceNameCompleter("Microsoft.DataBoxEdge/dataBoxEdgeDevices", nameof(ResourceGroupName))]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = HelpMessageStorageAccountCredential.StorageAccountNameHelpMessage,
            Position = 2)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, 
            HelpMessage = HelpMessageStorageAccountCredential.StorageAccountTypeHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string StorageAccountType { get; set; }

        [Parameter(Mandatory = true, 
            HelpMessage = HelpMessageStorageAccountCredential.StorageAccountAccessKeyHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string StorageAccountAccessKey { get; set; }

        [Parameter(Mandatory = true, 
            HelpMessage = Constants.EncryptionKeyHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SecureString EncryptionKey { get; set; }

        [Parameter(Mandatory = false, HelpMessage = Constants.AsJobHelpMessage)]
        public SwitchParameter AsJob { get; set; }

        private static ResourceModel InitStorageAccountCredentialObject(
            string name,
            string storageAccountName,
            string accountType,
            string sslStatus,
            AsymmetricEncryptedSecret secret)
        {
            var storageAccountCredential = new ResourceModel(
                name,
                sslStatus,
                accountType,
                userName: storageAccountName,
                accountKey: secret);
            return storageAccountCredential;
        }

        public override void ExecuteCmdlet()
        {
            var encryptedSecret =
                DataBoxEdgeManagementClient.Devices.GetAsymmetricEncryptedSecret(
                    this.DeviceName,
                    this.ResourceGroupName,
                    this.StorageAccountAccessKey,
                    this.EncryptionKey.ConvertToString()
                );
            var results = new List<PSDataBoxEdgeStorageAccountCredential>();
            var user = new PSDataBoxEdgeStorageAccountCredential(
                StorageAccountCredentialsOperationsExtensions.CreateOrUpdate(
                    this.DataBoxEdgeManagementClient.StorageAccountCredentials,
                    this.DeviceName,
                    this.Name,
                    InitStorageAccountCredentialObject(
                        name: this.Name,
                        storageAccountName: this.Name,
                        accountType: this.StorageAccountType,
                        sslStatus: HelpMessageStorageAccountCredential.SslStatus,
                        secret: encryptedSecret
                    ),
                    this.ResourceGroupName
                ));
            results.Add(user);

            WriteObject(results, true);
        }
    }
}