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

### SetParameterSet (Default)
```
Set-AzDataBoxEdgeBandwidthSchedule -ResourceGroupName <String> -Name <String> [-DeviceName <String>]
 [-StartTime <String>] [-StopTime <String>] [-Days <String[]>] [-Bandwidth <Int32>] [-UnlimitedBandwidth]
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### ByResourceIdParameterSet
```
Set-AzDataBoxEdgeBandwidthSchedule -ResourceId <String> [-DeviceName <String>] [-StartTime <String>]
 [-StopTime <String>] [-Days <String[]>] [-Bandwidth <Int32>] [-UnlimitedBandwidth]
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

## DESCRIPTION
This **Set-AzDataBoxEdgeBandwidthSchedule** will help in updating Bandwidth schedule's configuration for a device.

## EXAMPLES

### Example 1
```powershell
PS C:\> New-AzDataBoxEdgeBandwidthSchedule  -ResourceGroupName resource-group-name -DeviceName device-name -Name bandwidth-schedule -UnlimitedBandwidth
Days                      BandwidthSchedule.Name   BandwidthSchedule.RateInMbps BandwidthSchedule.StartTime BandwidthSchedule.StopTime
----                      ----------------------   ---------------------------- --------------------------- --------------------------
Sunday, Tuesday, Saturday bandwidth-schedule       0                            11:00:00                    12:00:00

```

### Example 2
```powershell
PS C:\> New-AzDataBoxEdgeBandwidthSchedule -ResourceGroupName resource-group-name -DeviceName device-name -Name bandwidth-schedule -StopTime 21:00
Days                      BandwidthSchedule.Name   BandwidthSchedule.RateInMbps BandwidthSchedule.StartTime BandwidthSchedule.StopTime
----                      ----------------------   ---------------------------- --------------------------- --------------------------
Sunday, Tuesday, Saturday bandwidth-schedule       0                            11:00:00                    21:00:00

```

## PARAMETERS

### -Bandwidth
Bandwidth in Mbps

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Days
Scheduled Days

```yaml
Type: String[]
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
Type: IAzureContextContainer
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
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Resource Name

```yaml
Type: String
Parameter Sets: SetParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
Resource Group Name

```yaml
Type: String
Parameter Sets: SetParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceId
Azure ResourceId

```yaml
Type: String
Parameter Sets: ByResourceIdParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartTime
Schedule Start Time

```yaml
Type: String
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
Type: String
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
Type: SwitchParameter
Parameter Sets: (All)
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
