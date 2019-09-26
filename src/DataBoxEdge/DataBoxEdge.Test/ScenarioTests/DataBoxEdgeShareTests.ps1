# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

function Get-StorageAccountCredentialName
{
    return getAssetName
}

function Get-ShareName
{
    return getAssetName
}



<#
.SYNOPSIS
Negative test. Get resources from an non-existing empty group.
#>
function Test-GetShareNonExistent
{	
    $rgname = Get-DeviceResourceGroupName
    $dfname = Get-DeviceName
	$sharename = Get-ShareName
	
    # Test
	Assert-ThrowsContains { Get-AzDataBoxEdgeShare $rgname $dfname $sharename  } "not find"    
}

<#
.SYNOPSIS
Tests Create New StorageAccountCredential
#>
function Test-CreateShare
{	
    $rgname = Get-DeviceResourceGroupName
    $dfname = Get-DeviceName
	$sharename = Get-ShareName
	$dataFormat = 'BlobBlob'


	$staname = Get-StorageAccountCredentialName
	$encryptionKey = Get-EncryptionKey
	$storageAccountType = 'GeneralPurposeStorage'
	$storageAccountSkuName = 'Standard_LRS'
	$storageAccountLocation = 'WestUS'
	$storageAccountSslStatus = 'Enabled'
	$storageAccount = New-AzStorageAccount $rgname $staname $storageAccountSkuName -Location $storageAccountLocation

	$storageAccountKeys = Get-AzStorageAccountKey $rgname $staname
	$storageAccountKey = $storageAccountKeys[0]
    $storageAccountCredential = New-AzDataBoxEdgeStorageAccountCredential $rgname $dfname $staname -StorageAccountType $storageAccountType -StorageAccountSslStatus $storageAccountSslStatus -StorageAccountAccessKey $storageAccountKey -EncryptionKey $encryptionKey
		
	# Test
	try
    {
        $expected = New-AzDataBoxEdgeShare $rgname $dfname $sharename $storageAccountCredential.Name -DataFormat  
		Assert-AreEqual $expected.Name $staname
		
    }
    finally
    {
		Remove-AzDataBoxEdgeStorageAccountCredential $rgname $dfname $staname
		Remove-AzStorageAccount $rgname $staname -Force
    }  
}
