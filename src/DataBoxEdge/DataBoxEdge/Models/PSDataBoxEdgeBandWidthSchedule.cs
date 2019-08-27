using System;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using BandwidthSchedule = Microsoft.Azure.Management.EdgeGateway.Models.BandwidthSchedule;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeBandWidthSchedule
    {
        [Ps1Xml(Label = "Share.Name", Target = ViewControl.Table,
            ScriptBlock = "$_.share.Name")]
        public BandwidthSchedule BandwidthSchedule;

        [Ps1Xml(Label = "ResourceGroup", Target = ViewControl.Table)]
        public string ResourceGroup;

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
            this.Name = bandwidthSchedule.Name;
        }
    }
}