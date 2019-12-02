using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.DataBoxEdge.Models;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeStorageAccount
    {
        [Ps1Xml(Label = "Name", Target = ViewControl.Table,
            ScriptBlock = "$_.storageAccount.Name", Position = 0)]
        public StorageAccount EdgeStorageAccount;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table)]
        public string ResourceGroupName;

        [Ps1Xml(Label = "DeviceName", Target = ViewControl.Table)]
        public string DeviceName;

        [Ps1Xml(Label = "CloudStorageAccountName", Target = ViewControl.Table)]
        public string StorageAccountName;

        public string Id;
        public string Name;
        
        public PSDataBoxEdgeStorageAccount()
        {
            EdgeStorageAccount = new StorageAccount();
        }

        private static string GetStorageAccountCredentialAccountName(string resourceId)
        {
            var splits = resourceId.Split(new[] { '/' });
            for (var i = 0; i < splits.Length; i++)
            {
                if (splits[i].Equals("storageAccountCredentials", StringComparison.CurrentCultureIgnoreCase))
                {
                    return splits[i + 1];
                }
            }

            throw new Exception("InvalidStorageAccountCredential");
        }

        public PSDataBoxEdgeStorageAccount(StorageAccount storageAccount)
        {
            this.EdgeStorageAccount = storageAccount ?? throw new ArgumentNullException("storageAccount");
            this.Id = storageAccount.Id;
            var resourceIdentifier = new DataBoxEdgeResourceIdentifier(EdgeStorageAccount.Id);
            this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
            this.DeviceName = resourceIdentifier.DeviceName;
            this.Name = resourceIdentifier.ResourceName;
            if (storageAccount.DataPolicy == "Cloud")
            {
                this.StorageAccountName = GetStorageAccountCredentialAccountName(storageAccount.StorageAccountCredentialId);
            }
        }
    }
}