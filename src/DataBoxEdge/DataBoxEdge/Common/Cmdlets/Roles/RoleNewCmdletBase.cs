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
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.Common.Strategies;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.Management.WebSites.Version2016_09_01.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Roles
{
    [Cmdlet(VerbsCommon.New, Constants.Role, DefaultParameterSetName = ConnectionStringParameterSet),
     OutputType(typeof(PSDataBoxEdgeStorageAccountCredential))]
    public class RoleNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ConnectionStringParameterSet = "ConnectionStringParameterSet";
        private const string IOTParameterSet = "IOTParameterSet";

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Name { get; set; }

        [Parameter(Mandatory = false,
            HelpMessage = "Use this to Provide Connection Strings",
            ParameterSetName = ConnectionStringParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public SwitchParameter ConnectionString { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = ConnectionStringParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string IOTDeviceConnectionString { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ConnectionStringParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string IOTEdgeDeviceConnectionString { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Use this to Provide Device Properties",
            ParameterSetName = IOTParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public SwitchParameter DeviceProperties { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = IOTParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string IOTDeviceId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = IOTParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string IOTDeviceAccessKey { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = IOTParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string IOTEdgeDeviceId { get; set; }


        [Parameter(Mandatory = true, ParameterSetName = IOTParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string IOTEdgeDeviceAccessKey { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = IOTParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string IOTHostHub { get; set; }


        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string EncryptionKey { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string Platform { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string RoleStatus { get; set; }
        

        public static string HostName = "HostName";
        public static string DeviceId = "DeviceId";
        public static string SharedAccessKey = "SharedAccessKey";

        public static IoTRole GetIoTRoleObject(
            string deviceId,
            string edgeDeviceId,
            string ioTHostHub,
            string platform,
            AsymmetricEncryptedSecret iotDeviceSecret,
            AsymmetricEncryptedSecret iotEdgeDeviceSecret,
            string roleStatus)
        {
            var authentication = new Authentication() {SymmetricKey = new SymmetricKey(iotDeviceSecret)};
            var ioTDeviceInfo = new IoTDeviceInfo(deviceId, ioTHostHub, authentication);

            var edgeAuthentication = new Authentication()
                {SymmetricKey = new SymmetricKey(iotEdgeDeviceSecret)};
            var ioTEdgeDeviceInfo = new IoTDeviceInfo(edgeDeviceId, ioTHostHub, edgeAuthentication);

            var ioTRole = new IoTRole(platform, ioTDeviceInfo, ioTEdgeDeviceInfo, roleStatus);
            return ioTRole;
        }

        private static void ThrowInvalidConnection(string message = "Invalid connection string")
        {
            throw new PSInvalidOperationException(message);
        }

        private static StringDictionary GetIotDeviceProperties(string connectionString)
        {
            var deviceProperties = new StringDictionary();
            var iotProps = connectionString.Split(';');
            foreach (var iotProp in iotProps)
            {
                var deviceProperty = iotProp.Split('=');
                if (deviceProperty.Length != 2)
                {
                    ThrowInvalidConnection();
                }
                else
                {
                    deviceProperties.Properties.Add(deviceProperty[0], deviceProperty[1]);
                }
            }

            var keys = new List<string> {HostName, DeviceId, SharedAccessKey};

            foreach (var key in keys)
            {
                if (!deviceProperties.Properties.ContainsKey(key))
                {
                    ThrowInvalidConnection("Missing property " + key + " in connection string");
                }
            }

            return deviceProperties;
        }

        private void ParseIotDeviceConnectionString()
        {
            var deviceProperties = GetIotDeviceProperties(this.IOTDeviceConnectionString);
            this.IOTDeviceId = deviceProperties.Properties.GetOrNull(DeviceId);
            this.IOTDeviceAccessKey = deviceProperties.Properties.GetOrNull(SharedAccessKey);
            this.IOTHostHub = deviceProperties.Properties.GetOrNull(HostName);
        }

        private void ParseEdgeDeviceConnectionString()
        {
            var deviceProperties = GetIotDeviceProperties(this.IOTEdgeDeviceConnectionString);
            this.IOTEdgeDeviceId = deviceProperties.Properties.GetOrNull(DeviceId);
            this.IOTEdgeDeviceAccessKey = deviceProperties.Properties.GetOrNull(SharedAccessKey);
            this.IOTHostHub = deviceProperties.Properties.GetOrNull(HostName);
        }


        public override void ExecuteCmdlet()
        {
            if (ConnectionString.IsPresent)
            {
                ParseIotDeviceConnectionString();
                ParseEdgeDeviceConnectionString();
            }

            if (!ConnectionString.IsPresent && !DeviceProperties.IsPresent)
            {
                throw new Exception(
                    string.Format(
                        "Please select one of -'{0}' or -'{1}'", nameof(this.ConnectionString), nameof(this.DeviceProperties)));

            }

            var iotDeviceSecret = DataBoxEdgeManagementClient.Devices.GetAsymmetricEncryptedSecret(
                this.DeviceName,
                this.ResourceGroupName,
                this.IOTDeviceAccessKey,
                this.EncryptionKey
            );

            var iotEdgeDeviceSecret = DataBoxEdgeManagementClient.Devices.GetAsymmetricEncryptedSecret(
                this.DeviceName,
                this.ResourceGroupName,
                this.IOTEdgeDeviceAccessKey,
                this.EncryptionKey
            );

            var results = new List<PSDataBoxEdgeRole>();
            var iotRole = GetIoTRoleObject(
                this.IOTDeviceId,
                this.IOTEdgeDeviceId,
                this.IOTHostHub,
                this.Platform,
                iotDeviceSecret,
                iotEdgeDeviceSecret,
                this.RoleStatus
            );

            var psRole = new PSDataBoxEdgeRole(
                DataBoxEdgeManagementClient.Roles.CreateOrUpdate(
                    this.DeviceName, this.Name, iotRole,
                    this.ResourceGroupName)
            );

            results.Add(psRole);

            WriteObject(results, true);
        }
    }
}