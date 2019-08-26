using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using System.Text;
using Share = Microsoft.Azure.Management.EdgeGateway.Models.Share;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeShare
    {
        [Ps1Xml(Label = "Share.Name", Target = ViewControl.Table,
            ScriptBlock = "$_.share.Name")]
        public Share Share;

        [Ps1Xml(Label = "ResourceGroup", Target = ViewControl.Table)]
        public string ResourceGroup;

        public string Id;
        public string Name;

        public PSDataBoxEdgeShare()
        {
            Share = new Share();
        }

        public PSDataBoxEdgeShare(Share share)
        {
            this.Share = share ?? throw new ArgumentNullException("share");
            this.Id = share.Id;
            this.Name = share.Name;
        }
    }
}