---
external help file: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.dll-Help.xml
Module Name: Az.DataBoxEdge
online version:
schema: 2.0.0
---

# Set-AzDataBoxEdgeBandwidthSchedule

## SYNOPSIS
Update a Bandwidth Schedule for the device

## SYNTAX

### UpdateByNameParameterSet (Default)
```
Set-AzDataBoxEdgeBandwidthSchedule [-ResourceGroupName] <String> [-DeviceName] <String> [-Name] <String>
 [-StartTime <String>] [-StopTime <String>] [-DaysOfWeek <String[]>] [-AsJob]
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### ByResourceIdParameterSet
```
Set-AzDataBoxEdgeBandwidthSchedule [-ResourceId] <String> [-StartTime <String>] [-StopTime <String>]
 [-DaysOfWeek <String[]>] [-AsJob] [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### BandwidthSchedule
```
Set-AzDataBoxEdgeBandwidthSchedule [-ResourceId] <String>
 [-PSDataBoxEdgeBandWidthSchedule] <PSDataBoxEdgeBandWidthSchedule> [-ResourceGroupName] <String>
 [-DeviceName] <String> [-Name] <String> [-StartTime <String>] [-StopTime <String>] [-DaysOfWeek <String[]>]
 [-Bandwidth <Int32>] [-AsJob] [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### UnlimitedBandwidthSchedule
```
Set-AzDataBoxEdgeBandwidthSchedule [-ResourceId] <String>
 [-PSDataBoxEdgeBandWidthSchedule] <PSDataBoxEdgeBandWidthSchedule> [-ResourceGroupName] <String>
 [-DeviceName] <String> [-Name] <String> [-StartTime <String>] [-StopTime <String>] [-DaysOfWeek <String[]>]
 [-UnlimitedBandwidth] [-AsJob] [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### UpdateByInputObjectParameterSet
```
Set-AzDataBoxEdgeBandwidthSchedule [-PSDataBoxEdgeBandWidthSchedule] <PSDataBoxEdgeBandWidthSchedule>
 [-StartTime <String>] [-StopTime <String>] [-DaysOfWeek <String[]>] [-AsJob]
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

## DESCRIPTION
This **Set-AzDataBoxEdgeBandwidthSchedule** will help in updating Bandwidth schedule's configuration for a device.

## EXAMPLES

### Example 1
```powershell
PS C:\> New-AzDataBoxEdgeBandwidthSchedule  -ResourceGroupName resource-group-name -DeviceName device-name -Name bandwidth-schedule -UnlimitedBandwidth
Name                DaysOfWeek                    RateInMbps StartTime StopTime
----                ----------                    ---------- --------- --------
bandwidth-schedule  Sunday, Tuesday, Saturday     0          11:00:00  12:00:00
```

### Example 2
```powershell
PS C:\> New-AzDataBoxEdgeBandwidthSchedule -ResourceGroupName resource-group-name -DeviceName device-name -Name bandwidth-schedule -StopTime 21:00
Name                DaysOfWeek                    RateInMbps StartTime StopTime
----                ----------                    ---------- --------- --------
bandwidth-schedule  Sunday, Tuesday, Saturday     0          11:00:00  21:00:00
```

## PARAMETERS

### -AsJob
Run cmdlet in the background

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Bandwidth
Bandwidth in Mbps

```yaml
Type: System.Nullable`1[System.Int32]
Parameter Sets: BandwidthSchedule
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DaysOfWeek
Scheduled DaysOfWeek

```yaml
Type: System.String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultProfile
The credentials, account, tenant, and subscription used for communication with Azure.

```yaml
Type: Microsoft.Azure.Commands.Common.Authentication.Abstractions.Core.IAzureContextContainer
Parameter Sets: (All)
Aliases: AzContext, AzureRmContext, AzureCredential

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeviceName
Device Name

```yaml
Type: System.String
Parameter Sets: UpdateByNameParameterSet, BandwidthSchedule, UnlimitedBandwidthSchedule
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Resource Name

```yaml
Type: System.String
Parameter Sets: UpdateByNameParameterSet, BandwidthSchedule, UnlimitedBandwidthSchedule
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PSDataBoxEdgeBandWidthSchedule
Azure ResourceId

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeBandWidthSchedule
Parameter Sets: BandwidthSchedule, UnlimitedBandwidthSchedule, UpdateByInputObjectParameterSet
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
Resource Group Name

```yaml
Type: System.String
Parameter Sets: UpdateByNameParameterSet, BandwidthSchedule, UnlimitedBandwidthSchedule
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceId
Azure ResourceId

```yaml
Type: System.String
Parameter Sets: ByResourceIdParameterSet, BandwidthSchedule, UnlimitedBandwidthSchedule
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartTime
Schedule Start Time

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StopTime
Schedule Stop Time

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UnlimitedBandwidth
Set Unlimited Bandwidth

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: UnlimitedBandwidthSchedule
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeBandWidthSchedule

## NOTES

## RELATED LINKS
