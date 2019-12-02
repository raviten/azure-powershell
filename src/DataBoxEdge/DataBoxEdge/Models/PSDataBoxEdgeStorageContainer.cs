using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.DataBoxEdge.Models;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeStorageContainer
    {
        [Ps1Xml(Label = "Name", Target = ViewControl.Table,
            ScriptBlock = "$_.container.Name", Position = 0)]
        [Ps1Xml(Label = "Name", Target = ViewControl.Table,
            ScriptBlock = "$_.container.DataFormat", Position = 1)]
        public Container EdgeStorageContainer;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table)]
        public string ResourceGroupName;

        [Ps1Xml(Label = "DeviceName", Target = ViewControl.Table)]
        public string DeviceName;

        [Ps1Xml(Label = "EdgeStorageAccountName", Target = ViewControl.Table)]
        public string EdgeStorageAccountName;

        public string Id;
        public string Name;

        public PSDataBoxEdgeStorageContainer()
        {
            EdgeStorageContainer = new Container();
        }


        public PSDataBoxEdgeStorageContainer(Container container)
        {
            this.EdgeStorageContainer = container ?? throw new ArgumentNullException(nameof(container));
            this.Id = container.Id;
            var resourceIdentifier = new DataBoxEdgeResourceIdentifier(container.Id);
            this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
            this.DeviceName = resourceIdentifier.DeviceName;
            this.EdgeStorageAccountName = resourceIdentifier.ParentResource;
            this.Name = resourceIdentifier.ResourceName;
        }
    }
}