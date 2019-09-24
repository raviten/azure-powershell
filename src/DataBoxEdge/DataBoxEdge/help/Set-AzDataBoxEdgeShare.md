---
external help file: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.dll-Help.xml
Module Name: Az.DataBoxEdge
online version:
schema: 2.0.0
---

# Set-AzDataBoxEdgeShare

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### SmbParameterSet (Default)
```
Set-AzDataBoxEdgeShare [-ResourceGroupName] <String> [-DeviceName] <String> [-Name] <String>
 -SetUserAccessRights <Hashtable> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### UpdateByResourceIdSmbParameterSet
```
Set-AzDataBoxEdgeShare -ResourceId <String> -SetUserAccessRights <Hashtable>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### UpdateByResourceIdNfsParameterSet
```
Set-AzDataBoxEdgeShare -ResourceId <String> -SetClientAccessRights <Hashtable>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### UpdateByInputObjectSmbParameterSet
```
Set-AzDataBoxEdgeShare -InputObject <PSDataBoxEdgeShare> -SetUserAccessRights <Hashtable>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### UpdateByInputObjectNfsParameterSet
```
Set-AzDataBoxEdgeShare -InputObject <PSDataBoxEdgeShare> -SetClientAccessRights <Hashtable>
 [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

### NfsParameterSet
```
Set-AzDataBoxEdgeShare [-ResourceGroupName] <String> [-DeviceName] <String> [-Name] <String>
 -SetClientAccessRights <Hashtable> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
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
Parameter Sets: SmbParameterSet, NfsParameterSet
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Input Object

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeShare
Parameter Sets: UpdateByInputObjectSmbParameterSet, UpdateByInputObjectNfsParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
Get Share with Resource name as Name

```yaml
Type: System.String
Parameter Sets: SmbParameterSet, NfsParameterSet
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
Share will be created under this ResourceGroupName

```yaml
Type: System.String
Parameter Sets: SmbParameterSet, NfsParameterSet
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
Parameter Sets: UpdateByResourceIdSmbParameterSet, UpdateByResourceIdNfsParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SetClientAccessRights
Read/Write Access for clientIps

```yaml
Type: System.Collections.Hashtable
Parameter Sets: UpdateByResourceIdNfsParameterSet, UpdateByInputObjectNfsParameterSet, NfsParameterSet
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SetUserAccessRights
provide access right along with existing usernames to access SMB Share types

```yaml
Type: System.Collections.Hashtable
Parameter Sets: SmbParameterSet, UpdateByResourceIdSmbParameterSet, UpdateByInputObjectSmbParameterSet
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
