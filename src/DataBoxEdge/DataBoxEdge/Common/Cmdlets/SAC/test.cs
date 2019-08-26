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
    [Cmdlet(VerbsCommon.Get, Constants.Test, DefaultParameterSetName = NewParameterSet),
     OutputType(typeof(PSStorageAccountCredential))]
    public class TestCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string NewParameterSet = "NewParameterSet";
        private const string SMBParameterSet = "SMBParameterSet";
        private const string NFSParameterSet = "NFSParameterSet";

        [Parameter(Mandatory = true, ParameterSetName = NewParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = SMBParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NFSParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Collect notice log type.", ParameterSetName = SMBParameterSet)]
        public SwitchParameter SMB { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SMBParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Username { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NFSParameterSet,
            HelpMessage = "Collect notice log type.")]
        public SwitchParameter NFS { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NFSParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ClientId { get; set; }
        public bool NotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }


        public override void ExecuteCmdlet()
        {
            WriteVerbose("NFS.IsPresent" + NFS.IsPresent);
        }
    }
}