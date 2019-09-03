---
external help file: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.dll-Help.xml
Module Name: Az.DataBoxEdge
online version:
schema: 2.0.0
---

# Get-AzDataBoxEdgeBandwidthSchedule

## SYNOPSIS
Gets the information about Bandwidth schedules

## SYNTAX

### ListParameterSet (Default)
```
Get-AzDataBoxEdgeBandwidthSchedule -ResourceGroupName <String> -DeviceName <String>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### GetByNameParameterSet
```
Get-AzDataBoxEdgeBandwidthSchedule -ResourceGroupName <String> -Name <String> -DeviceName <String>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

## DESCRIPTION
The **Get-AzDataBoxEdgeBandwidthSchedule** cmdlet gets information about Bandwidth Schedules.
If you specify the Name of the Schedule along with the resource group name and Device name, this cmdlet gets information about that specific Bandwidth schedule


## EXAMPLES

### Example 1
```powershell
PS C:\>Get-AzDataBoxEdgeBandwidthSchedule -ResourceGroupname resource-group-name -DeviceName device-name

BandwidthSchedule.Name                        BandwidthSchedule.RateInMbps BandwidthSchedule.StartTime BandwidthSchedule.StopTime BandwidthSchedule.Days 
----------------------                        ---------------------------- --------------------------- ---------------------- ---------------------- 
Schedule-0fc25d5d-2a14-466b-b80e-aabf2f7850e5 0                            00:00:00                    23:59:00               Tuesday
Schedule-2c3b06c5-9da6-4e14-b554-80eabd530a8e 50                           00:01:00                    05:00:00               Monday
```

## PARAMETERS

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
The name of the device

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Name of the BandwidthSchedule

```yaml
Type: System.String
Parameter Sets: GetByNameParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
Name and Device belongs to this ResourceGroup

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
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
