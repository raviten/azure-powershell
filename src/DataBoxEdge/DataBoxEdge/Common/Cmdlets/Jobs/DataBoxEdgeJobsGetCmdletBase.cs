// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using Microsoft.Rest.Azure;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using Job = Microsoft.Azure.Management.EdgeGateway.Models.Job;

namespace Microsoft.Azure.Commands.DataBoxEdge.Common
{
    [Cmdlet(VerbsCommon.Get,
         Constants.Job,
         DefaultParameterSetName = GetByNameParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeJob))]
    public class DataBoxEdgeJobsGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string GetByNameParameterSet = "GetByNameParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        public string ResourceId { get; set; }


        public override void ExecuteCmdlet()
        {
            if (this.ParameterSetName.Equals(GetByNameParameterSet))
            {
                this.ResourceGroupName = ResourceGroupName;
                this.DeviceName = DeviceName;
                this.Name = Name;
            }

            List<PSDataBoxEdgeJob> results = new List<PSDataBoxEdgeJob>();
            if (!string.IsNullOrEmpty(this.Name) &&
                !string.IsNullOrEmpty(this.DeviceName) &&
                !string.IsNullOrEmpty(this.ResourceGroupName))
            {
                results.Add(
                    new PSDataBoxEdgeJob(
                        JobsOperationsExtensions.Get(
                            this.DataBoxEdgeManagementClient.Jobs,
                            this.DeviceName,
                            this.Name,
                            this.ResourceGroupName
                        )
                    )
                );
            }

            WriteObject(results, true);
        }
    }
}