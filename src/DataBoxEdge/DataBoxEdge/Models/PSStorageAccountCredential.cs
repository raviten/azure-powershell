using Microsoft.WindowsAzure.Commands.Common.Attributes;
using StorageAccountCredential = Microsoft.Azure.Management.EdgeGateway.Models.StorageAccountCredential;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSStorageAccountCredential
    {
        [Ps1Xml(Label = "StorageAccountCredential.Name", Target = ViewControl.Table,
            ScriptBlock = "$_.storageAccountCredential.Name")]
        public StorageAccountCredential StorageAccountCredential;

        [Ps1Xml(Label = "ResourceGroup", Target = ViewControl.Table)]
        public string ResourceGroup;

        public string Id;
        public string Name;

        public PSStorageAccountCredential()
        {
            StorageAccountCredential = new StorageAccountCredential();
        }

        public PSStorageAccountCredential(StorageAccountCredential storageAccountCredential)
        {
            this.StorageAccountCredential = storageAccountCredential;
            this.Id = storageAccountCredential.Id;
            this.Name = storageAccountCredential.Name;
        }
    }
}