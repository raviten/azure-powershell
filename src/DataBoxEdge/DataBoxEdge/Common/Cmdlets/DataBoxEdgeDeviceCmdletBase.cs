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

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.DataBox.Models;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using Microsoft.Rest.Azure;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using JobsOperationsExtensions = Microsoft.Azure.Management.DataBox.JobsOperationsExtensions;

namespace Microsoft.Azure.Commands.DataBoxEdge.Common
{
    [Cmdlet(VerbsCommon.Get,
         "AzDataBoxEdgeDeviceOld",
         DefaultParameterSetName = ListParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeDevice))]
    public class DataBoxEdgeDeviceCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ListParameterSet = "ListParameterSet";
        private const string GetByNameParameterSet = "GetByNameParameterSet";
        private const string GetByResourceIdParameterSet = "GetByResourceIdParameterSet";

        [Parameter(Mandatory = false, ParameterSetName = ListParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
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

        [Parameter(Mandatory = false, ParameterSetName = ListParameterSet)]
        public SwitchParameter Completed { get; set; } = false;

        [Parameter(Mandatory = false, ParameterSetName = ListParameterSet)]
        public SwitchParameter CompletedWithError { get; set; } = false;

        [Parameter(Mandatory = false, ParameterSetName = ListParameterSet)]
        public SwitchParameter Cancelled { get; set; } = false;

        [Parameter(Mandatory = false, ParameterSetName = ListParameterSet)]
        public SwitchParameter Aborted { get; set; } = false;

        public override void ExecuteCmdlet()
        {
            WriteVerbose("Inside Execute CMDletbase");
            if (this.ParameterSetName.Equals("GetByResourceIdParameterSet"))
            {
                this.ResourceGroupName = ResourceIdHandler.GetResourceGroupName(ResourceId);
                this.Name = ResourceIdHandler.GetResourceName(ResourceId);
            }

            if (!string.IsNullOrEmpty(this.Name))
            {
                List<PSDataBoxEdgeDevice> result = new List<PSDataBoxEdgeDevice>();
                result.Add(new PSDataBoxEdgeDevice(
                    DevicesOperationsExtensions.Get(
                        this.DataBoxEdgeManagementClient.Devices,
                        this.Name,
                        this.ResourceGroupName)));
                WriteObject(result, true);
            }
            else if (!string.IsNullOrEmpty(this.ResourceGroupName))
            {
                IPage<DataBoxEdgeDevice> dataBoxEdgePageList = null;
                List<DataBoxEdgeDevice> result = new List<DataBoxEdgeDevice>();
                List<PSDataBoxEdgeDevice> finalResult = new List<PSDataBoxEdgeDevice>();

                do
                {
                    // Lists all the jobs available under resource group.
                    if (dataBoxEdgePageList == null)
                    {
                        dataBoxEdgePageList = DevicesOperationsExtensions.ListByResourceGroup(
                            this.DataBoxEdgeManagementClient.Devices,
                            this.ResourceGroupName);
                    }
                    else
                    {
                        dataBoxEdgePageList = DevicesOperationsExtensions.ListByResourceGroupNext(
                            this.DataBoxEdgeManagementClient.Devices,
                            this.ResourceGroupName);
                    }

                    foreach (DataBoxEdgeDevice device in dataBoxEdgePageList)
                    {
                        result.Add(device);
                    }
                } while (!(string.IsNullOrEmpty(dataBoxEdgePageList.NextPageLink)));

                foreach (var dataBoxEdgeDevice in result)
                {
                    finalResult.Add(new PSDataBoxEdgeDevice(dataBoxEdgeDevice));
                }

                WriteObject(finalResult, true);
            }
            else
            {
                WriteVerbose("Inside fetching subscription");
                IPage<DataBoxEdgeDevice> dataBoxEdgeDevices = null;
                List<DataBoxEdgeDevice> result = new List<DataBoxEdgeDevice>();
                List<PSDataBoxEdgeDevice> finalResult = new List<PSDataBoxEdgeDevice>();

                do
                {
                    // Lists all the jobs available under the subscription.
                    if (dataBoxEdgeDevices == null)
                    {
                        WriteVerbose("List is null");
                        IPage<DataBoxEdgeDevice> devices = DevicesOperationsExtensions.ListBySubscription(
                            this.DataBoxEdgeManagementClient.Devices);
                        dataBoxEdgeDevices = DevicesOperationsExtensions.ListBySubscription(
                            this.DataBoxEdgeManagementClient.Devices);
                    }
                    else
                    {
                        WriteVerbose("Fetching next Devices List");
                        WriteVerbose(dataBoxEdgeDevices.NextPageLink);
                        dataBoxEdgeDevices = DevicesOperationsExtensions.ListBySubscriptionNext(
                            this.DataBoxEdgeManagementClient.Devices,
                            dataBoxEdgeDevices.NextPageLink);
                    }

                    result.AddRange(dataBoxEdgeDevices.ToList());
                } while (!(string.IsNullOrEmpty(dataBoxEdgeDevices.NextPageLink)));

                foreach (var dataBoxEdgeDevice in result)
                {
                    WriteVerbose(dataBoxEdgeDevice.Id);
                    finalResult.Add(new PSDataBoxEdgeDevice(dataBoxEdgeDevice));
                }

                WriteVerbose("Final Devices " + finalResult.Count);

                WriteObject(finalResult, true);
            }
        }
    }
}