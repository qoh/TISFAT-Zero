@echo off
rem GenFragments - 32.bat
rem Version 1.0.0.25

rem Latest template version 1.0.0.0
cd Fragments
set Fragments=%CD%

for /f "delims=" %%x in (config_Batch32.cfg) do (set "%%x")

cd %BinDir%
set InstallFiles="%CD%"
set File="%Fragments%\FilesFragment.wxs"

cd %Fragments%\..
cd %TemplateDir%
set TemplateDir=%CD%

set Template="%Fragments%\FragmentTemplate - 32.xslt"
cd %BinDir%
set CopyPath="..\..\..\%ProjectName%\bin\Release\"

echo ************************************
echo *  GenFragments - Deleting .pdb's  *
echo ************************************
del /s /q /f %InstallFiles%\*.pdb 2>NUL
echo Lingering .pdb's have been removed.
echo.

echo ****************************************************
echo *  GenFragments - Adding dlls from child projects  *
echo ****************************************************
echo Only projects that have a 'bin/Release' folder are listed
echo Skipping DLLs which already exist in the target project
echo.

cd %InstallFiles%
cd ../../..

for /d %%d in (*) do (
    if not "%%d" == "%ProjectName%" (
	    if exist "%%d\bin\Release" (
	    	echo Found project %%d
	    	cd "%%d\bin\Release"

	        for %%f in (*.dll) do (
	            if exist "..\..\..\%ProjectName%\bin\Release\%%f" (
	            	rem echo  -Skipping %%f
	            )

	            if not exist "..\..\..\%ProjectName%\bin\Release\%%f" (
	            	echo  -Copying %%f
	                copy "%%f" %CopyPath% 1>NUL 2>NUL
	            )
	        )
	        cd ..\..\..
	    )
	)
)

echo.


echo ************************************
echo * GenFragments - Variable Display  *
echo ************************************
echo.
echo Fragments Folder: %Fragments%
echo Bin Folder: %InstallFiles%
echo.
echo Component Group: %CmpGroup%
echo Template: %Template%
echo Output File: %File%
echo.
echo Heat Command: C:\Program Files (x86)\WiX Toolset v3.10\bin\heat.exe dir %InstallFiles% -cg %CmpGroup% -dr INSTALLFOLDER -gg -scom -sreg -sfrag -srd -t %Template% -var var.BinDirectory InstallFiles -out %File%
echo.

echo ************************************
echo * GenFragments - Copying Config    *
echo ************************************
echo.
copy "%Fragments%\config_Template.xml" %TemplateDir%
echo.

echo ************************************
echo * GenFragments - Heat output below *
echo ************************************
echo.
"C:\Program Files (x86)\WiX Toolset v3.10\bin\heat.exe" dir %InstallFiles% -cg %CmpGroup% -dr INSTALLFOLDER -gg -scom -sreg -sfrag -srd -t %Template% -var var.BinDirectory InstallFiles -out %File%
pause
cd.
cd.