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
    [Cmdlet(VerbsCommon.New, Constants.BandwidthSchedule, DefaultParameterSetName = NewParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeBandWidthSchedule))]
    public class DataBoxEdgeBandwidthNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string StartTime { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string StopTime { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string StartDay { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string StopDay { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public int Bandwidth { get; set; }


        public override void ExecuteCmdlet()
        {
            var results = new List<PSDataBoxEdgeBandWidthSchedule>();
            WriteVerbose(StartDay);
            WriteVerbose(StopDay);

            var days = new List<String>();
            days.Add(StartDay);
            days.Add(StopDay);
            var scheduler = new BandwidthSchedule(
                this.StartTime,
                this.StopTime,
                Bandwidth,
                days,
                null,
                this.Name
            );


            results.Add(
                new PSDataBoxEdgeBandWidthSchedule(
                    BandwidthSchedulesOperationsExtensions.CreateOrUpdate(
                        this.DataBoxEdgeManagementClient.BandwidthSchedules,
                        this.DeviceName,
                        this.Name,
                        scheduler,
                        this.ResourceGroupName)));

            WriteObject(results, true);
        }
    }
}