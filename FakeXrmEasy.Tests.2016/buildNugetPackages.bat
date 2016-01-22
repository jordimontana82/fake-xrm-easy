copy ..\FakeXrmEasy.2016\bin\Debug\FakeXrmEasy.dll .\build\lib\net452
cd build
nuget pack FakeXrmEasy.2016.dll.nuspec
nuget push FakeXrmEasy.2016.1.8.4.nupkg
pause