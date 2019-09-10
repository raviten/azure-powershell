using System;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using DataBoxEdgeDeviceExtendedInfo = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDeviceExtendedInfo;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeDeviceExtendedInfo
    {
        [Ps1Xml(Label = "EncryptionKey", Target = ViewControl.List,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.EncryptionKey")]
        [Ps1Xml(Label = "EncryptionKeyThumbprint", Target = ViewControl.List,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.EncryptionKeyThumbprint")]
        [Ps1Xml(Label = "ResourceKey", Target = ViewControl.List,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.ResourceKey")]
        public DataBoxEdgeDeviceExtendedInfo DataBoxEdgeDeviceExtendedInfo;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.List, Position = 2, GroupByThis = false)]
        public string ResourceGroupName { get; set; }

        [Ps1Xml(Label = "DeviceName", Target = ViewControl.List, Position = 1)]
        public string DeviceName;

        [Ps1Xml(Label = "Name", Target = ViewControl.List, Position = 0)]
        public string Name;

        public string Id;

        public PSDataBoxEdgeDeviceExtendedInfo()
        {
            DataBoxEdgeDeviceExtendedInfo = new DataBoxEdgeDeviceExtendedInfo();
        }

        public PSDataBoxEdgeDeviceExtendedInfo(DataBoxEdgeDeviceExtendedInfo dataBoxEdgeDeviceExtendedInfo)
        {
            this.DataBoxEdgeDeviceExtendedInfo = dataBoxEdgeDeviceExtendedInfo ??
                                                 throw new ArgumentNullException("dataBoxEdgeDeviceExtendedInfo");
            this.Id = DataBoxEdgeDeviceExtendedInfo.Id;
            var resourceIdntifier = new DataBoxEdgeResourceIdentifier(dataBoxEdgeDeviceExtendedInfo.Id);
            this.ResourceGroupName = ResourceIdHandler.GetResourceGroupName(DataBoxEdgeDeviceExtendedInfo.Id);
            this.DeviceName = DataBoxEdgeDeviceExtendedInfo.EncryptionKey;
            this.Name = DataBoxEdgeDeviceExtendedInfo.Name;
        }
    }
}