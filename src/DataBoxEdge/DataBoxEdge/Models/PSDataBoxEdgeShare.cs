using System;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Share = Microsoft.Azure.Management.EdgeGateway.Models.Share;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeShare
    {
        [Ps1Xml(Label = "Share.Name", Target = ViewControl.Table,
            ScriptBlock = "$_.share.Name", Position = 0)]
        [Ps1Xml(Label = "Share.Type", Target = ViewControl.Table,
            ScriptBlock = "$_.share.AccessProtocol")]
        [Ps1Xml(Label = "Share.DataPolicy", Target = ViewControl.Table,
            ScriptBlock = "$_.share.DataPolicy")]
        [Ps1Xml(Label = "Share.DataFormat", Target = ViewControl.Table,
            ScriptBlock = "$_.share.AzureContainerInfo.DataFormat")]
        
        public Share Share;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table)]
        public string ResourceGroupName;

        [Ps1Xml(Label = "StorageAccount.Name", Target = ViewControl.Table)]
        public string StorageAccountName;

        public string Id;
        public string Name;

        public PSDataBoxEdgeShare()
        {
            Share = new Share();
        }

        private static string GetStorageAccountCredentialAccountName(string resourceId)
        {
            var splits = resourceId.Split(new[] {'/'});
            for (int i = 0; i < splits.Length; i++)
            {
                if (splits[i].Equals("storageAccountCredentials", StringComparison.CurrentCultureIgnoreCase))
                {
                    return splits[i + 1];
                }
            }

            throw new Exception("InvalidStorageAccountCredential");
        }

        public PSDataBoxEdgeShare(Share share)
        {
            this.Share = share ?? throw new ArgumentNullException("share");
            this.Id = share.Id;
            this.ResourceGroupName = ResourceIdHandler.GetResourceGroupName(share.Id);
            this.StorageAccountName = GetStorageAccountCredentialAccountName(share.AzureContainerInfo
                .StorageAccountCredentialId);
            this.Name = share.Name;
        }
    }
}