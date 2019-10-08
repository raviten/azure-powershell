﻿using System;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using DataBoxEdgeDeviceExtendedInfo = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDeviceExtendedInfo;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeDeviceExtendedInfo
    {
        [Ps1Xml(Label = "EncryptionKeyThumbprint", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.EncryptionKeyThumbprint")]
        [Ps1Xml(Label = "ResourceKey", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.ResourceKey")]
        public DataBoxEdgeDeviceExtendedInfo DataBoxEdgeDeviceExtendedInfo;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table, Position = 2, GroupByThis = false)]
        public string ResourceGroupName { get; set; }

        [Ps1Xml(Label = "DeviceName", Target = ViewControl.Table, Position = 1)]
        public string DeviceName;

        public string Name;

        public string Id;
        [Ps1Xml(Label = "EncryptionKey", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeDeviceExtendedInfo.EncryptionKey")]
        public string EncryptionKey;

        public PSDataBoxEdgeDeviceExtendedInfo()
        {
            DataBoxEdgeDeviceExtendedInfo = new DataBoxEdgeDeviceExtendedInfo();
        }

        public PSDataBoxEdgeDeviceExtendedInfo(DataBoxEdgeDeviceExtendedInfo dataBoxEdgeDeviceExtendedInfo)
        {
            this.DataBoxEdgeDeviceExtendedInfo = dataBoxEdgeDeviceExtendedInfo ??
                                                 throw new ArgumentNullException("dataBoxEdgeDeviceExtendedInfo");
            this.Id = DataBoxEdgeDeviceExtendedInfo.Id;
            var resourceIdentifier = new DataBoxEdgeResourceIdentifier(dataBoxEdgeDeviceExtendedInfo.Id);
            this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
            this.DeviceName = resourceIdentifier.DeviceName;
            this.Name = resourceIdentifier.DeviceName;
            this.EncryptionKey = DataBoxEdgeDeviceExtendedInfo.EncryptionKey;
        }
    }
}