using DMU.Math;
using System;
using System.IO;
using System.Text;

namespace HopefieldSimulator
{
    public static class OutputRenderer
    {
        public static StreamWriter streamWriter { get; set; }

        static OutputRenderer()
        {
            streamWriter = new StreamWriter("HopefieldStudyResults.txt");
        }

        public static void CloseWriter()
        {
            streamWriter.Close();
        }

        public static void OutputModeHeader(string mode)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("///////////////////////////////////");
            stringBuilder.AppendLine(string.Format("Performing study in {0} mode", mode));
            stringBuilder.AppendLine("///////////////////////////////////");
            stringBuilder.AppendLine();

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);
        }

        public static void OutputStudyHeader(Matrix vector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Moving to following vector: ############################");

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);

            OutputVectorWithEmptyLine(vector);
        }

        public static void OutputCurrentStepHeader(int step)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Step nr {0} --------------", step));
            stringBuilder.AppendLine();

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);
        }

        public static void OutputInputPotentialAsync(double value, int position)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Input potential: ");
            switch(position)
            {
                case 0:
                    stringBuilder.AppendLine(value.ToString());
                    stringBuilder.AppendLine("nw");
                    stringBuilder.AppendLine("nw");
                    break;
                case 1:
                    stringBuilder.AppendLine("nw");
                    stringBuilder.AppendLine(value.ToString());
                    stringBuilder.AppendLine("nw");
                    break;
                case 2:
                    stringBuilder.AppendLine("nw");
                    stringBuilder.AppendLine("nw");
                    stringBuilder.AppendLine(value.ToString());
                    break;
                default: throw new Exception("Unexpected potential position");
            }
            stringBuilder.AppendLine();

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);
        }

        public static void OutputOutputPotential(Matrix outputPotentialVector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Output potential: ");

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);

            OutputVectorWithEmptyLine(outputPotentialVector);
        }

        public static void OutputEnergy(double value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Energy = {0}", value));
            stringBuilder.AppendLine();

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);
        }

        public static void OutputIterationStreak(int streak)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Equal streak: {0}", streak));
            if (streak >= 3)
                stringBuilder.AppendLine("--> Network become stable! <--");

            stringBuilder.AppendLine();

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);
        }

        public static void OutputResultVector(Matrix resultVector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Result vector (V2): "));

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);

            OutputVectorWithEmptyLine(resultVector);
        }

        public static void OutputVectorWithEmptyLine(Matrix vector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(vector.GetElement(0, 0).ToString());
            stringBuilder.AppendLine(vector.GetElement(1, 0).ToString());
            stringBuilder.AppendLine(vector.GetElement(2, 0).ToString());

            stringBuilder.AppendLine();

            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);
        }
    }
}
