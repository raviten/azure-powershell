using System;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using DataBoxEdgeDeviceExtendedInfo = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDeviceExtendedInfo;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeDeviceExtendedInfo
    {
        [Ps1Xml(Label = "DataBoxEdgeDeviceExtendedInfo.EncryptionKey", Target = ViewControl.List,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.EncryptionKey", Position = 2)]
        [Ps1Xml(Label = "DataBoxEdgeDeviceExtendedInfo.EncryptionKeyThumbprint", Target = ViewControl.List,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.EncryptionKeyThumbprint", Position = 3)]
        [Ps1Xml(Label = "DataBoxEdgeDeviceExtendedInfo.ResourceKey", Target = ViewControl.List,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.ResourceKey", Position = 4)]
        public DataBoxEdgeDeviceExtendedInfo DataBoxEdgeDeviceExtendedInfo;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.List, Position = 1, GroupByThis = false)]
        public string ResourceGroupName { get; set; }

        [Ps1Xml(Label = "DataBoxEdgeDevice.Name", Target = ViewControl.List, Position = 0)]
        public string Name;

        public string Id;

        public PSDataBoxEdgeDeviceExtendedInfo()
        {
            DataBoxEdgeDeviceExtendedInfo = new DataBoxEdgeDeviceExtendedInfo();
        }

        public PSDataBoxEdgeDeviceExtendedInfo(DataBoxEdgeDeviceExtendedInfo dataBoxEdgeDeviceExtendedInfo)
        {
            this.DataBoxEdgeDeviceExtendedInfo = dataBoxEdgeDeviceExtendedInfo ?? throw new ArgumentNullException("dataBoxEdgeDeviceExtendedInfo");
            this.ResourceGroupName = ResourceIdHandler.GetResourceGroupName(DataBoxEdgeDeviceExtendedInfo.Id);
            this.Name = DataBoxEdgeDeviceExtendedInfo.Name;
            this.Id = DataBoxEdgeDeviceExtendedInfo.Id;
            
        }
    }
}