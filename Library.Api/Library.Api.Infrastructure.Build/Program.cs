//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

var aDotNetClient = new ADotNetClient();

var githubPipeline = new GithubPipeline
{
    Name = "Library Build Pipeline",

    OnEvents = new Events
    {
        Push = new PushEvent
        {
            Branches = new string[] { "main" }
        },

        PullRequest = new PullRequestEvent
        {
            Branches = new string[] { "main" }
        }
    },

    Jobs = new Dictionary<string, Job>
    {
        {
            "build",
            new Job
            {
                RunsOn = BuildMachines.Windows2022,

                Steps = new List<GithubTask>
                {
                    new CheckoutTaskV2
                    {
                        Name = "Check out"
                    },

                    new SetupDotNetTaskV1
                    {
                        Name = "Setup .Net",

                        TargetDotNetVersion = new TargetDotNetVersion
                        {
                            DotNetVersion = "10.0.102",
                            IncludePrerelease = true
                        }
                    },

                    new RestoreTask
                    {
                        Name = "Restore"
                    },

                    new DotNetBuildTask
                    {
                        Name = "Build"
                    },

                    new TestTask
                    {
                        Name = "Test"
                    }
                }
            }
        }
    }
};

string? solutionRoot = null;

DirectoryInfo? current = new DirectoryInfo(Directory.GetCurrentDirectory());

while (current != null)
{
    if (current.GetFiles("*.sln").Any())
    {
        solutionRoot = current.FullName;
        break;
    }

    current = current.Parent;
}

if (solutionRoot is null)
{
    throw new InvalidOperationException("Solution root (.sln) not found.");
}

string buildScriptPath =
    Path.Combine(solutionRoot, ".github", "workflows", "dotnet.yml");

string directoryPath = Path.GetDirectoryName(buildScriptPath)!;

if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

aDotNetClient.SerializeAndWriteToFile(githubPipeline, buildScriptPath);

Console.WriteLine("YML CREATED AT:");
Console.WriteLine(buildScriptPath);
Console.ReadLine();
