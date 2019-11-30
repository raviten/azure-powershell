﻿// ----------------------------------------------------------------------------------
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

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.DataBoxEdge;
using Microsoft.Azure.Management.DataBoxEdge.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using Microsoft.Rest.Azure;
using Microsoft.WindowsAzure.Commands.Common;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Security;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.StorageAccountCredentials
{
    [Cmdlet(VerbsCommon.New, Constants.Sac, DefaultParameterSetName = NewParameterSet,
         SupportsShouldProcess = true
     ),
     OutputType(typeof(PSDataBoxEdgeStorageAccountCredential))]
    public class DataBoxEdgeStorageAccountCredentialNewCmdlet : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";

        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = Constants.ResourceGroupNameHelpMessage,
            Position = 0)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
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
        [PSArgumentCompleter("GeneralPurposeStorage", "BlobStorage")]
        public string StorageAccountType { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = HelpMessageStorageAccountCredential.StorageAccountAccessKeyHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SecureString StorageAccountAccessKey { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = Constants.EncryptionKeyHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SecureString EncryptionKey { get; set; }

        [Parameter(Mandatory = false, HelpMessage = Constants.AsJobHelpMessage)]
        public SwitchParameter AsJob { get; set; }

        private string GetKeyForEncryption()
        {
            var encryptionKey = this.EncryptionKey.ConvertToString();
            return encryptionKey;
        }

        private StorageAccountCredential GetResource()
        {
            return this.DataBoxEdgeManagementClient.StorageAccountCredentials.Get(
                this.DeviceName,
                this.Name,
                this.ResourceGroupName);
        }

        private string GetResourceAlreadyExistMessage()
        {
            return string.Format("'{0}'{1}{2}'.",
                HelpMessageStorageAccountCredential.ObjectName, Constants.ResourceAlreadyExists, this.Name);
        }

        private bool DoesResourceExists()
        {
            try
            {
                var resource = GetResource();
                if (resource == null) return false;
                var msg = GetResourceAlreadyExistMessage();
                throw new Exception(msg);
            }
            catch (CloudException e)
            {
                if (e.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                throw;
            }
        }

        private PSDataBoxEdgeStorageAccountCredential CreateResource()
        {
            var encryptedSecret =
                DataBoxEdgeManagementClient.Devices.GetAsymmetricEncryptedSecret(
                    this.DeviceName,
                    this.ResourceGroupName,
                    this.StorageAccountAccessKey.ConvertToString(),
                    GetKeyForEncryption()
                );

            return new PSDataBoxEdgeStorageAccountCredential(
                this.DataBoxEdgeManagementClient.StorageAccountCredentials.CreateOrUpdate(
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
        }

        private static StorageAccountCredential InitStorageAccountCredentialObject(
            string name,
            string storageAccountName,
            string accountType,
            string sslStatus,
            AsymmetricEncryptedSecret secret)
        {
            var storageAccountCredential = new StorageAccountCredential(
                name,
                sslStatus,
                accountType,
                userName: storageAccountName,
                accountKey: secret);
            return storageAccountCredential;
        }

        public override void ExecuteCmdlet()
        {
            if (this.ShouldProcess(this.Name,
                string.Format("Creating '{0}' in device '{1}' with name '{2}'.",
                    HelpMessageStorageAccountCredential.ObjectName, this.DeviceName, this.Name)))
            {
                DoesResourceExists();
                var results = new List<PSDataBoxEdgeStorageAccountCredential>()
                {
                    CreateResource()
                };

                WriteObject(results, true);
            }
        }
    }
}