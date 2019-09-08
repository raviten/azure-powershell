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
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using Microsoft.Azure.Management.EdgeGateway;
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.BandwidthSchedule;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Bandwidth
{
    using HelpMessageConstants = BandwidthScheduleHelpMessages;
    using PSResourceModel = PSDataBoxEdgeBandWidthSchedule;

    [Cmdlet(VerbsCommon.Set, Constants.BandwidthSchedule, DefaultParameterSetName = SetParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeBandWidthSchedule))]
    public class DataBoxEdgeBandwidthSetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string SetParameterSet = "SetParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = ResourceIdParameterSet, HelpMessage = Constants.ResourceIdHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetParameterSet, HelpMessage = Constants.ResourceGroupNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetParameterSet, HelpMessage = Constants.NameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = Constants.DeviceNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.StartTime)]
        [ValidateNotNullOrEmpty]
        public string StartTime { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.StopTime)]
        [ValidateNotNullOrEmpty]
        public string StopTime { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.Days)]
        [ValidateNotNullOrEmpty]
        public string[] Days { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.Bandwidth)]
        [ValidateNotNullOrEmpty]
        public int? Bandwidth { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.UnlimitedBandwidth)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter UnlimitedBandwidth{ get; set; }

        private ResourceModel GetResourceModel()
        {
            return BandwidthSchedulesOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.BandwidthSchedules,
                this.DeviceName,
                this.Name,
                this.ResourceGroupName);
        }

        private PSResourceModel CreateResourceModel()
        {
            var resourceModel = GetResourceModel();

            if (this.Days.Length!=0)
            {
                var days = new List<string>(this.Days);
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
            if (this.ParameterSetName.Equals(ResourceIdParameterSet))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                if (resourceIdentifier.ValidateSubResource())
                {
                    this.DeviceName = resourceIdentifier.DeviceName;
                    this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                    this.Name = resourceIdentifier.ResourceName;
                }
            }

            var results = new List<PSResourceModel>
            {
                CreateResourceModel()
            };
            WriteObject(results, true);
        }
    }
}