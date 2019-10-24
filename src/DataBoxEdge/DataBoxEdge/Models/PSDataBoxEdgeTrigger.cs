using System;
using Microsoft.Azure.Management.EdgeGateway.Models;
using Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Common;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Trigger = Microsoft.Azure.Management.EdgeGateway.Models.Trigger;

namespace Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models
{
    public class PSDataBoxEdgeTrigger
    {
        public Trigger Trigger;

        [Ps1Xml(Label = "ResourceGroupName", Target = ViewControl.Table)]
        public string ResourceGroupName;


        [Ps1Xml(Label = "DeviceName", Target = ViewControl.Table, Position = 1)]
        public string DeviceName;

        public string Properties;

        public string Id;

        [Ps1Xml(Label = "Name", Target = ViewControl.Table, Position = 2)]
        public string Name;

        [Ps1Xml(Label = "Kind", Target = ViewControl.Table, Position = 3)]
        public string Kind;

        public FileEventTrigger PSFileEventTrigger { get; set; }
        public PeriodicTimerEventTrigger PSPeriodicTimerEventTrigger { get; set; }

        public PSDataBoxEdgeTrigger()
        {
            Trigger = new Trigger();
        }

        public PSDataBoxEdgeTrigger(Trigger trigger)
        {
            this.Trigger = trigger ?? throw new ArgumentNullException("trigger");
            this.Id = trigger.Id;
            var resourceIdentifier = new DataBoxEdgeResourceIdentifier(trigger.Id);
            this.ResourceGroupName = resourceIdentifier.ResourceGroupName;
            this.DeviceName = resourceIdentifier.DeviceName;
            this.Name = resourceIdentifier.ResourceName;
           

            switch (trigger)
            {
                case FileEventTrigger fileEventTrigger:
                    this.Kind = "FileEvent";
                    this.PSFileEventTrigger = fileEventTrigger;
                    var share = new DataBoxEdgeResourceIdentifier(fileEventTrigger.SourceInfo.ShareId);
                    var fileEventTriggerSinkRole = new DataBoxEdgeResourceIdentifier(fileEventTrigger.SinkInfo.RoleId);
                    this.Properties = "Share: " + share.Name;
                    this.Properties += ", Role: " + fileEventTriggerSinkRole.Name;
                    break;
                case PeriodicTimerEventTrigger periodicTimerEventTrigger:
                    this.Kind = "PeriodicTimerEvent";
                    this.PSPeriodicTimerEventTrigger = periodicTimerEventTrigger;
                    var periodicTimerEventSinkRole =
                        new DataBoxEdgeResourceIdentifier(periodicTimerEventTrigger.SinkInfo.RoleId);
                    this.Properties = "Schedule: " + periodicTimerEventTrigger.SourceInfo.Schedule;
                    this.Properties += ", StartTime: " + periodicTimerEventTrigger.SourceInfo.StartTime;
                    this.Properties += ", Topic: " + periodicTimerEventTrigger.SourceInfo.Topic;
                    this.Properties += ", Role: " + periodicTimerEventSinkRole.Name;
                    break;
            }
        }
    }
}