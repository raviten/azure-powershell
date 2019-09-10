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

using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Rest.Azure;
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.Share;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeShare;


namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Share
{
    [Cmdlet(VerbsCommon.Get, Constants.Share, DefaultParameterSetName = ListParameterSet
     ),
     OutputType(typeof(PSResourceModel))]
    public class DataBoxEdgeShareGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ListParameterSet = "ListParameterSet";
        private const string GetByNameParameterSet = "GetByNameParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = ResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ListParameterSet,
            HelpMessage = Constants.ResourceGroupNameHelpMessage)]
        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet,
            HelpMessage = Constants.ResourceGroupNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet, HelpMessage = Constants.NameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ListParameterSet,
            HelpMessage = Constants.DeviceNameHelpMessage)]
        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet,
            HelpMessage = Constants.DeviceNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        private ResourceModel GetResourceModel()
        {
            return SharesOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.Shares,
                this.DeviceName,
                this.Name,
                this.ResourceGroupName);
        }

        private IPage<ResourceModel> ListResourceModel()
        {
            return SharesOperationsExtensions.ListByDataBoxEdgeDevice(
                this.DataBoxEdgeManagementClient.Shares,
                this.DeviceName,
                this.ResourceGroupName);
        }

        private IPage<ResourceModel> ListResourceModel(string nextPageLink)
        {
            return SharesOperationsExtensions.ListByDataBoxEdgeDeviceNext(
                this.DataBoxEdgeManagementClient.Shares,
                nextPageLink
            );
        }

        private List<PSResourceModel> GetByResourceName()
        {
            var resourceModel = GetResourceModel();
            return new List<PSResourceModel>() {new PSResourceModel(resourceModel)};
        }

        private List<PSResourceModel> ListByDevice()
        {
            var resourceModel = ListResourceModel();
            var paginatedResult = new List<ResourceModel>(resourceModel);
            while (!string.IsNullOrEmpty(resourceModel.NextPageLink))
            {
                resourceModel = ListResourceModel(resourceModel.NextPageLink);
                paginatedResult.AddRange(resourceModel);
            }

            return paginatedResult.Select(t => new PSResourceModel(t)).ToList();
        }

        public override void ExecuteCmdlet()
        {
            var results = new List<PSResourceModel>();
            if (this.ParameterSetName.Equals(ResourceIdParameterSet))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                if (resourceIdentifier.IsSubResource)
                {
                    this.DeviceName = resourceIdentifier.DeviceName;
                    this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                    this.Name = resourceIdentifier.ResourceName;
                    results = GetByResourceName();
                }
                else
                {
                    this.DeviceName = resourceIdentifier.ResourceName;
                    this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                    results = ListByDevice();
                }
            }
            else if (this.ParameterSetName.Equals(GetByNameParameterSet))
            {
                results = GetByResourceName();
            }
            else if (this.ParameterSetName.Equals(ListParameterSet))
            {
                results = ListByDevice();
            }

            WriteObject(results, true);
        }
    }
}