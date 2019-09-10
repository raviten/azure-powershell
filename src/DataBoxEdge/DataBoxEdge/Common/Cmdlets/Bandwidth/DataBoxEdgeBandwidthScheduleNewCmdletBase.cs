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
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.BandwidthSchedule;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeBandWidthSchedule;
using Resource = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Resources.Resource;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Bandwidth
{
    using HelpMessageConstants = BandwidthScheduleHelpMessages;

    [Cmdlet(VerbsCommon.New,
         Constants.BandwidthSchedule,
         DefaultParameterSetName = NewParameterSet,
         SupportsShouldProcess = true),
     OutputType(typeof(PSResourceModel))]
    public class DataBoxEdgeBandwidthNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";
        private const string UnlimitedBandwidthSchedule = "UnlimitedBandwidthSchedule";


        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet, HelpMessage = Constants.DeviceNameHelpMessage,
            Position = 1)]
        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = Constants.DeviceNameHelpMessage, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet, HelpMessage = Constants.NameHelpMessage,
            Position = 2)]
        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = Constants.NameHelpMessage, Position = 2)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.StartTime)]
        [ValidateNotNullOrEmpty]
        public string StartTime { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.StopTime)]
        [ValidateNotNullOrEmpty]
        public string StopTime { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.DaysOfWeek)]
        [ValidateNotNullOrEmpty]
        public string[] DaysOfWeek { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet,
            HelpMessage = HelpMessageConstants.Bandwidth)]
        [ValidateNotNullOrEmpty]
        public int? Bandwidth { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = HelpMessageConstants.UnlimitedBandwidth)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter UnlimitedBandwidth { get; set; }


        [Parameter(Mandatory = false, HelpMessage = Constants.AsJobHelpMessage)]
        public SwitchParameter AsJob { get; set; }

        private PSResourceModel CreateResourceModel()
        {
            if (UnlimitedBandwidth.IsPresent)
            {
                Bandwidth = 0;
            }

            if (!Bandwidth.HasValue)
            {
                throw new Exception(Resource.InvalidBandwidthInput);
            }

            var days = new List<string>(this.DaysOfWeek);
            var resourceModel = new ResourceModel(
                this.StartTime,
                this.StopTime,
                Bandwidth.Value,
                days,
                null,
                this.Name
            );
            return new PSResourceModel(
                BandwidthSchedulesOperationsExtensions.CreateOrUpdate(
                    this.DataBoxEdgeManagementClient.BandwidthSchedules,
                    this.DeviceName,
                    this.Name,
                    resourceModel,
                    this.ResourceGroupName));
        }

        public override void ExecuteCmdlet()
        {
            if (this.ShouldProcess(this.Name,
                string.Format("Creating a new '{0}' in device '{1}' with name '{2}'.",
                    HelpMessageConstants.ObjectName, this.DeviceName, this.Name)))
            {
                var result = new List<PSResourceModel>
                {
                    CreateResourceModel()
                };
                WriteObject(result);
            }
        }
    }
}