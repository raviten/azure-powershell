---
external help file: Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.dll-Help.xml
Module Name: Az.DataBoxEdge
online version: https://docs.microsoft.com/en-us/powershell/module/az.databoxedge/get-azdataboxedgestorageaccount
schema: 2.0.0
---

# New-AzDataBoxEdgeStorageAccount

## SYNOPSIS
Creates a new storage account in the device

## SYNTAX

```
New-AzDataBoxEdgeStorageAccount [-ResourceGroupName] <String> [-DeviceName] <String> [-Name] <String>
 -StorageAccountCredentialName <String> [-AsJob] [-DefaultProfile <IAzureContextContainer>] [-Cloud] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
The command **New-AzDataBoxEdgeStorageAccount** is used to create a new EdgeStorageAccount in the device. 
For a device, each EdgeStorageAccount can be mapped at most to one cloud storage account only. 

## EXAMPLES

### Example 1
```powershell
PS C:\> New-AzDataBoxEdgeStorageAccount -ResourceGroupName rgpName -DeviceName db-edge -Name edgestoragegacount1 -StorageAccountCredentialName cloudstorageaccount1 -Cloud
Name                ContainerCount  CloudStorageAccountName BlobEndpoint                                                   DeviceName ResourceGroupName
----                --------------  ----------------------- ------------                                                   ---------- -----------------
edgestoragegacount1 0               cloudstorageaccount1    https://edgestoragegacount1.blob.db-edge.microsoftdatabox.com/ db-edge    rgpName          
```

### Example 2
```powershell
PS C:\> New-AzDataBoxEdgeStorageAccount -ResourceGroupName rgpName -DeviceName db-edge -Name edgestoragegacount2 -StorageAccountCredentialName cloudstorageaccount2

Name                ContainerCount  CloudStorageAccountName BlobEndpoint                                                   DeviceName ResourceGroupName
----                --------------  ----------------------- ------------                                                   ---------- -----------------
edgestoragegacount2 0               cloudstorageaccount2    https://edgestoragegacount2.blob.db-edge.microsoftdatabox.com/ db-edge    rgpName          
```

2 EdgeStorageAccounts on the device cannot share more than 1 Cloud Storage Account

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

### -Cloud
Will use a CloudStorageAccount for tiering

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
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
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Name
Resource Name

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
Resource Group Name

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
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
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### System.Management.Automation.SwitchParameter

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.DataBoxEdge.Models.PSDataBoxEdgeShare

## NOTES

## RELATED LINKS
