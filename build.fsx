#I @"tools\FAKE\tools\"
#r @"tools\FAKE\tools\FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile
open Fake.Git
open Fake.Testing.XUnit2
open System.IO
open Fake.OpenCoverHelper
open Fake.ReportGeneratorHelper
open Fake.FileHelper

let projectName           = "FakeXrmEasy"

//Directories
let buildDir              = @".\build"

let FakeXrmEasyBuildDir                    = buildDir + @"\FakeXrmEasy"
let FakeXrmEasy2013BuildDir                = buildDir + @"\FakeXrmEasy.2013"
let FakeXrmEasy2015BuildDir                = buildDir + @"\FakeXrmEasy.2015"
let FakeXrmEasy2016BuildDir                = buildDir + @"\FakeXrmEasy.2016"
let FakeXrmEasy365BuildDir                 = buildDir + @"\FakeXrmEasy.365"
let FakeXrmEasySharedBuildDir              = buildDir + @"\FakeXrmEasy.Shared"

let testDir              = @".\test"

let FakeXrmEasyTestsBuildDir               = testDir + @"\FakeXrmEasy.Tests"
let FakeXrmEasyTests2013BuildDir           = testDir + @"\FakeXrmEasy.Tests.2013"
let FakeXrmEasyTests2015BuildDir           = testDir + @"\FakeXrmEasy.Tests.2015"
let FakeXrmEasyTests2016BuildDir           = testDir + @"\FakeXrmEasy.Tests.2016"
let FakeXrmEasyTests365BuildDir            = testDir + @"\FakeXrmEasy.Tests.365"
let FakeXrmEasyTestsSharedBuildDir         = testDir + @"\FakeXrmEasy.Tests.Shared"

let deployDir               = @".\Publish"

let FakeXrmEasyDeployDir                    = deployDir + @"\FakeXrmEasy"
let FakeXrmEasy2013DeployDir                = deployDir + @"\FakeXrmEasy.2013"
let FakeXrmEasy2015DeployDir                = deployDir + @"\FakeXrmEasy.2015"
let FakeXrmEasy2016DeployDir                = deployDir + @"\FakeXrmEasy.2016"
let FakeXrmEasy365DeployDir                 = deployDir + @"\FakeXrmEasy.365"
let FakeXrmEasySharedDeployDir              = deployDir + @"\FakeXrmEasy.Shared"

let nugetDir                = @".\nuget\"
let nugetDeployDir          = @"[Enter_NuGet_Url]"
let packagesDir             = @".\packages\"

let nuGetCommandLine           = @".\tools\nuget\nuget286.exe"
let mutable previousVersion = "1.22.3"
let mutable version         = "1.23.0" //Copy this into previousVersion before publishing packages...
let mutable build           = buildVersion
let mutable nugetVersion    = version
let mutable asmVersion      = version
let mutable asmInfoVersion  = version
let mutable setupVersion    = ""

let mutable releaseNotes    = "https://github.com/jordimontana82/fake-xrm-easy/compare/v" + previousVersion + "...v" + version

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
    BulkReplaceAssemblyInfoVersions "." (fun f ->
                                              {f with
                                                  AssemblyVersion = asmVersion
                                                  AssemblyFileVersion = asmVersion
                                                  AssemblyInformationalVersion = asmInfoVersion})
)

Target "BuildFakeXrmEasy" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY") ]
    !! @"FakeXrmEasy\*.csproj"
      |> MSBuild FakeXrmEasyBuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.2013" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY_2013") ]
    !! @"FakeXrmEasy.2013\*.csproj"
      |> MSBuild FakeXrmEasy2013BuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.2015" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY_2015") ]
    !! @"FakeXrmEasy.2015\*.csproj"
      |> MSBuild FakeXrmEasy2015BuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.2016" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY_2016") ]
    !! @"FakeXrmEasy.2016\*.csproj"
      |> MSBuild FakeXrmEasy2016BuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.365" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY_365") ]
    !! @"FakeXrmEasy.365\*.csproj"
      |> MSBuild FakeXrmEasy365BuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests" (fun _->
    let properties =
        [ ("DefineConstants", "") ]
    !! @"FakeXrmEasy.Tests\*.csproj"
      |> MSBuild FakeXrmEasyTestsBuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests.2013" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY_2013") ]
    !! @"FakeXrmEasy.Tests.2013\*.csproj"
      |> MSBuild FakeXrmEasyTests2013BuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests.2015" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY_2015") ]
    !! @"FakeXrmEasy.Tests.2015\*.csproj"
      |> MSBuild FakeXrmEasyTests2015BuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests.2016" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY_2016") ]
    !! @"FakeXrmEasy.Tests.2016\*.csproj"
      |> MSBuild FakeXrmEasyTests2016BuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "BuildFakeXrmEasy.Tests.365" (fun _->
    let properties =
        [ ("DefineConstants", "FAKE_XRM_EASY_365") ]
    !! @"FakeXrmEasy.Tests.365\*.csproj"
      |> MSBuild FakeXrmEasyTests365BuildDir "Rebuild" (properties)
      |> Log "Build - Output: "
)

