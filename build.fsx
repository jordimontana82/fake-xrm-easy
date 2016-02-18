#I @"tools\FAKE\tools\"
#r @"tools\FAKE\tools\FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile
open Fake.Git
open Fake.Testing.XUnit
open System.IO

let projectName           = "FakeXrmEasy"

//Directories
let buildDir              = @".\build"

let FakeXrmEasyBuildDir                    = buildDir + @"\FakeXrmEasy"
let FakeXrmEasy2013BuildDir                = buildDir + @"\FakeXrmEasy.2013"
let FakeXrmEasy2015BuildDir                = buildDir + @"\FakeXrmEasy.2015"
let FakeXrmEasy2016BuildDir                = buildDir + @"\FakeXrmEasy.2016"
let FakeXrmEasySharedBuildDir              = buildDir + @"\FakeXrmEasy.Shared"

let testDir              = @".\test"

let FakeXrmEasyTestsBuildDir               = testDir + @"\FakeXrmEasy.Tests"
let FakeXrmEasyTests2013BuildDir           = testDir + @"\FakeXrmEasy.Tests.2013"
let FakeXrmEasyTests2015BuildDir           = testDir + @"\FakeXrmEasy.Tests.2015"
let FakeXrmEasyTests2016BuildDir           = testDir + @"\FakeXrmEasy.Tests.2016"
let FakeXrmEasyTestsSharedBuildDir         = testDir + @"\FakeXrmEasy.Tests.Shared"

let deployDir               = @".\Publish"

let FakeXrmEasyDeployDir                    = deployDir + @"\FakeXrmEasy"
let FakeXrmEasy2013DeployDir                = deployDir + @"\FakeXrmEasy.2013"
let FakeXrmEasy2015DeployDir                = deployDir + @"\FakeXrmEasy.2015"
let FakeXrmEasy2016DeployDir                = deployDir + @"\FakeXrmEasy.2016"
let FakeXrmEasySharedDeployDir              = deployDir + @"\FakeXrmEasy.Shared"

let nugetDir                = @".\nuget\"
let nugetDeployDir          = @"[Enter_NuGet_Url]"
let packagesDir             = @".\packages\"

let mutable version         = "1.9"
let mutable build           = buildVersion
let mutable nugetVersion    = ""
let mutable asmVersion      = ""
let mutable asmInfoVersion  = ""
let mutable setupVersion    = ""

let gitbranch = Git.Information.getBranchName "."
let sha = Git.Information.getCurrentHash()

Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir]
)

Target "RestorePackages" (fun _ ->
   RestorePackages()
)

Target "BuildVersions" (fun _ ->

    let safeBuildNumber = if not isLocalBuild then build else "0"

    asmVersion      <- version + "." + safeBuildNumber
    asmInfoVersion  <- asmVersion + " - " + gitbranch + " - " + sha

    nugetVersion    <- version + "." + safeBuildNumber
    setupVersion    <- version + "." + safeBuildNumber

    match gitbranch with
        | "master" -> ()
        | "develop" -> (nugetVersion <- nugetVersion + " - " + "beta")
        | _ -> (nugetVersion <- nugetVersion + " - " + gitbranch)

    SetBuildNumber nugetVersion
)
Target "AssemblyInfo" (fun _ ->
    BulkReplaceAssemblyInfoVersions "src/" (fun f ->
                                              {f with
                                                  AssemblyVersion = asmVersion
                                                  AssemblyInformationalVersion = asmInfoVersion})
)

Target "BuildFakeXrmEasy" (fun _->
    !! @"FakeXrmEasy\*.csproj"
      |> MSBuildRelease FakeXrmEasyBuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.2013" (fun _->
    !! @"FakeXrmEasy.2013\*.csproj"
      |> MSBuildRelease FakeXrmEasy2013BuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.2015" (fun _->
    !! @"FakeXrmEasy.2015\*.csproj"
      |> MSBuildRelease FakeXrmEasy2015BuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.2016" (fun _->
    !! @"FakeXrmEasy.2016\*.csproj"
      |> MSBuildRelease FakeXrmEasy2016BuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Shared" (fun _->
    !! @"FakeXrmEasy.Shared\*.csproj"
      |> MSBuildRelease FakeXrmEasySharedBuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests" (fun _->
    !! @"FakeXrmEasy.Tests\*.csproj"
      |> MSBuildRelease FakeXrmEasyTestsBuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests.2013" (fun _->
    !! @"FakeXrmEasy.Tests.2013\*.csproj"
      |> MSBuildRelease FakeXrmEasyTests2013BuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests.2015" (fun _->
    !! @"FakeXrmEasy.Tests.2015\*.csproj"
      |> MSBuildRelease FakeXrmEasyTests2015BuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests.2016" (fun _->
    !! @"FakeXrmEasy.Tests.2016\*.csproj"
      |> MSBuildRelease FakeXrmEasyTests2016BuildDir "Build"
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests.Shared" (fun _->
    !! @"FakeXrmEasy.Tests.Shared\*.csproj"
      |> MSBuildRelease FakeXrmEasyTestsSharedBuildDir "Build"
      |> Log "Build - Output: "
)

