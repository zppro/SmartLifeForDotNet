
set PATH="%VS100COMNTOOLS%..\IDE\"
tf  @checkout.tfc "..\Solution-Bins\SmartLife.Share.Models.Auth.dll"
copy ..\Solution-Outputs\SmartLife.Share.Models.Auth.dll "..\Solution-Bins" 

tf  @checkin.tfc "..\Solution-Bins\SmartLife.Share.Models.Auth.dll" 

pause