
set PATH="%VS100COMNTOOLS%..\IDE\"
tf  @checkout.tfc "..\Solution-Bins\SmartLife.Client.PensionAgency.Models.dll"
copy ..\Solution-Outputs\SmartLife.Client.PensionAgency.Models.dll "..\Solution-Bins" 

tf  @checkin.tfc "..\Solution-Bins\SmartLife.Client.PensionAgency.Models.dll" 

pause