
set PATH="%VS100COMNTOOLS%..\IDE\"
tf  @checkout.tfc "..\Solution-Bins\SmartLife.Share.Models.PO.dll"
copy ..\Solution-Outputs\SmartLife.Share.Models.PO.dll "..\Solution-Bins" 

tf  @checkin.tfc "..\Solution-Bins\SmartLife.Share.Models.PO.dll" 

pause