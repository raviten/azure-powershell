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

namespace Microsoft.Azure.Commands.DataBoxEdge.Common
{
    [Cmdlet(VerbsCommon.Get, Constants.Device, DefaultParameterSetName = ListParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeDevice))]
    public class DataBoxEdgeDeviceGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ListParameterSet = "ListParameterSet";
        private const string GetByNameParameterSet = "GetByNameParameterSet";
        private const string GetByResourceGroupNameParameterSet = "GetByResourceGroupNameParameterSet";
        private const string GetByResourceIdParameterSet = "GetByResourceIdParameterSet";

        [Parameter(Mandatory = false, ParameterSetName = ListParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = GetByResourceGroupNameParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true,
            ParameterSetName = GetByResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        public bool NotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }


        public override void ExecuteCmdlet()
        {
            if (this.ParameterSetName.Equals(GetByResourceIdParameterSet))
            {
                this.ResourceGroupName = ResourceIdHandler.GetResourceGroupName(ResourceId);
                this.Name = ResourceIdHandler.GetResourceName(ResourceId);
            }

            var results = new List<PSDataBoxEdgeDevice>();
            if (NotNullOrEmpty(this.Name))
            {
                results.Add(
                    new PSDataBoxEdgeDevice(
                        DevicesOperationsExtensions.Get(
                            this.DataBoxEdgeManagementClient.Devices,
                            this.Name,
                            this.ResourceGroupName)));
            }
            else if (!string.IsNullOrEmpty(this.ResourceGroupName))
            {
                var dataBoxEdgeDevices = DevicesOperationsExtensions.ListByResourceGroup(
                    this.DataBoxEdgeManagementClient.Devices,
                    this.ResourceGroupName);
                var paginatedResult = new List<DataBoxEdgeDevice>(dataBoxEdgeDevices);
                while (NotNullOrEmpty(dataBoxEdgeDevices.NextPageLink))
                {
                    dataBoxEdgeDevices = DevicesOperationsExtensions.ListByResourceGroupNext(
                        this.DataBoxEdgeManagementClient.Devices,
                        dataBoxEdgeDevices.NextPageLink
                    );
                    paginatedResult.AddRange(dataBoxEdgeDevices);
                }

                results = paginatedResult.Select(t => new PSDataBoxEdgeDevice(t)).ToList();
            }
            else
            {
                var dataBoxEdgeDevices = DevicesOperationsExtensions.ListBySubscription(
                    this.DataBoxEdgeManagementClient.Devices);
                var paginatedResult = new List<DataBoxEdgeDevice>(dataBoxEdgeDevices);
                while (NotNullOrEmpty(dataBoxEdgeDevices.NextPageLink))
                {
                    dataBoxEdgeDevices = DevicesOperationsExtensions.ListBySubscriptionNext(
                        this.DataBoxEdgeManagementClient.Devices,
                        dataBoxEdgeDevices.NextPageLink
                    );
                    paginatedResult.AddRange(dataBoxEdgeDevices);
                }

                results = paginatedResult.Select(t => new PSDataBoxEdgeDevice(t)).ToList();
            }

            WriteObject(results, true);
        }
    }
}