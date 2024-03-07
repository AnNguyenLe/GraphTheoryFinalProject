using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.SolutionForProblems;

namespace GraphTheoryFinalProject;

class Program
{
    static void Main(string[] args)
    {
        // ============== Yeu cau 3: ===============
        const string FILEPATH_3 = "/Users/thienannguyenle/Downloads/GraphTheoryFinalProject_Samples/connected_undirected_graph_3.txt";
        SolutionForProblem3.Run(FILEPATH_3);


        // ============== Yeu cau 4: ===============
        const string FILEPATH_4 = "/Users/thienannguyenle/Downloads/GraphTheoryFinalProject_Samples/problem_4_graph.txt";
        SolutionForProblem4.Run(FILEPATH_4);

        Console.Read();
    }
}