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
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;

namespace Microsoft.Azure.Commands.DataBoxEdge.Common
{
    [Cmdlet(VerbsCommon.Set, Constants.BandwidthSchedule, DefaultParameterSetName = SetParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeBandWidthSchedule))]
    public class DataBoxEdgeBandwidthSetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string SetParameterSet = "SetParameterSet";

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string StartTime { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string StopTime { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string StartDay { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string StopDay { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public int Bandwidth { get; set; }

        public bool NotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }

        public override void ExecuteCmdlet()
        {
            var results = new List<PSDataBoxEdgeBandWidthSchedule>();
            var bandwidthSchedule = BandwidthSchedulesOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.BandwidthSchedules,
                this.DeviceName,
                this.Name,
                this.ResourceGroupName
            );
            if (NotNullOrEmpty(this.StartDay))
            {
                bandwidthSchedule.Days.Add(StartDay);
            }

            if (NotNullOrEmpty(this.StopDay))
            {
                bandwidthSchedule.Days.Add(this.StopDay);
            }

            if (NotNullOrEmpty(this.StartTime))
            {
                bandwidthSchedule.Start = this.StartTime;
            }

            if (NotNullOrEmpty(this.StartDay))
            {
                bandwidthSchedule.Stop = this.StopTime;
            }

            if (this.Bandwidth > 0)
            {
                bandwidthSchedule.RateInMbps = Bandwidth;
            }

            results.Add(
                new PSDataBoxEdgeBandWidthSchedule(
                    BandwidthSchedulesOperationsExtensions.CreateOrUpdate(
                        this.DataBoxEdgeManagementClient.BandwidthSchedules,
                        this.DeviceName,
                        this.Name,
                        bandwidthSchedule,
                        this.ResourceGroupName)));

            WriteObject(results, true);
        }
    }
}