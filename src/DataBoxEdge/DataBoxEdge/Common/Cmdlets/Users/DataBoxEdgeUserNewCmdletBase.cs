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
    [Cmdlet(VerbsCommon.New, Constants.User, DefaultParameterSetName = NewParameterSet
     ),
     OutputType(typeof(PSDataBoxEdgeDevice))]
    public class DataBoxEdgeUserNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Username { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Password { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string EncryptedKey { get; set; }

        public bool NotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }


        public override void ExecuteCmdlet()
        {
            AsymmetricEncryptedSecret encryptedSecret =
                DataBoxEdgeManagementClient.Devices.GetAsymmetricEncryptedSecret(
                    this.DeviceName,
                    this.ResourceGroupName,
                    this.Password,
                    this.EncryptedKey
                );
                var results = new List<PSDataBoxEdgeUser>();
            var user = new PSDataBoxEdgeUser(
                UsersOperationsExtensions.CreateOrUpdate(
                    this.DataBoxEdgeManagementClient.Users,
                    this.DeviceName,
                    this.Username,
                    this.ResourceGroupName,
                    encryptedSecret
                ));
            results.Add(user);

            WriteObject(results, true);
        }
    }
}