Target "Test.2011" (fun _ ->
    !! (testDir @@ "\FakeXrmEasy.Tests\FakeXrmEasy.Tests.dll")
      |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (testDir @@ "xunit.2011.html") })
)

Target "Test.2013" (fun _ ->
    !! (testDir @@ "\FakeXrmEasy.Tests.2013\FakeXrmEasy.Tests.2013.dll")
      |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (testDir @@ "xunit.2013.html") })
)

Target "Test.2015" (fun _ ->
    !! (testDir @@ "\FakeXrmEasy.Tests.2015\FakeXrmEasy.Tests.2015.dll")
      |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (testDir @@ "xunit.2015.html") })
)

Target "Test.2016" (fun _ ->
    !! (testDir @@ "\FakeXrmEasy.Tests.2016\FakeXrmEasy.Tests.2016.dll")
      |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (testDir @@ "xunit.2016.html") })
)

Target "Test.365" (fun _ ->
    !! (testDir @@ "\FakeXrmEasy.Tests.365\FakeXrmEasy.Tests.365.dll")
      |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (testDir @@ "xunit.365.html") })
)

Target "NuGet" (fun _ ->
    CreateDir(nugetDir)

    "FakeXrmEasy.2011.nuspec"
     |> NuGet (fun p -> 
           {p with  
               Project = "FakeXrmEasy"           
               Version = version
               NoPackageAnalysis = true
               ToolPath = nuGetCommandLine                            
               OutputPath = nugetDir
               ReleaseNotes = releaseNotes
               Publish = true })

    "FakeXrmEasy.2013.nuspec"
     |> NuGet (fun p -> 
           {p with 
               Project = "FakeXrmEasy.2013"                  
               Version = version
               NoPackageAnalysis = true
               ToolPath = nuGetCommandLine                            
               OutputPath = nugetDir
               ReleaseNotes = releaseNotes
               Publish = true })

    "FakeXrmEasy.2015.nuspec"
     |> NuGet (fun p -> 
           {p with
               Project = "FakeXrmEasy.2015"                   
               Version = version
               NoPackageAnalysis = true
               ToolPath = nuGetCommandLine                            
               OutputPath = nugetDir
               ReleaseNotes = releaseNotes
               Publish = true })

    "FakeXrmEasy.2016.nuspec"
     |> NuGet (fun p -> 
           {p with     
               Project = "FakeXrmEasy.2016"              
               Version = version
               NoPackageAnalysis = true
               ToolPath = nuGetCommandLine                            
               OutputPath = nugetDir
               ReleaseNotes = releaseNotes
               Publish = true })

    "FakeXrmEasy.365.nuspec"
     |> NuGet (fun p -> 
           {p with     
               Project = "FakeXrmEasy.365"              
               Version = version
               NoPackageAnalysis = true
               ToolPath = nuGetCommandLine                            
               OutputPath = nugetDir
               ReleaseNotes = releaseNotes
               Publish = true })
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
    CreateDir FakeXrmEasy365DeployDir

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

    !! (FakeXrmEasy365BuildDir @@ @"/**/*.* ")
      -- " *.pdb"
        |> CopyTo FakeXrmEasy365DeployDir
)

Target "CodeCoverage.2011" (fun _ ->
    OpenCover (fun p -> { p with 
                                TestRunnerExePath = "./packages/xunit.runner.console.2.1.0/tools/xunit.console.exe"
                                ExePath = "./packages/OpenCover.4.6.519/tools/OpenCover.Console"
                                Register = RegisterType.RegisterUser
                                WorkingDir = (testDir @@ "\FakeXrmEasy.Tests")
                                Filter = "+[FakeXrmEasy*]* -[*.Tests*]*"
                                Output = "../coverage.2011.xml"
                        }) "FakeXrmEasy.Tests.dll"
    
)

Target "CodeCoverage.2013" (fun _ ->
    OpenCover (fun p -> { p with 
                                TestRunnerExePath = "./packages/xunit.runner.console.2.1.0/tools/xunit.console.exe"
                                ExePath = "./packages/OpenCover.4.6.519/tools/OpenCover.Console"
                                Register = RegisterType.RegisterUser
                                WorkingDir = (testDir @@ "\FakeXrmEasy.Tests.2013")
                                Filter = "+[FakeXrmEasy*]* -[*.Tests*]*"
                                Output = "../coverage.2013.xml"
                        }) "FakeXrmEasy.Tests.2013.dll"
    
)

