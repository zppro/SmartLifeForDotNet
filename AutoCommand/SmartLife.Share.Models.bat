
set PATH="%VS100COMNTOOLS%..\IDE\"
tf  @checkout.tfc "..\Solution-Bins\SmartLife.Share.Models.dll"
copy ..\Solution-Outputs\SmartLife.Share.Models.dll "..\Solution-Bins" 

tf  @checkin.tfc "..\Solution-Bins\SmartLife.Share.Models.dll" 

pause