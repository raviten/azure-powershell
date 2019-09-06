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
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;

namespace Microsoft.Azure.Commands.DataBoxEdge.Common.Roles
{
    [Cmdlet(VerbsCommon.Get, Constants.Role, DefaultParameterSetName = ListParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeRole))]
    public class RoleGetCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ListParameterSet = "ListParameterSet";
        private const string GetByNameParameterSet = "GetByNameParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = ListParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = GetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ListParameterSet)]
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
            var results = new List<PSDataBoxEdgeRole>();
            if (NotNullOrEmpty(this.Name))
            {
                results.Add(
                    new PSDataBoxEdgeRole(
                        RolesOperationsExtensions.Get(
                            this.DataBoxEdgeManagementClient.Roles,
                            this.DeviceName,
                            this.Name,
                            this.ResourceGroupName)));
            }
            else if (!string.IsNullOrEmpty(this.ResourceGroupName))
            {
                var roles = RolesOperationsExtensions.ListByDataBoxEdgeDevice(
                    this.DataBoxEdgeManagementClient.Roles,
                    this.DeviceName,
                    this.ResourceGroupName);
                var paginatedResult = new List<Role>(roles);
                while (NotNullOrEmpty(roles.NextPageLink))
                {
                    roles =
                        RolesOperationsExtensions.ListByDataBoxEdgeDeviceNext(
                            this.DataBoxEdgeManagementClient.Roles,
                            roles.NextPageLink);
                    paginatedResult.AddRange(roles);
                }

                results = paginatedResult.Select(t => new PSDataBoxEdgeRole(t)).ToList();
            }

            WriteObject(results, true);
        }
    }
}