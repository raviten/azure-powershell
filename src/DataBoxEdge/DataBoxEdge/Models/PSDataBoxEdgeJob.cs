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
        [Ps1Xml(Label = "DataBoxEdgeDevice.Name", Target = ViewControl.Table, ScriptBlock = "$_.job.Name")]
        public Job Job;

        [Ps1Xml(Label = "ResourceGroup", Target = ViewControl.Table)]
        public string ResourceGroup;

        public string Id;
        public string Name;

        public PSDataBoxEdgeJob()
        {
            Job = new Job();
        }

        public PSDataBoxEdgeJob(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException("job");
            }

            this.Job = job;
            this.ResourceGroup = ResourceIdHandler.GetResourceGroupName(job.Id);
            this.Name = job.Name;
            this.Id = job.Id;
        }
    }
}