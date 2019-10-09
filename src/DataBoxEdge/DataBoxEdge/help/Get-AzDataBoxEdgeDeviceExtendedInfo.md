---
external help file: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.dll-Help.xml
Module Name: Az.DataBoxEdge
online version:
schema: 2.0.0
---

# Get-AzDataBoxEdgeDeviceExtendedInfo

## SYNOPSIS
Get the extended info for a device

## SYNTAX

### GetByNameParameterSet (Default)
```
Get-AzDataBoxEdgeDeviceExtendedInfo [-ResourceGroupName] <String> [-Name] <String>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### GetByResourceIdParameterSet
```
Get-AzDataBoxEdgeDeviceExtendedInfo -ResourceId <String> [-DefaultProfile <IAzureContextContainer>]
 [<CommonParameters>]
```

### GetByInputObjectSet
```
Get-AzDataBoxEdgeDeviceExtendedInfo -InputObject <PSDataBoxEdgeDeviceExtendedInfo>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### GetByParentObjectParameterSet
```
Get-AzDataBoxEdgeDeviceExtendedInfo [-DefaultProfile <IAzureContextContainer>]
 -DeviceObject <PSDataBoxEdgeDevice> [<CommonParameters>]
```

## DESCRIPTION
The **Get-AzDataBoxEdgeDeviceEdtendedInfo** cmdlet gets extended information of a single device.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-AzDataBoxEdgeDeviceExtendedInfo -ResourceGroupName rgp-name -Name device-name
DeviceName EncryptionKey
---------- -------------
dbEdge     EAAAAJdnau2XC0QtRGj6DP86ItYz52MjnynWY07yg13VH5FStmUa/4tQxlmJuaFLF6BY5qHIy0hnTvdJbWCHzr7g6xufw48yNFE4uYhlanI3N6SRKd2vrp15Tu3N0K4/e9zxheo35ITEkJDjvobgI9zxM3+IZYA7srD4NvwNuo1CnFA5eM/yLm1LCTZcaSxA0G0O9vb3�
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

### -DeviceObject
Please provide corresponding device object

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeDevice
Parameter Sets: GetByParentObjectParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -InputObject
Provide Corresponding Input Object

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeDeviceExtendedInfo
Parameter Sets: GetByInputObjectSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
Device Name

```yaml
Type: System.String
Parameter Sets: GetByNameParameterSet
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
ResourceGroup Name

```yaml
Type: System.String
Parameter Sets: GetByNameParameterSet
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
Parameter Sets: GetByResourceIdParameterSet
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

### Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeDevice

## NOTES

## RELATED LINKS