Target "Test" (fun _ ->
    !! (testDir @@ "\FakeXrmEasy.Tests\FakeXrmEasy.Tests.dll")
      |> xUnit (fun p -> { p with HtmlOutputPath = Some (testDir @@ "xunit.html") })
)

Target "NuGet" (fun _ ->
    CreateDir(nugetDir)

    "FakeXrmEasy.2011.nuspec"
     |> NuGet (fun p -> 
           {p with               
               Version = version
               NoPackageAnalysis = true
               ToolPath = @".\tools\nuget\Nuget.exe"                             
               OutputPath = nugetDir })

    "FakeXrmEasy.2013.nuspec"
     |> NuGet (fun p -> 
           {p with               
               Version = version
               NoPackageAnalysis = true
               ToolPath = @".\tools\nuget\Nuget.exe"                             
               OutputPath = nugetDir })

    "FakeXrmEasy.2015.nuspec"
     |> NuGet (fun p -> 
           {p with               
               Version = version
               NoPackageAnalysis = true
               ToolPath = @".\tools\nuget\Nuget.exe"                             
               OutputPath = nugetDir })

    "FakeXrmEasy.2016.nuspec"
     |> NuGet (fun p -> 
           {p with               
               Version = version
               NoPackageAnalysis = true
               ToolPath = @".\tools\nuget\Nuget.exe"                             
               OutputPath = nugetDir })
)

Target "PublishNuGet" (fun _ ->

  let nugetPublishDir = (deployDir + "nuget")
  CreateDir nugetPublishDir

  !! (nugetDir + "*.nupkg") 
     |> Copy nugetPublishDir

  XCopy nugetPublishDir nugetDeployDir 
)

Target "Publish" (fun _ ->
    CreateDir deployDir

    CreateDir FakeXrmEasyDeployDir
    CreateDir FakeXrmEasy2013DeployDir
    CreateDir FakeXrmEasy2015DeployDir
    CreateDir FakeXrmEasy2016DeployDir

    !! (FakeXrmEasyBuildDir @@ @"/**/*.* ")
      -- " *.pdb"
        |> CopyTo FakeXrmEasyDeployDir
        
    !! (FakeXrmEasy2013BuildDir @@ @"/**/*.* ")
      -- " *.pdb"
        |> CopyTo FakeXrmEasy2013DeployDir

    !! (FakeXrmEasy2015BuildDir @@ @"/**/*.* ")
      -- " *.pdb"
        |> CopyTo FakeXrmEasy2015DeployDir

    !! (FakeXrmEasy2016BuildDir @@ @"/**/*.* ")
      -- " *.pdb"
        |> CopyTo FakeXrmEasy2016DeployDir
)

"Clean"
  ==> "RestorePackages"
  ==> "BuildVersions"
  =?> ("AssemblyInfo", not isLocalBuild )
  ==> "BuildFakeXrmEasy"
  ==> "BuildFakeXrmEasy.2013"
  ==> "BuildFakeXrmEasy.2015"
  ==> "BuildFakeXrmEasy.2016"
  ==> "BuildFakeXrmEasy.Shared"
  ==> "BuildFakeXrmEasy.Tests"
  ==> "BuildFakeXrmEasy.Tests.2013"
  ==> "BuildFakeXrmEasy.Tests.2015"
  ==> "BuildFakeXrmEasy.Tests.2016"
  ==> "BuildFakeXrmEasy.Tests.Shared"
  ==> "Test"
  ==> "Publish"
  ==> "NuGet"
  ==> "PublishNuGet"
  
RunTargetOrDefault "NuGet"