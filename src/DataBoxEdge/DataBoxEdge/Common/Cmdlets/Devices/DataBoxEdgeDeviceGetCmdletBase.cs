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
using Microsoft.WindowsAzure.Commands.Utilities.Common;
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDevice;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeDevice;


namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Devices
{
    [Cmdlet(VerbsCommon.Get, Constants.Device, DefaultParameterSetName = ListByParameterSet
     ),
     OutputType(typeof(PSResourceModel))]
    public class DataBoxEdgeDeviceGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ListByParameterSet = "ListByParameterSet";
        private const string GetByResourceIdParameterSet = "GetByResourceIdParameterSet";
        private const string GetByNameParameterSet = "GetByNameParameterSet";

        [Parameter(Mandatory = true, 
            ParameterSetName = GetByResourceIdParameterSet, 
            HelpMessage = Constants.ResourceIdHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = false, 
            ParameterSetName = ListByParameterSet,
            HelpMessage = Constants.ResourceGroupNameHelpMessage,
            Position = 0)]
        [Parameter(Mandatory = true, 
            ParameterSetName = GetByNameParameterSet, 
            HelpMessage = Constants.ResourceGroupNameHelpMessage,
            Position = 0)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = GetByNameParameterSet, 
            HelpMessage = Constants.ResourceGroupNameHelpMessage,
            Position = 1)]
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
            if (!string.IsNullOrEmpty(this.ResourceGroupName))
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
            if (!string.IsNullOrEmpty(this.ResourceGroupName))
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
            var results = new List<PSResourceModel>();
            if (!string.IsNullOrEmpty(this.Name))
            {
                return GetByResourceName();
            }
            else
            {
                var resourceModels = ListResourceModel();
                var paginatedResult = new List<ResourceModel>(resourceModels);
                while (!string.IsNullOrEmpty(resourceModels.NextPageLink))
                {
                    resourceModels = ListResourceModel(resourceModels.NextPageLink);
                    paginatedResult.AddRange(resourceModels);
                }

                results = paginatedResult.Select(t => new PSResourceModel(t)).ToList();
            }

            return results;
        }

        public override void ExecuteCmdlet()
        {
            var results = new List<PSResourceModel>();
            if (this.IsParameterBound(c => c.ResourceId))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                this.Name = resourceIdentifier.ResourceName;
            }

            results = ListForEverything();

            WriteObject(results, true);
        }
    }
}