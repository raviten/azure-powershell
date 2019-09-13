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

function Get-BandwidthScheduleName
{
    return getAssetName
}

<#
.SYNOPSIS
Negative test. Get resources from an non-existing empty group.
#>
function Test-GetNonExistingBandwidthSchedule
{	
    $dfname = Get-DeviceName
    $rgname = Get-DeviceResourceGroupName
	$bwname = Get-BandwidthScheduleName
	
    
    # Test
	Assert-ThrowsContains { Get-AzDataBoxEdgeBandwidthSchedule -ResourceGroupName $rgname -DeviceName $dfname -Name $bwname } "not found"    

}


<#
.SYNOPSIS
Negative test. Get resources from an non-existing empty group.
#>
function Test-CreateBandwidthSchedule
{	
    $rgname = Get-DeviceResourceGroupName
    $dfname = Get-DeviceName
	$bwname = Get-BandwidthScheduleName
	$bwRateInMbps = 45
	$bwStartTime = 11:00:00
	$bwEndTime = 13:00:00
	$bwDaysOfWeek = Sunday,Saturday

    # Test
	try
    {
        $expected = New-AzDataBoxEdgeBandwidthSchedule $rgname $dfname $bwname -DaysOfWeek $bwDaysOfWeek -StartTime $bwStartTime -StopTime $bwStopTime -Bandwidth $bwRateInMbps
		Assert-AreEqual $expected.Name $bwname
    }
    finally
    {
		Remove-AzDataBoxEdgeBandwidthSchedule $rgname $dfname $bwname
    }  
    
	Assert-ThrowsContains { Get-AzDataBoxEdgeBandwidthSchedule -ResourceGroupName $rgname -DeviceName $dfname -Name $bwname } "not found"    
}
