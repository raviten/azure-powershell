﻿using System;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using DataBoxEdgeDevice = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDevice;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeDevice
    {
        [Ps1Xml(Label = "DataBoxEdgeDeviceStatus", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeDevice.DataBoxEdgeDeviceStatus", Position = 1)]
        [Ps1Xml(Label = "DeviceType", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeDevice.DeviceType", Position = 2)]
        [Ps1Xml(Label = "Location", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeDevice.Location", Position = 4)]
        public DataBoxEdgeDevice DataBoxEdgeDevice;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table, Position = 3, GroupByThis = false)]
        public string ResourceGroupName { get; set; }

        [Ps1Xml(Label = "DataBoxEdgeDevice.Name", Target = ViewControl.Table, Position = 0)]
        public string Name;

        public string Id;

        public PSDataBoxEdgeDevice()
        {
            DataBoxEdgeDevice = new DataBoxEdgeDevice();
        }

        public PSDataBoxEdgeDevice(DataBoxEdgeDevice dataBoxEdgeDevice)
        {
            if (dataBoxEdgeDevice == null)
            {
                throw new ArgumentNullException("dataBoxEdgeDevice");
            }

            this.DataBoxEdgeDevice = dataBoxEdgeDevice;
            this.Id = dataBoxEdgeDevice.Id;
            var dataBoxEdgeResourceIdentifier = new DataBoxEdgeResourceIdentifier(dataBoxEdgeDevice.Id);
            this.ResourceGroupName = dataBoxEdgeResourceIdentifier.ResourceGroupName;
            this.Name = dataBoxEdgeResourceIdentifier.Name;
        }
    }
}