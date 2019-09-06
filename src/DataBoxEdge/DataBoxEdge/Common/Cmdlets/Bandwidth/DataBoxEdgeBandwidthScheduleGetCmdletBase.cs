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

using System;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Resource = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Resources.Resource;

namespace Microsoft.Azure.Commands.DataBoxEdge.Common
{
    [Cmdlet(VerbsCommon.Get, Constants.BandwidthSchedule, DefaultParameterSetName = ListParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeBandWidthSchedule))]
    public class DataBoxEdgeBandwidthGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ListParameterSet = "ListParameterSet";
        private const string GetByNameParameterSet = "GetByNameParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = ResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ListParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = ListParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        public bool NotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }

        private List<PSDataBoxEdgeBandWidthSchedule> GetForResourceName()
        {
            var bwObj = BandwidthSchedulesOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.BandwidthSchedules,
                this.DeviceName,
                this.Name,
                this.ResourceGroupName);
            return new List<PSDataBoxEdgeBandWidthSchedule>() { new PSDataBoxEdgeBandWidthSchedule(bwObj) };
        }

        private List<PSDataBoxEdgeBandWidthSchedule> GetForDevice()
        {
            var bandwidthSchedules = BandwidthSchedulesOperationsExtensions.ListByDataBoxEdgeDevice(
                    this.DataBoxEdgeManagementClient.BandwidthSchedules,
                    this.DeviceName,
                    this.ResourceGroupName);
            var paginatedResult = new List<BandwidthSchedule>(bandwidthSchedules);
            while (NotNullOrEmpty(bandwidthSchedules.NextPageLink))
            {
                bandwidthSchedules = BandwidthSchedulesOperationsExtensions.ListByDataBoxEdgeDeviceNext(
                    this.DataBoxEdgeManagementClient.BandwidthSchedules,
                    bandwidthSchedules.NextPageLink
                );
                paginatedResult.AddRange(bandwidthSchedules);
            }
            return paginatedResult.Select(t => new PSDataBoxEdgeBandWidthSchedule(t)).ToList();
        }


        public override void ExecuteCmdlet()
        {
            var results = new List<PSDataBoxEdgeBandWidthSchedule>();
            if (this.ParameterSetName.Equals(ResourceIdParameterSet))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                if (resourceIdentifier.IsDeviceSubType)
                {
                    this.DeviceName = resourceIdentifier.DeviceName;
                    this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                    this.Name = resourceIdentifier.ResourceName;
                    results = GetForResourceName();
                }
                else 
                {
                    this.DeviceName = resourceIdentifier.ResourceName;
                    this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                    results = GetForDevice();
                    
                }
                
            }
            else if (this.ParameterSetName.Equals(GetByNameParameterSet))
            {
                results = GetForResourceName();
            }
            else if (this.ParameterSetName.Equals(ListParameterSet))
            {
                results = GetForDevice();
            }

            WriteObject(results, true);
        }
    }
}