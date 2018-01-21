using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMU.Math;

namespace HopefieldSimulator
{
    public class HopefieldNetwork
    {
        public List<double[]> AllVectors { get; set; }
        public Matrix Matrix { get; set; }

        public HopefieldNetwork(Matrix matrix)
        {
            AllVectors = new List<double[]>
            {
                new double[] { -1, -1, -1 },
                new double[] { -1, -1, 1 },
                new double[] { -1, 1, -1 },
                new double[] { -1, 1, 1 },
                new double[] { 1, -1, -1 },
                new double[] { 1, 1, -1 },
                new double[] { 1, -1, 1 },
                new double[] { 1, 1, 1 }
            };

            Matrix = matrix;
        }

        public void StudyAllVectorsSync()
        {
            foreach (double[] vector in AllVectors)
            {
                Console.WriteLine("");
                Console.WriteLine("Nowy wektor");

                var previousStepVector = new Matrix(vector, true);
                var currentStepVector = previousStepVector.Clone();

                const int maxSteps = 8; // 3 pow 2, because vector contains 3 bipolar values
                double previousStepEnergy = -99999;

                // Perform 8 steps
                for (int stepsDone = 0; stepsDone < maxSteps; stepsDone++)
                {

                    Console.WriteLine("Krok " + (stepsDone + 1) + "");


                    currentStepVector = Matrix.Multiply(Matrix, previousStepVector).ToBiPolar();

                    double energy = getEnergySyncOnly(previousStepVector, currentStepVector);

                    if(stepsDone > 0)
                    {
                        bool isStable = currentStepVector.Equals(previousStepVector);
                        bool energiesAreEqual = previousStepEnergy == energy;

                        if (isStable) Console.WriteLine("Siec się ustablizowała");
                        if (energiesAreEqual) Console.WriteLine("Oscylacja dwupunktowa");
                    }

                    previousStepEnergy = energy;
                    previousStepVector = currentStepVector;
                }
            }
        }

        public void StudyAllVectorsAsync()
        {
            throw new NotImplementedException();
        }

        private double getEnergySyncOnly(Matrix previousStepVector, Matrix currentStepVector)
        {
            double result = 0;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    result += Matrix.GetElement(i, j) * currentStepVector.GetElement(i, 0) * previousStepVector.GetElement(j, 0);

            return result * -1;
        }

        private double getEnergyAsyncOnly()
        {
            double result = 0;
            return result;
        }
    }
}
