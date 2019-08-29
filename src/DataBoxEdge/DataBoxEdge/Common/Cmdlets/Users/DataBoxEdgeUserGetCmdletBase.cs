﻿// ----------------------------------------------------------------------------------
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
    [Cmdlet(VerbsCommon.Get, Constants.User, DefaultParameterSetName = ListParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeUser))]
    public class DataBoxEdgeUserGetCmdletBase : AzureDataBoxEdgeCmdletBase
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
            var results = new List<PSDataBoxEdgeUser>();
            if (this.ParameterSetName.Equals(GetByNameParameterSet))
            {
                results.Add(
                    new PSDataBoxEdgeUser(
                        UsersOperationsExtensions.Get(
                            this.DataBoxEdgeManagementClient.Users,
                            this.DeviceName,
                            this.Name,
                            this.ResourceGroupName)));
            }
            else if (this.ParameterSetName.Equals(ListParameterSet))
            {
                var users = UsersOperationsExtensions.ListByDataBoxEdgeDevice(
                    this.DataBoxEdgeManagementClient.Users,
                    this.DeviceName,
                    this.ResourceGroupName);
                var paginatedResult = new List<User>(users);
                while (NotNullOrEmpty(users.NextPageLink))
                {
                    users = UsersOperationsExtensions.ListByDataBoxEdgeDeviceNext(
                        this.DataBoxEdgeManagementClient.Users,
                        users.NextPageLink
                    );
                    paginatedResult.AddRange(users);
                }

                results = paginatedResult.Select(t => new PSDataBoxEdgeUser(t)).ToList();
            }

            WriteObject(results, true);
        }
    }
}