Target "CodeCoverage.2015" (fun _ ->
    OpenCover (fun p -> { p with 
                                TestRunnerExePath = "./packages/xunit.runner.console.2.1.0/tools/xunit.console.exe"
                                ExePath = "./packages/OpenCover.4.6.519/tools/OpenCover.Console"
                                Register = RegisterType.RegisterUser
                                WorkingDir = (testDir @@ "\FakeXrmEasy.Tests.2015")
                                Filter = "+[FakeXrmEasy*]* -[*.Tests*]*"
                                Output = "../coverage.2015.xml"
                        }) "FakeXrmEasy.Tests.2015.dll"
    
)

Target "CodeCoverage.2016" (fun _ ->
    OpenCover (fun p -> { p with 
                                TestRunnerExePath = "./packages/xunit.runner.console.2.1.0/tools/xunit.console.exe"
                                ExePath = "./packages/OpenCover.4.6.519/tools/OpenCover.Console"
                                Register = RegisterType.RegisterUser
                                WorkingDir = (testDir @@ "\FakeXrmEasy.Tests.2016")
                                Filter = "+[FakeXrmEasy*]* -[*.Tests*]*"
                                Output = "../coverage.2016.xml"
                        }) "FakeXrmEasy.Tests.2016.dll"
    
)

Target "CodeCoverage.365" (fun _ ->
    OpenCover (fun p -> { p with 
                                TestRunnerExePath = "./packages/xunit.runner.console.2.1.0/tools/xunit.console.exe"
                                ExePath = "./packages/OpenCover.4.6.519/tools/OpenCover.Console"
                                Register = RegisterType.RegisterUser
                                WorkingDir = (testDir @@ "\FakeXrmEasy.Tests.365")
                                Filter = "+[FakeXrmEasy*]* -[*.Tests*]*"
                                Output = "../coverage.365.xml"
                        }) "FakeXrmEasy.Tests.365.dll"
    
)

Target "ReportCodeCoverage" (fun _ ->
    ReportGenerator (fun p -> { p with 
                                    ExePath = "./packages/ReportGenerator.2.4.5.0/tools/ReportGenerator"
                                    WorkingDir = (testDir @@ "\FakeXrmEasy.Tests")
                                    TargetDir = "../reports"
                                    ReportTypes = [ReportGeneratorReportType.Html; ReportGeneratorReportType.Badges ]
                               }) [ "..\coverage.2011.xml";  "..\coverage.2013.xml";  "..\coverage.2015.xml";  "..\coverage.2016.xml"; "..\coverage.365.xml" ]
    
)

Target "ReplaceVersion" (fun _ ->
    RegexReplaceInFileWithEncoding previousVersion version System.Text.Encoding.UTF8 "./Install-scripts/365/Install.ps1"
    RegexReplaceInFileWithEncoding previousVersion version System.Text.Encoding.UTF8 "./Install-scripts/2011/Install.ps1"
    RegexReplaceInFileWithEncoding previousVersion version System.Text.Encoding.UTF8 "./Install-scripts/2013/Install.ps1"
    RegexReplaceInFileWithEncoding previousVersion version System.Text.Encoding.UTF8 "./Install-scripts/2015/Install.ps1"
    RegexReplaceInFileWithEncoding previousVersion version System.Text.Encoding.UTF8 "./Install-scripts/2016/Install.ps1"
    RegexReplaceInFileWithEncoding previousVersion version System.Text.Encoding.UTF8 "README.md"
)

"Clean"
  ==> "RestorePackages"
  ==> "BuildVersions"
  ==> "AssemblyInfo"
  ==> "BuildFakeXrmEasy"
  ==> "BuildFakeXrmEasy.2013"
  ==> "BuildFakeXrmEasy.2015"
  ==> "BuildFakeXrmEasy.2016"
  ==> "BuildFakeXrmEasy.365"
  ==> "BuildFakeXrmEasy.Tests"
  ==> "BuildFakeXrmEasy.Tests.2013"
  ==> "BuildFakeXrmEasy.Tests.2015"
  ==> "BuildFakeXrmEasy.Tests.2016"
  ==> "BuildFakeXrmEasy.Tests.365"
  ==> "Test.2011"
  ==> "Test.2013"
  ==> "Test.2015"
  ==> "Test.2016"
  ==> "Test.365"
  ==> "CodeCoverage.2011"
  ==> "CodeCoverage.2013"
  ==> "CodeCoverage.2015"
  ==> "CodeCoverage.2016"
  ==> "CodeCoverage.365"
  ==> "ReportCodeCoverage"
  ==> "ReplaceVersion"
  ==> "Publish"
  ==> "NuGet"
  ==> "PublishNuGet"
  
RunTargetOrDefault "NuGet"