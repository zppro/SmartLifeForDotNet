
set PATH="%VS100COMNTOOLS%..\IDE\"
tf  @checkout.tfc "..\Solution-Bins\SmartLife.Share.Behaviors.dll"
copy ..\Solution-Outputs\SmartLife.Share.Behaviors.dll "..\Solution-Bins" 
copy ..\Solution-Outputs\SmartLife.Share.Behaviors.pdb "..\SmartLife.Applications\SmartLife.CertManage.ManageServices\bin" 
copy ..\Solution-Outputs\SmartLife.Share.Behaviors.pdb "..\SmartLife.Applications\SmartLife.OldCare.ManageServices\bin"

tf  @checkin.tfc "..\Solution-Bins\SmartLife.Share.Behaviors.dll" 

pause