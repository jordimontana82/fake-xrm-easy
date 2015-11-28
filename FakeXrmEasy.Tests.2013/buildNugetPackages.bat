copy ..\FakeXrmEasy.2013\bin\Debug\FakeXrmEasy.dll .\build\lib\net40
cd build
nuget pack FakeXrmEasy.2013.dll.nuspec
nuget push FakeXrmEasy.2013.1.7.0.nupkg
pause