using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Role = Microsoft.Azure.Management.EdgeGateway.Models.Role;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSRole
    {
        [Ps1Xml(Label = "Role.Name", Target = ViewControl.Table,
            ScriptBlock = "$_.role.Name")]
        public Role Role;

        [Ps1Xml(Label = "ResourceGroup", Target = ViewControl.Table)]
        public string ResourceGroup;

        public string Id;
        public string Name;

        public PSRole()
        {
            Role = new Role();
        }

        public PSRole(Role role)
        {
            this.Role = role;
            this.Id = role.Id;
            this.Name = role.Name;
        }
    }
}