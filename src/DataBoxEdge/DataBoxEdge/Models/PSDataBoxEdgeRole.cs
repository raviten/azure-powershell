﻿using System;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Role = Microsoft.Azure.Management.EdgeGateway.Models.Role;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeRole
    {
        [Ps1Xml(Label = "DataBoxEdgeRole.Name", Target = ViewControl.Table,
            ScriptBlock = "$_.role.Name")]
        public Role Role;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table)]
        public string ResourceGroupName;
        public string DeviceName { get; set; }

        public string Id;
        public string Name;

        public PSDataBoxEdgeRole()
        {
            Role = new Role();
        }

        public PSDataBoxEdgeRole(Role role)
        {
            this.Role = role ?? throw new ArgumentNullException("role");
            this.Id = role.Id;
            var resourceIdentifier = new DataBoxEdgeResourceIdentifier(role.Id);
            this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
            this.DeviceName = resourceIdentifier.DeviceName;
            this.Name = resourceIdentifier.ResourceName;
        }
    }
}