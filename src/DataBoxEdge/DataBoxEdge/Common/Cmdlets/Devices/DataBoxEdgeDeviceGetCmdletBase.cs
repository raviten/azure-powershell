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
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDevice;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeDevice;


namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Devices
{
    [Cmdlet(VerbsCommon.Get, Constants.Device, DefaultParameterSetName = ListParameterSet
     ),
     OutputType(typeof(PSResourceModel))]
    public class DataBoxEdgeDeviceGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ListParameterSet = "ListParameterSet";
        private const string GetByNameParameterSet = "GetByNameParameterSet";
        private const string GetByResourceGroupNameParameterSet = "GetByResourceGroupNameParameterSet";


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

        private ResourceModel GetResourceModel()
        {
            return DevicesOperationsExtensions.Get(
                this.DataBoxEdgeManagementClient.Devices,
                this.Name,
                this.ResourceGroupName);
        }


        private IPage<ResourceModel> ListResourceModel()
        {
            if (string.IsNullOrEmpty(this.ResourceGroupName))
            {
                return DevicesOperationsExtensions.ListByResourceGroup(
                    this.DataBoxEdgeManagementClient.Devices,
                    this.ResourceGroupName);
            }

            return DevicesOperationsExtensions.ListBySubscription(
                this.DataBoxEdgeManagementClient.Devices);
        }

        private IPage<ResourceModel> ListResourceModel(string nextPageLink)
        {
            if (string.IsNullOrEmpty(this.ResourceGroupName))
            {
                return DevicesOperationsExtensions.ListByResourceGroupNext(
                    this.DataBoxEdgeManagementClient.Devices,
                    nextPageLink);
            }

            return DevicesOperationsExtensions.ListBySubscriptionNext(
                this.DataBoxEdgeManagementClient.Devices,
                nextPageLink
            );
        }

        private List<PSResourceModel> GetByResourceName()
        {
            var resourceModel = GetResourceModel();
            return new List<PSResourceModel>() {new PSResourceModel(resourceModel)};
        }

        private List<PSResourceModel> ListForEverything()
        {
            var resourceModels = ListResourceModel();
            var paginatedResult = new List<ResourceModel>(resourceModels);
            while (!string.IsNullOrEmpty(resourceModels.NextPageLink))
            {
                resourceModels = ListResourceModel(resourceModels.NextPageLink);
                paginatedResult.AddRange(resourceModels);
            }

            return paginatedResult.Select(t => new PSResourceModel(t)).ToList();
        }

        public override void ExecuteCmdlet()
        {
            var results = new List<PSResourceModel>();
            if (this.ParameterSetName.Equals(ResourceIdParameterSet))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                this.Name = resourceIdentifier.ResourceName;
                if (string.IsNullOrEmpty(this.Name))
                {
                    results = GetByResourceName();
                }
                else
                {
                    results = ListForEverything();
                }
            }else if (this.ParameterSetName.Equals(GetByNameParameterSet))
            {
                results = GetByResourceName();
            }
            else if (this.ParameterSetName.Equals(ListParameterSet) ||
                     this.ParameterSetName.Equals(GetByResourceGroupNameParameterSet))
            {
                results = ListForEverything();
            }

            WriteObject(results, true);
        }
    }
}