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

```
Get-AzDataBoxEdgeDeviceExtendedInfo -ResourceGroupName <String> -Name <String>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

## DESCRIPTION
The **Get-AzDataBoxEdgeDeviceEdtendedInfo** cmdlet gets extended information of a single device.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-AzDataBoxEdgeDeviceExtendedInfo -ResourceGroupName rgp-name -Name device-name

DataBoxEdgeDevice.Name                                : device-name
ResourceGroupName                                     : ranandu-name
DataBoxEdgeDeviceExtendedInfo.EncryptionKey           : {LONG_ENCRYPTION_KEY}
DataBoxEdgeDeviceExtendedInfo.EncryptionKeyThumbprint : {THUMBPRINT}
DataBoxEdgeDeviceExtendedInfo.ResourceKey             : {RESOURCEKEY}

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

### -Name
Device Name

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

### -ResourceGroupName
ResourceGroup Name

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

### Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeDevice

## NOTES

## RELATED LINKS
