using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using System.Text;
using DataBoxEdgeDevice = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDevice;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeJob
    {
        [Ps1Xml(Label = "Job.Name", Target = ViewControl.Table, ScriptBlock = "$_.job.Name")]
        public Job Job;

        public string Id;
        public string Name;

        public PSDataBoxEdgeJob()
        {
            Job = new Job();
        }

        public PSDataBoxEdgeJob(Job job)
        {
            this.Job = job ?? throw new ArgumentNullException("job");
            this.Name = job.Name;
            this.Id = job.Id;
        }
    }
}