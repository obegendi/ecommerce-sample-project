///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

Task("Restore-NuGet-Packages")
    .Does(() =>
{
	DotNetCoreRestore(new DotNetCoreRestoreSettings
	{
		NoCache = true,
		Verbosity = DotNetCoreVerbosity.Normal
	});
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    DotNetCoreBuild("./EcommerceSample.sln");
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);