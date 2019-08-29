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
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;

namespace Microsoft.Azure.Commands.DataBoxEdge.Common
{
    [Cmdlet(VerbsCommon.New, Constants.Device, DefaultParameterSetName = NewParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeDevice))]
    public class DataBoxEdgeDeviceNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Location { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        [ValidateSet("Edge", "Gateway")]
        public string Sku { get; set; }

        public override void ExecuteCmdlet()
        {
            DataBoxEdgeDevice dbe = new DataBoxEdgeDevice();
            dbe.Sku = new Sku(this.Sku);
            dbe.Location = this.Location;
            var results = new List<PSDataBoxEdgeDevice>();
            var device = new PSDataBoxEdgeDevice(
                DevicesOperationsExtensions.CreateOrUpdate(
                    this.DataBoxEdgeManagementClient.Devices,
                    this.Name,
                    dbe,
                    this.ResourceGroupName));
            results.Add(device);

            WriteObject(results, true);
        }
    }
}