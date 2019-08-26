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
        public const string ExtendedInfo = AzurePrefix + SERVICE_NAME + "Device" + "ExtendedInfo";
        public const string User = AzurePrefix + SERVICE_NAME + "User";
        public const string Sac = AzurePrefix + SERVICE_NAME + "SAC";
        public const string Share = AzurePrefix + SERVICE_NAME + "Share";
        public const string Test = AzurePrefix + SERVICE_NAME + "Test";


        //Job Comands
        public const string Job = AzurePrefix + SERVICE_NAME + "Job";
        
        
    }
}