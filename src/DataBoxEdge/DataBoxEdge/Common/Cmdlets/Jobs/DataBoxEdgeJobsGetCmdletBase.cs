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
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Jobs
{
    [Cmdlet(VerbsCommon.Get,
         Constants.Job,
         DefaultParameterSetName = GetByNameParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeJob))]
    public class DataBoxEdgeJobsGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string GetByNameParameterSet = "GetByNameParameterSet";
        private const string GetByResourceIdObject = "GetByResourceIdObject";

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet, Position = 0,
            HelpMessage = Constants.ResourceGroupNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet, Position = 1,
            HelpMessage = HelpMessageJobs.DeviceName)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet, Position = 2,
            HelpMessage = HelpMessageJobs.Name)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = GetByResourceIdObject, Position = 0,
            HelpMessage = Constants.ResourceIdHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }


        public override void ExecuteCmdlet()
        {
            if (this.IsParameterBound(c => this.ResourceId))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                this.ResourceGroupName = this.ResourceGroupName;
                this.DeviceName = this.DeviceName;
                this.Name = this.Name;
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