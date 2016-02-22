copy ..\FakeXrmEasy.2015\bin\Debug\FakeXrmEasy.dll .\build\lib\net452
cd build
nuget pack FakeXrmEasy.2015.dll.nuspec
nuget push FakeXrmEasy.2015.1.9.1.nupkg
pause