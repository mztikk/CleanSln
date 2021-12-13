// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using Microsoft.Build.Construction;

[DllImport("msvcrt.dll")]
static extern int system(string format);

if (args.Length == 0)
{
    Console.WriteLine("No solution file provided");
    return;
}

string sln = args[0];
if (string.IsNullOrEmpty(sln))
{
    Console.WriteLine("No solution file provided");
    return;
}

if (Path.GetExtension(sln) != ".sln")
{
    Console.WriteLine($"'{sln}' not a valid solution file");
    return;
}

SolutionFile solution = SolutionFile.Parse(sln);
foreach (SolutionConfigurationInSolution? config in solution.SolutionConfigurations)
{
    system($"msbuild -t:Clean -p:Configuration={config.ConfigurationName} -p:Platform={config.PlatformName} {sln}");
}
