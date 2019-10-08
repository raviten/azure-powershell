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
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security;
using Microsoft.Azure.Commands.Common.Strategies;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.EdgeGateway;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.Management.WebSites.Version2016_09_01.Models;
using Microsoft.WindowsAzure.Commands.Common;
using PSResourceModel = Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeRole;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common.Cmdlets.Roles
{
    [Cmdlet(VerbsCommon.New, Constants.Role, DefaultParameterSetName = ConnectionStringParameterSet,
         SupportsShouldProcess = true
         ),
     OutputType(typeof(PSResourceModel))]
    public class DataBoxEdgeRoleNewCmdletBase : AzureDataBoxEdgeCmdletBase
    {
        private const string ConnectionStringParameterSet = "ConnectionStringParameterSet";
        private const string IotParameterSet = "IotParameterSet";

        [Parameter(Mandatory = true, 
            HelpMessage = Constants.ResourceGroupNameHelpMessage,
            Position = 0)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = Constants.DeviceNameHelpMessage,
            Position = 1)]
        [ValidateNotNullOrEmpty]
        public string DeviceName { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = Constants.NameHelpMessage,
            Position = 2)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false,
            ParameterSetName = ConnectionStringParameterSet,
            HelpMessage = HelpMessageRoles.ConnectionStringHelpMessage)]
        public SwitchParameter ConnectionString { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = ConnectionStringParameterSet,
            HelpMessage = HelpMessageRoles.IotDeviceConnectionStringHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SecureString IotDeviceConnectionString { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = ConnectionStringParameterSet,
            HelpMessage = HelpMessageRoles.IotEdgeDeviceConnectionStringHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SecureString IotEdgeDeviceConnectionString { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = IotParameterSet,
            HelpMessage = HelpMessageRoles.DeviceProperties)]
        public SwitchParameter DeviceProperties { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = IotParameterSet,
            HelpMessage = HelpMessageRoles.IotDeviceIdHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string IotDeviceId { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = IotParameterSet,
            HelpMessage =  HelpMessageRoles.IotDeviceAccessKeyHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SecureString IotDeviceAccessKey { get; set; }

        private string iotDeviceAccessKey;

        [Parameter(Mandatory = true, 
            ParameterSetName = IotParameterSet,
            HelpMessage =  HelpMessageRoles.IotEdgeDeviceId)]
        [ValidateNotNullOrEmpty]
        public string IotEdgeDeviceId { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = IotParameterSet,
            HelpMessage = HelpMessageRoles.IotEdgeDeviceAccessKeyHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SecureString IotEdgeDeviceAccessKey { get; set; }
        private string iotEdegeDeviceAccessKey { get; set; }

        [Parameter(Mandatory = true, 
            ParameterSetName = IotParameterSet,
            HelpMessage = HelpMessageRoles.IotHostHubHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string IotHostHub { get; set; }

        [Parameter(Mandatory = true, HelpMessage = Constants.EncryptionKeyHelpMessage)]
        [ValidateNotNullOrEmpty]
        public SecureString EncryptionKey { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageRoles.PlatformHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Platform { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageRoles.RoleStatusHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string RoleStatus { get; set; }

        [Parameter(Mandatory = false, HelpMessage = Constants.AsJobHelpMessage)]
        public SwitchParameter AsJob { get; set; }

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

            foreach (var key in keys.Where(key => !deviceProperties.Properties.ContainsKey(key)))
            {
                ThrowInvalidConnection("Missing property " + key + " in connection string");
            }

            return deviceProperties;
        }

        private void ParseIotDeviceConnectionString()
        {
            var deviceProperties = GetIotDeviceProperties(this.IotDeviceConnectionString.ConvertToString());
            this.IotDeviceId = deviceProperties.Properties.GetOrNull(DeviceId);
            this.iotEdegeDeviceAccessKey = deviceProperties.Properties.GetOrNull(SharedAccessKey);
            this.IotHostHub = deviceProperties.Properties.GetOrNull(HostName);
        }

        private void ParseEdgeDeviceConnectionString()
        {
            var deviceProperties = GetIotDeviceProperties(this.IotEdgeDeviceConnectionString.ConvertToString());
            this.IotEdgeDeviceId = deviceProperties.Properties.GetOrNull(DeviceId);
            this.iotEdegeDeviceAccessKey = deviceProperties.Properties.GetOrNull(SharedAccessKey);
            this.IotHostHub = deviceProperties.Properties.GetOrNull(HostName);
        }


        public override void ExecuteCmdlet()
        {
            if (ConnectionString.IsPresent)
            {
                ParseIotDeviceConnectionString();
                ParseEdgeDeviceConnectionString();
            }

            if (DeviceProperties.IsPresent)
            {
                this.iotEdegeDeviceAccessKey = this.IotEdgeDeviceAccessKey.ConvertToString();
                this.iotDeviceAccessKey = this.IotDeviceAccessKey.ConvertToString();
            }

            if (!ConnectionString.IsPresent && !DeviceProperties.IsPresent)
            {
                throw new Exception(
                    string.Format(
                        "Please select one of -'{0}' or -'{1}'",
                        nameof(this.ConnectionString),
                        nameof(this.DeviceProperties)));
            }

            var iotDeviceSecret = DataBoxEdgeManagementClient.Devices.GetAsymmetricEncryptedSecret(
                this.DeviceName,
                this.ResourceGroupName,
                this.iotDeviceAccessKey,
                this.EncryptionKey.ConvertToString()
            );

            var iotEdgeDeviceSecret = DataBoxEdgeManagementClient.Devices.GetAsymmetricEncryptedSecret(
                this.DeviceName,
                this.ResourceGroupName,
                this.iotEdegeDeviceAccessKey,
                this.EncryptionKey.ConvertToString()
            );

            var results = new List<PSResourceModel>();
            var iotRole = GetIoTRoleObject(
                this.IotDeviceId,
                this.IotEdgeDeviceId,
                this.IotHostHub,
                this.Platform,
                iotDeviceSecret,
                iotEdgeDeviceSecret,
                this.RoleStatus
            );

            if (this.ShouldProcess(this.Name,
                string.Format("Creating '{0}' in device '{1}' with name '{2}'.",
                    HelpMessageRoles.ObjectName, this.DeviceName, this.Name)))
            {

                var psRole = new PSResourceModel(
                    DataBoxEdgeManagementClient.Roles.CreateOrUpdate(
                        this.DeviceName, this.Name, iotRole,
                        this.ResourceGroupName)
                );

                results.Add(psRole);

                WriteObject(results, true);
            }
        }
    }
}