#load nuget:?package=DevelopEngine.Cake


///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var projects = GetProjects(File("./src/IdEngine.sln"), configuration);
var artifacts = "./dist/";
var frameworks = new List<string> { "netstandard2.0" };


///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Publish")
	.WithCriteria(() => shouldPublish)
	.IsDependentOn("NuGet")
	.Does(() =>
{
	NuGetPush(GetFiles($"{artifacts}package/*.nupkg"), new NuGetPushSettings {
		Source = "https://api.nuget.org/v3/index.json",
		ApiKey = EnvironmentVariable("NUGET_API_KEY")
	});
});

Task("CI")
.IsDependentOn("Publish");

Task("Default")
.IsDependentOn("Post-Build")
.IsDependentOn("NuGet");

RunTarget(target);
