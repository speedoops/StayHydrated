@set NET4_MSBUILD="C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe" /maxcpucount 

for /F %%a in ( 'dir *.sln /b /a-h /on' ) do ( 
  %NET4_MSBUILD% "%%a" /t:clean /p:Configuration=Release /p:platform="Any CPU"
)

pause