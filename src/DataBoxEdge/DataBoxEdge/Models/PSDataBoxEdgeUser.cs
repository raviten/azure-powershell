using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Microsoft.Azure.Commands.DataBoxEdge.Common;
using System.Text;
using User = Microsoft.Azure.Management.EdgeGateway.Models.User;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeUser
    {
        [Ps1Xml(Label = "DataBoxEdgeDevice.Name", Target = ViewControl.Table,
            ScriptBlock = "$_.dataBoxEdgeDevice.Name")]
        public User User;

        [Ps1Xml(Label = "ResourceGroup", Target = ViewControl.Table)]
        public string ResourceGroup;

        public string Id;
        public string Name;

        public PSDataBoxEdgeUser()
        {
            User = new User();
        }

        public PSDataBoxEdgeUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.User = user;
            this.Id = user.Id;
            this.Name = user.Name;
        }
    }
}