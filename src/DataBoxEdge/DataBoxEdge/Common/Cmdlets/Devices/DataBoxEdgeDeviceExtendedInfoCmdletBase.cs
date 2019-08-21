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
    [Cmdlet(VerbsCommon.Get, Constants.ExtendedInfo, DefaultParameterSetName = ExtendedInfoParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeDevice))]
    public class DataBoxEdgeDeviceExtendedInfoCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ExtendedInfoParameterSet = "ExtendedInfoParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = ExtendedInfoParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ExtendedInfoParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }


        public bool NotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }

        public override void ExecuteCmdlet()
        {
            var results = new List<DataBoxEdgeDeviceExtendedInfo>();
            if (NotNullOrEmpty(this.Name) && NotNullOrEmpty(this.ResourceGroupName))
            {
                var device = DevicesOperationsExtensions.GetExtendedInformation(
                    this.DataBoxEdgeManagementClient.Devices,
                    this.Name,
                    this.ResourceGroupName);
                results.Add(device);
            }

            WriteObject(results, true);
        }
    }
}