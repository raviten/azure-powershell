---
external help file: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.dll-Help.xml
Module Name: Az.DataBoxEdge
online version:
schema: 2.0.0
---

# New-AzDataBoxEdgeRole

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### ConnectionStringParameterSet (Default)
```
New-AzDataBoxEdgeRole -ResourceGroupName <String> -DeviceName <String> -Name <String> [-ConnectionString]
 -IOTDeviceConnectionString <String> -IOTEdgeDeviceConnectionString <String> -EncryptionKey <String>
 -Platform <String> -RoleStatus <String> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### IOTParameterSet
```
New-AzDataBoxEdgeRole -ResourceGroupName <String> -DeviceName <String> -Name <String> [-DeviceProperties]
 -IOTDeviceId <String> -IOTDeviceAccessKey <String> -IOTEdgeDeviceId <String> -IOTEdgeDeviceAccessKey <String>
 -IOTHostHub <String> -EncryptionKey <String> -Platform <String> -RoleStatus <String>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -ConnectionString
Use this to Provide Connection Strings

```yaml
Type: SwitchParameter
Parameter Sets: ConnectionStringParameterSet
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
{{ Fill DeviceName Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeviceProperties
Use this to Provide Device Properties

```yaml
Type: SwitchParameter
Parameter Sets: IOTParameterSet
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EncryptionKey
{{ Fill EncryptionKey Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IOTDeviceAccessKey
{{ Fill IOTDeviceAccessKey Description }}

```yaml
Type: String
Parameter Sets: IOTParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IOTDeviceConnectionString
{{ Fill IOTDeviceConnectionString Description }}

```yaml
Type: String
Parameter Sets: ConnectionStringParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IOTDeviceId
{{ Fill IOTDeviceId Description }}

```yaml
Type: String
Parameter Sets: IOTParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IOTEdgeDeviceAccessKey
{{ Fill IOTEdgeDeviceAccessKey Description }}

```yaml
Type: String
Parameter Sets: IOTParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IOTEdgeDeviceConnectionString
{{ Fill IOTEdgeDeviceConnectionString Description }}

```yaml
Type: String
Parameter Sets: ConnectionStringParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IOTEdgeDeviceId
{{ Fill IOTEdgeDeviceId Description }}

```yaml
Type: String
Parameter Sets: IOTParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IOTHostHub
{{ Fill IOTHostHub Description }}

```yaml
Type: String
Parameter Sets: IOTParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
{{ Fill Name Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Platform
{{ Fill Platform Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
{{ Fill ResourceGroupName Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RoleStatus
{{ Fill RoleStatus Description }}

```yaml
Type: String
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

### Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSStorageAccountCredential

## NOTES

## RELATED LINKS
