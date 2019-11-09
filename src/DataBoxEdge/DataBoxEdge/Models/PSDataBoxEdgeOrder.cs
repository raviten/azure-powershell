using System;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using DataBoxEdgeOrder = Microsoft.Azure.Management.EdgeGateway.Models.Order;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeOrder
    {
        [Ps1Xml(Label = "DeviceType", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeDevice.DeviceType", Position = 2)]
        [Ps1Xml(Label = "Location", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeOrder.OrderStatus.'", Position = 4)]
        public DataBoxEdgeOrder DataBoxEdgeOrder;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table, Position = 3, GroupByThis = false)]
        public string ResourceGroupName { get; set; }

        [Ps1Xml(Label = "DeviceName", Target = ViewControl.Table, Position = 0)]
        public string DeviceName;

        public PSDataBoxEdgeOrderStatus OrderStatus;

        public string Id;

        public PSDataBoxEdgeOrder()
        {
            DataBoxEdgeOrder = new DataBoxEdgeOrder();
        }

        public PSDataBoxEdgeOrder(DataBoxEdgeOrder dataBoxEdgeOrder)
        {
            if (dataBoxEdgeOrder == null)
            {
                throw new ArgumentNullException("dataBoxEdgeOrder");
            }

            this.DataBoxEdgeOrder = dataBoxEdgeOrder;
            this.Id = dataBoxEdgeOrder.Id;
            var dataBoxEdgeResourceIdentifier = new DataBoxEdgeResourceIdentifier(dataBoxEdgeOrder.Id);
            this.ResourceGroupName = dataBoxEdgeResourceIdentifier.ResourceGroupName;
            this.DeviceName = dataBoxEdgeResourceIdentifier.Name;
            this.OrderStatus = new PSDataBoxEdgeOrderStatus(dataBoxEdgeOrder.CurrentStatus);
                                                                                    
        }
    }
}