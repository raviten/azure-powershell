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
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Devices
{
    [Cmdlet(VerbsCommon.New,
         Constants.Device,
         DefaultParameterSetName = CreateByNewParameterSet,
         SupportsShouldProcess = true
     ),
     OutputType(typeof(PSDataBoxEdgeDevice))]
    public class DataBoxEdgeDeviceNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string CreateByNewParameterSet = "CreateByNewParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = CreateByNewParameterSet, Position = 0,
            HelpMessage = Constants.ResourceGroupNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = CreateByNewParameterSet, Position = 1,
            HelpMessage = Constants.NameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = CreateByNewParameterSet,
            HelpMessage = HelpMessageDevice.LocationHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = CreateByNewParameterSet,
            HelpMessage = HelpMessageDevice.SkuHelpMessage)]
        [ValidateNotNullOrEmpty]
        [PSArgumentCompleter("Edge", "Gateway")]
        public string Sku { get; set; }

        [Parameter(Mandatory = false, HelpMessage = Constants.AsJobHelpMessage)]
        public SwitchParameter AsJob { get; set; }

        public override void ExecuteCmdlet()
        {
            DataBoxEdgeDevice dbe = new DataBoxEdgeDevice();
            dbe.Sku = new Sku(this.Sku);
            dbe.Location = this.Location;
            var results = new List<PSDataBoxEdgeDevice>();
            if (this.ShouldProcess(this.Name,
                string.Format("Creating '{0}' with name '{1}'.",
                    HelpMessageDevice.ObjectName, this.Name)))
            {
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
}