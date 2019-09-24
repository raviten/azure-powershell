using Microsoft.Azure.Commands.DataBoxEdge.Common;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using StorageAccountCredential = Microsoft.Azure.Management.EdgeGateway.Models.StorageAccountCredential;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeStorageAccountCredential
    {
        [Ps1Xml(Label = "StorageAccountCredential.Name", Target = ViewControl.Table,
            ScriptBlock = "$_.storageAccountCredential.Name")]
        [Ps1Xml(Label = "StorageAccountCredential.StorageAccount", Target = ViewControl.Table,
            ScriptBlock = "$_.storageAccountCredential.UserName")]
        [Ps1Xml(Label = "StorageAccountCredential.SslStatus", Target = ViewControl.Table,
            ScriptBlock = "$_.storageAccountCredential.SslStatus")]
        
        public StorageAccountCredential StorageAccountCredential;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table)]
        public string ResourceGroupName;

        [Ps1Xml(Label = "DeviceName", Target = ViewControl.Table)]
        public string DeviceName;

        public string Id;
        public string Name;

        public PSDataBoxEdgeStorageAccountCredential()
        {
            StorageAccountCredential = new StorageAccountCredential();
        }

        public PSDataBoxEdgeStorageAccountCredential(StorageAccountCredential storageAccountCredential)
        {
            this.StorageAccountCredential = storageAccountCredential;
            this.Id = storageAccountCredential.Id;
            var resourceIdentifier = new DataBoxEdgeResourceIdentifier(storageAccountCredential.Id);
            this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
            this.DeviceName = resourceIdentifier.DeviceName;
            this.Name = resourceIdentifier.ResourceName;


        }
    }
}