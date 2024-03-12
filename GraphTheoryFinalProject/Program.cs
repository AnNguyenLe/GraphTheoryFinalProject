using GraphTheoryFinalProject.SolutionForProblems;

namespace GraphTheoryFinalProject;

class Program
{
    static void Main(string[] args)
    {
        // Cac file do thi nam trong folder: GraphTheoryFinalProject/GraphTheoryFinalProject/bin/Debug/net7.0/SampleGraphs/
        Console.WriteLine("\n============== Yeu cau 1: ===============\n");
        var FILEPATH_1WINDMILL = GetFilePath("SampleGraphs/problem_1_sample_windmill_graph.txt");
        SolutionForProblem1.Run(FILEPATH_1WINDMILL);

        var FILEPATH_1BARBELL = GetFilePath("SampleGraphs/problem_1_sample_barbell_graph.txt");
        SolutionForProblem1.Run(FILEPATH_1BARBELL);


        Console.WriteLine("\n============== Yeu cau 3: ===============\n");
        var FILEPATH_3 = GetFilePath("SampleGraphs/problem_3_sample_graph.txt");
        SolutionForProblem3.Run(FILEPATH_3);

        Console.WriteLine("\n============== Yeu cau 4: ===============\n");
        var FILEPATH_4 = GetFilePath("SampleGraphs/problem_4_sample_graph.txt");
        SolutionForProblem4.Run(FILEPATH_4);

        Console.WriteLine("\n============== Yeu cau 5: ===============\n");
        var FILEPATH_5A = GetFilePath("SampleGraphs/problem_5a_euler_circuit_sample_graph.txt");
        SolutionForProblem5.Run(FILEPATH_5A);

        var FILEPATH_5B = GetFilePath("SampleGraphs/problem_5b_none_euler_path_sample_graph.txt");
        SolutionForProblem5.Run(FILEPATH_5B);

        var FILEPATH_5C = GetFilePath("SampleGraphs/problem_5c_euler_path_sample_graph.txt");
        SolutionForProblem5.Run(FILEPATH_5C);

        Console.Read();
    }

    private static string GetFilePath(string relativePath)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), relativePath);
    }
}