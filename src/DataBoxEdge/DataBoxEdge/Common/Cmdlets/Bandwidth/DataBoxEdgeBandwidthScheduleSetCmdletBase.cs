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

using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.WindowsAzure.Commands.Utilities.Common;
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.BandwidthSchedule;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Bandwidth
{
    using HelpMessageConstants = BandwidthScheduleHelpMessages;
    using PSResourceModel = PSDataBoxEdgeBandWidthSchedule;

    [Cmdlet(VerbsCommon.Set, Constants.BandwidthSchedule, DefaultParameterSetName = UpdateByNameParameterSet,
         SupportsShouldProcess = true
     ),
     OutputType(typeof(PSDataBoxEdgeBandWidthSchedule))]
    public class DataBoxEdgeBandwidthSetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string UpdateByNameParameterSet = "UpdateByNameParameterSet";
        private const string UpdateByInputObjectParameterSet = "UpdateByInputObjectParameterSet";
        private const string BandwidthSchedule = "BandwidthSchedule";
        private const string UnlimitedBandwidthSchedule = "UnlimitedBandwidthSchedule";

        [Parameter(Mandatory = true, ParameterSetName = ResourceIdParameterSet,
            HelpMessage = Constants.ResourceIdHelpMessage, Position = 0)]
        [Parameter(Mandatory = true, ParameterSetName = BandwidthSchedule,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = UpdateByInputObjectParameterSet,
            HelpMessage = Constants.ResourceIdHelpMessage, Position = 0)]
        [Parameter(Mandatory = true, ParameterSetName = BandwidthSchedule,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [ValidateNotNullOrEmpty]
        public PSResourceModel PSDataBoxEdgeBandWidthSchedule { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = UpdateByNameParameterSet,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [Parameter(Mandatory = true, ParameterSetName = BandwidthSchedule,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = UpdateByNameParameterSet,
            HelpMessage = Constants.DeviceNameHelpMessage, Position = 1)]
        [Parameter(Mandatory = true, ParameterSetName = BandwidthSchedule,
            HelpMessage = Constants.DeviceNameHelpMessage, Position = 1)]
        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = Constants.DeviceNameHelpMessage, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = UpdateByNameParameterSet,
            HelpMessage = Constants.NameHelpMessage, Position = 2)]
        [Parameter(Mandatory = true, ParameterSetName = BandwidthSchedule, 
            HelpMessage = Constants.NameHelpMessage, Position = 2)]
        [Parameter(Mandatory = true, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = Constants.NameHelpMessage, Position = 2)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.StartTime)]
        [ValidateNotNullOrEmpty]
        public string StartTime { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.StopTime)]
        [ValidateNotNullOrEmpty]
        public string StopTime { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.DaysOfWeek)]
        [ValidateNotNullOrEmpty]
        public string[] DaysOfWeek { get; set; }


        [Parameter(Mandatory = false, ParameterSetName = ResourceIdParameterSet,
            HelpMessage = HelpMessageConstants.Bandwidth)]
        [Parameter(Mandatory = false, ParameterSetName = UpdateByInputObjectParameterSet,
            HelpMessage = HelpMessageConstants.Bandwidth)]
        [Parameter(Mandatory = false, ParameterSetName = UpdateByNameParameterSet,
            HelpMessage = HelpMessageConstants.Bandwidth)]
        [Parameter(Mandatory = false, ParameterSetName = BandwidthSchedule,
            HelpMessage = HelpMessageConstants.Bandwidth)]
        [ValidateNotNullOrEmpty]
        public int? Bandwidth { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ResourceIdParameterSet,
            HelpMessage = HelpMessageConstants.UnlimitedBandwidth)]
        [Parameter(Mandatory = false, ParameterSetName = UpdateByInputObjectParameterSet,
            HelpMessage = HelpMessageConstants.UnlimitedBandwidth)]
        [Parameter(Mandatory = false, ParameterSetName = UpdateByNameParameterSet,
            HelpMessage = HelpMessageConstants.UnlimitedBandwidth)]
        [Parameter(Mandatory = false, ParameterSetName = UnlimitedBandwidthSchedule,
            HelpMessage = HelpMessageConstants.UnlimitedBandwidth)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter UnlimitedBandwidth { get; set; }


        [Parameter(Mandatory = false, HelpMessage = Constants.AsJobHelpMessage)]
        public SwitchParameter AsJob { get; set; }

        private ResourceModel GetResourceModel()
        {
            return BandwidthSchedulesOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.BandwidthSchedules,
                this.DeviceName,
                this.Name,
                this.ResourceGroupName);
        }

        private PSResourceModel UpdateResourceModel()
        {
            var resourceModel = GetResourceModel();

            if (this.DaysOfWeek.Length != 0)
            {
                var days = new List<string>(this.DaysOfWeek);
                resourceModel.Days = days;
            }

            if (this.Bandwidth.HasValue)
            {
                resourceModel.RateInMbps = Bandwidth.Value;
            }

            if (UnlimitedBandwidth.IsPresent)
            {
                resourceModel.RateInMbps = 0;
            }

            if (this.Bandwidth.HasValue)
            {
                resourceModel.RateInMbps = Bandwidth.Value;
            }

            resourceModel.Start = this.StartTime;
            resourceModel.Stop = this.StopTime;
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
            if (this.IsParameterBound(c => c.ResourceId))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                this.DeviceName = resourceIdentifier.DeviceName;
                this.Name = resourceIdentifier.ResourceName;
            }

            if (this.IsParameterBound(c => c.PSDataBoxEdgeBandWidthSchedule))
            {
                this.ResourceGroupName = this.PSDataBoxEdgeBandWidthSchedule.ResourceGroupName;
                this.DeviceName = this.PSDataBoxEdgeBandWidthSchedule.DeviceName;
                this.Name = this.PSDataBoxEdgeBandWidthSchedule.Name;
            }

            if (this.ShouldProcess(this.Name,
                string.Format("Updating '{0}' in device '{1}' with name '{2}'.",
                    HelpMessageConstants.ObjectName, this.DeviceName, this.Name)))
            {
                var result = UpdateResourceModel();
                WriteObject(result);
            }
        }
    }
}