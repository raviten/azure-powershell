using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common
{
    static class Constants
    {
        //Device Command List
        public const string AzurePrefix = "Az";
        public const string SERVICE_NAME = "DataBoxEdge";


        //Device Comands
        public const string Device = AzurePrefix + SERVICE_NAME + "Device";


        //Job Comands
        public const string Job = AzurePrefix + SERVICE_NAME + "Job";
        
        public const string ExtendedInfo = "ExtendedInfo";
    }
}