:: Moving the .dll file into the Unity project directory (on compilation)

set src=C:\Users\msrladmin\Documents\GitHub\ART_v5_1\ART_Frameworks\Visual_Studio_Framework\VS_Framework_ART_5_1\Release\Unity_Plugin_ART_5_1.dll
::set dst=C:\Users\msrladmin\Documents\GitHub\ART_v5_1\ART_5_1\ART_Project_5_1\Assets\Plugins\Unity_Plugin_Right.dll
::set dst=C:\Users\msrladmin\Documents\GitHub\ART_v5_1\ART_5_1\ART_Project_5_1\Assets\Plugins\Unity_Plugin_Left.dll
::copy %src% %dst% /Y


:: Moving the .dll plugin for use with only one camera into the Unity project directory (on compilation)

set src=C:\Users\msrladmin\Documents\GitHub\ART_v5_1\ART_Frameworks\Visual_Studio_Framework\VS_Framework_ART_5_1\Release\Unity_Plugin_ART_SplitCam_5_1.dll
set dst=C:\Users\msrladmin\Documents\GitHub\ART_v5_1\ART_5_1\ART_Project_5_1\Assets\Plugins\Unity_Plugin_OneCam.dll
::copy %src% %dst% /Y

:: Move the Windows Form Application to the Unity Project (on compilation)

set src=C:\Users\msrladmin\Documents\GitHub\ART_v5_1\ART_Frameworks\Visual_Studio_Framework\VS_Framework_ART_5_1\ART_Application\bin\Release\ART_Application.exe
set dst=C:\Users\msrladmin\Documents\GitHub\ART_v5_1\ART_5_1\ART_Project_5_1\Assets\ART_Application.exe
copy %src% %dst% /Y