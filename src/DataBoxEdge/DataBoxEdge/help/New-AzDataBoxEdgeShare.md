---
external help file: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.dll-Help.xml
Module Name: Az.DataBoxEdge
online version:
schema: 2.0.0
---

# New-AzDataBoxEdgeShare

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### NewParameterSet (Default)
```
New-AzDataBoxEdgeShare -ResourceGroupName <String> -DeviceName <String> -StorageAccountCredentialName <String>
 -Name <String> -DataFormat <String> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### NFSParameterSet
```
New-AzDataBoxEdgeShare -ResourceGroupName <String> -DeviceName <String> -StorageAccountCredentialName <String>
 -Name <String> [-SetClient] -ClientId <String> -ClientAccessRight <String> -DataFormat <String>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### SMBParameterSet
```
New-AzDataBoxEdgeShare -ResourceGroupName <String> -DeviceName <String> -StorageAccountCredentialName <String>
 -Name <String> [-AccessProtocol <String>] [-SetUser] -Username <String> -UserAccessRight <String>
 -DataFormat <String> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
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

### -AccessProtocol
will use this AccessProtocol in the case of creating Share

```yaml
Type: System.String
Parameter Sets: SMBParameterSet
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientAccessRight
Provide Read/Write Access for clientId

```yaml
Type: System.String
Parameter Sets: NFSParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientId
Provide ClientId for the NFS

```yaml
Type: System.String
Parameter Sets: NFSParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DataFormat
Set Data Format ex: PageBlob, BlobBlob

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
{{ Fill DeviceName Description }}

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
Creates a share with Name with Share Access protocol as NFS and

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
Share will be created under this ResourceGroupName

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

### -SetClient
Add ClientId in case of NFS Access Protocol

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: NFSParameterSet
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SetUser
Attach user in case of SMB Access Protocol

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: SMBParameterSet
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StorageAccountCredentialName
Provide existing StorageAccountCredential's Resource Name

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

### -UserAccessRight
Provide user access right for the Username

```yaml
Type: System.String
Parameter Sets: SMBParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Username
provide an existing username for SMB Share types

```yaml
Type: System.String
Parameter Sets: SMBParameterSet
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

### Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeShare

## NOTES

## RELATED LINKS
