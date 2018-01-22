using System;
using DMU.Math;

namespace HopefieldSimulator
{
    class Program
    {
        static void Main()
        {
            //   0     -1      -3
            //  -1      0       2
            //  -3      2       0
            var matrixFromTextExample = new Matrix(new double[,] { { 0, -1, -3 }, { -1, 0, 2 }, { -3, 2, 0 } });
            
            HopefieldNetwork network = new HopefieldNetwork(matrixFromTextExample);

            network.StudyAllVectorsSync();
            network.StudyAllVectorsAsync();

            OutputRenderer.CloseWriter();

            Console.ReadKey(true);
        }
    }
}
