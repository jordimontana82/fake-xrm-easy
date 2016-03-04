@echo off
cls
"tools\FAKE\tools\Fake.exe" build.fsx "%1"
exit /b %errorlevel%