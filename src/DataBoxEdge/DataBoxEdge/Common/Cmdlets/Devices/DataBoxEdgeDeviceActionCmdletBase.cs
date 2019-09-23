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
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.WindowsAzure.Commands.Utilities.Common;
using ResourceModel = Microsoft.Azure.Management.EdgeGateway.Models.DataBoxEdgeDeviceExtendedInfo;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeDeviceExtendedInfo;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Devices
{
    [Cmdlet(VerbsCommon.Get, Constants.Action, DefaultParameterSetName = ActionByNameParameterSet
     ),
     OutputType(typeof(PSResourceModel))]
    public class DataBoxEdgeDeviceActionCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ActionByResourceIdParameterSet = "ActionByResourceIdParameterSet";
        private const string ActionByInputObjectSet = "ActionByInputObjectSet";
        private const string ActionByNameParameterSet = "ActionByNameParameterSet";

        [Parameter(Mandatory = true, 
            ParameterSetName = ActionByResourceIdParameterSet,
            HelpMessage = Constants.ResourceIdHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = ActionByInputObjectSet,
            HelpMessage = Constants.InputObjectHelpMessage,
            ValueFromPipeline = true)]
        [ValidateNotNull]
        public PSResourceModel InputObject { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = ActionByNameParameterSet,
            HelpMessage = Constants.ResourceGroupNameHelpMessage, 
            Position = 0)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }


        [Parameter(Mandatory = true, 
            ParameterSetName = ActionByNameParameterSet, 
            HelpMessage = Constants.NameHelpMessage,
            Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        private ResourceModel GetResourceModel()
        {
            return DevicesOperationsExtensions.GetExtendedInformation(
                this.DataBoxEdgeManagementClient.Devices,
                this.Name,
                this.ResourceGroupName);
        }


        private List<PSResourceModel> GetByResourceName()
        {
            var resourceModel = GetResourceModel();
            return new List<PSResourceModel>() {new PSResourceModel(resourceModel)};
        }

        public override void ExecuteCmdlet()
        {
            if (this.IsParameterBound(c => c.ResourceId))
            {
                var resourceIdentifier = new DataBoxEdgeResourceIdentifier(this.ResourceId);
                this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
                this.Name = resourceIdentifier.Name;
            }

            if (this.IsParameterBound(c => c.InputObject))
            {
                this.ResourceGroupName = this.InputObject.ResourceGroupName;
                this.Name = this.InputObject.DeviceName;
            }

            var results = GetByResourceName();
            WriteObject(results, true);
        }
    }
}