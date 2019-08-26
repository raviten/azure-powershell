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
    [Cmdlet(VerbsCommon.Get, Constants.Share, DefaultParameterSetName = ListParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeUser))]
    public class DataBoxEdgeShareGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ListParameterSet = "ListParameterSet";
        private const string GetByNameParameterSet = "GetByNameParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = ListParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = ListParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        public bool NotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }


        public override void ExecuteCmdlet()
        {
            var results = new List<PSDataBoxEdgeShare>();
            if (NotNullOrEmpty(this.Name))
            {
                results.Add(
                    new PSDataBoxEdgeShare(
                        SharesOperationsExtensions.Get(
                            this.DataBoxEdgeManagementClient.Shares,
                            this.DeviceName,
                            this.Name,
                            this.ResourceGroupName)));
            }
            else if (!string.IsNullOrEmpty(this.ResourceGroupName))
            {
                var shares = SharesOperationsExtensions.ListByDataBoxEdgeDevice(
                    this.DataBoxEdgeManagementClient.Shares,
                    this.DeviceName,
                    this.ResourceGroupName);
                var paginatedResult = new List<Share>(shares);
                while (NotNullOrEmpty(shares.NextPageLink))
                {
                    shares = SharesOperationsExtensions.ListByDataBoxEdgeDeviceNext(
                        this.DataBoxEdgeManagementClient.Shares,
                        shares.NextPageLink
                    );
                    paginatedResult.AddRange(shares);
                }

                results = paginatedResult.Select(t => new PSDataBoxEdgeShare(t)).ToList();
            }

            WriteObject(results, true);
        }
    }
}