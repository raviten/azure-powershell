using System;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using BandwidthSchedule = Microsoft.Azure.Management.EdgeGateway.Models.BandwidthSchedule;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeBandWidthSchedule
    {
        [Ps1Xml(Label = "BandwidthSchedule.Name", Target = ViewControl.Table,  ScriptBlock = "$_.bandwidthSchedule.Name")]
        [Ps1Xml(Label = "BandwidthSchedule.RateInMbps", Target = ViewControl.Table, ScriptBlock = "$_.bandwidthSchedule.RateInMbps")]
        [Ps1Xml(Label = "BandwidthSchedule.StartTime", Target = ViewControl.Table, ScriptBlock = "$_.bandwidthSchedule.Start")]
        [Ps1Xml(Label = "BandwidthSchedule.StopTime", Target = ViewControl.Table, ScriptBlock = "$_.bandwidthSchedule.Stop")]
        public BandwidthSchedule BandwidthSchedule;

        public string ResourceGroupName { get; set; }

        [Ps1Xml(Label = "Days", Target = ViewControl.Table)]
        public string Days
        {
            get
            {
                return string.Join(",", this.BandwidthSchedule.Days);
            }

        }

        public string Id;
        public string Name;

        public PSDataBoxEdgeBandWidthSchedule()
        {
            BandwidthSchedule = new BandwidthSchedule();
        }

        public PSDataBoxEdgeBandWidthSchedule(BandwidthSchedule bandwidthSchedule)
        {
            this.BandwidthSchedule = bandwidthSchedule ?? throw new ArgumentNullException("bandwidthSchedule");
            this.Id = bandwidthSchedule.Id;
            this.ResourceGroupName = ResourceIdHandler.GetResourceGroupName(bandwidthSchedule.Id);
            this.Name = bandwidthSchedule.Name;
            

        }
    }
}