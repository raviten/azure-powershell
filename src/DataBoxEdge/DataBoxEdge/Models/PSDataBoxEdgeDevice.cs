using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using System.Text;
using DataBoxEdgeDevice = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDevice;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeDevice
    {
        [Ps1Xml(Label = "DataBoxEdgeDevice.Name", Target = ViewControl.Table, ScriptBlock = "$_.dataBoxEdgeDevice.Name")]
        public DataBoxEdgeDevice DataBoxEdgeDevice;

        [Ps1Xml(Label = "ResourceGroup", Target = ViewControl.Table)]
        public string ResourceGroup;

        public string Id;
        public string Name;

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
            this.ResourceGroup = ResourceIdHandler.GetResourceGroupName(dataBoxEdgeDevice.Id);
            this.Name = dataBoxEdgeDevice.Name;
            this.Id = dataBoxEdgeDevice.Id;
        }
    }
}