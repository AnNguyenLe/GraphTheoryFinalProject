using GraphTheoryFinalProject.SolutionForProblems;

namespace GraphTheoryFinalProject;

class Program
{
    static void Main(string[] args)
    {
        // Cac file do thi nam trong folder: GraphTheoryFinalProject/GraphTheoryFinalProject/bin/Debug/net7.0/SampleGraphs/

        // ============== Yeu cau 3: ===============
        var FILEPATH_3 = GetFilePath("SampleGraphs/problem_3_sample_graph.txt");
        SolutionForProblem3.Run(FILEPATH_3);


        // ============== Yeu cau 4: ===============
        var FILEPATH_4 = GetFilePath("SampleGraphs/problem_4_sample_graph.txt");
        SolutionForProblem4.Run(FILEPATH_4);

        Console.Read();
    }

    private static string GetFilePath(string relativePath)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), relativePath);
    }
}