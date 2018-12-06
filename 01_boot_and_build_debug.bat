@echo off
set FAKE_NO_LEGACY_WARNING=true
cls
call 00_boot.bat
call 02_build.bat %1
pause