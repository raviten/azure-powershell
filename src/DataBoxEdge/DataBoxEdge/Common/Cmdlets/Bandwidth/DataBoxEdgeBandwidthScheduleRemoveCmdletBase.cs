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
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Resource = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Resources.Resource;
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.BandwidthSchedule;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeBandWidthSchedule;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Bandwidth
{
    [Cmdlet(VerbsCommon.Remove, Constants.BandwidthSchedule, DefaultParameterSetName = RemoveByNameParameterSet
     ),
     OutputType(typeof(PSResourceModel))]
    public class DataBoxEdgeBandwidthRemoveCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string RemoveByNameParameterSet = "RemoveByNameParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = ResourceIdParameterSet, HelpMessage = Constants.ResourceIdHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RemoveByNameParameterSet, HelpMessage = Constants.ResourceGroupNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RemoveByNameParameterSet, HelpMessage = Constants.NameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RemoveByNameParameterSet, HelpMessage = Constants.DeviceNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = RemoveByNameParameterSet, HelpMessage = Constants.ForceHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter Force { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = RemoveByNameParameterSet, HelpMessage = Constants.PassThruHelpMessage)]
        public SwitchParameter PassThru;

        private bool Remove()
        {
            BandwidthSchedulesOperationsExtensions.Delete(
                this.DataBoxEdgeManagementClient.BandwidthSchedules,
                this.DeviceName,
                this.Name,
                this.ResourceGroupName);
            return true;
        }

        private bool ShouldProcess()
        {
            var action = string.Format(" ",
                Resource.Deleting,
                Constants.ServiceName,
                typeof(ResourceModel).Name,
                this.Name,
                Resource.InResourceGroup,
                this.ResourceGroupName
            );
            return ShouldProcess(this.Name, string.Format(action));
        }


        private bool ShouldContinue()
        {
            return ShouldContinue(string.Format(Resource.RemoveWarning + this.Name), "");
        }

        public override void ExecuteCmdlet()
        {
            if (this.ParameterSetName.Equals(ResourceIdParameterSet))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                if (resourceIdentifier.ValidateSubResource())
                {
                    this.DeviceName = resourceIdentifier.DeviceName;
                    this.Name = resourceIdentifier.ResourceName;
                    this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                }
            }

            if (ShouldProcess())
            {
                if (this.Force || ShouldContinue())
                {
                    if (PassThru)
                    {
                        WriteObject(Remove());
                    }
                    else
                    {
                        Remove();
                    }
                }
            }
        }
    }
}