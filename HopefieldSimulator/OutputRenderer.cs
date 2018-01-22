using DMU.Math;
using System;
using System.IO;
using System.Text;

namespace HopefieldSimulator
{
    public static class OutputRenderer
    {
        public static StreamWriter streamWriter { get; set; }

        private static int studyCounter = 1;

        static OutputRenderer()
        {
            streamWriter = new StreamWriter("HopefieldStudyResults.txt");
        }

        public static void CloseWriter()
        {
            streamWriter.Close();
        }

        public static void OutputInputMatrix3x3(Matrix matrix)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Input matrix 3x3:");
            for (int i = 0; i < 3; i++)
                stringBuilder.AppendLine(string.Format("{0}\t{1}\t{2}", 
                    matrix.GetElement(i, 0), 
                    matrix.GetElement(i, 1), 
                    matrix.GetElement(i, 2)));
            stringBuilder.AppendLine();

            PushToOutput(stringBuilder);
        }

        public static void OutputModeHeader(string mode)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("///////////////////////////////////");
            stringBuilder.AppendLine(string.Format("Performing study in {0} mode", mode));
            stringBuilder.AppendLine("///////////////////////////////////");
            stringBuilder.AppendLine();

            PushToOutput(stringBuilder);
        }

        public static void OutputStudyHeader(Matrix vector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("+++++++++++++++ Study nr: {0} +++++++++++++++++", studyCounter));
            stringBuilder.AppendLine("Moving to following vector: ############################");

            PushToOutput(stringBuilder);

            OutputVectorWithEmptyLine(vector);

            studyCounter++;
        }

        public static void OutputCurrentStepHeader(int step)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Step nr {0} --------------", step));
            stringBuilder.AppendLine();

            PushToOutput(stringBuilder);
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

            PushToOutput(stringBuilder);
        }

        public static void OutputInputPotentialSync(Matrix vector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Input potential: ");

            PushToOutput(stringBuilder);

            OutputVectorWithEmptyLine(vector);
        }

        public static void OutputOutputPotential(Matrix outputPotentialVector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Output potential: ");

            PushToOutput(stringBuilder);

            OutputVectorWithEmptyLine(outputPotentialVector);
        }

        public static void OutputEnergy(double value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Energy = {0}", value));
            stringBuilder.AppendLine();

            PushToOutput(stringBuilder);
        }

        public static void OutputIterationStreakAsync(int streak)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Equal streak: {0}", streak));
            if (streak >= 3)
                stringBuilder.AppendLine("--> Network become stable! <--");

            stringBuilder.AppendLine();

            PushToOutput(stringBuilder);
        }

        public static void OutputIterationStreakSync(Matrix resultVector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("--> Network become stable! <--");

            PushToOutput(stringBuilder);
            OutputVectorWithEmptyLine(resultVector);
        }

        public static void OutputResultVector(Matrix resultVector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Result vector (V2): "));

            PushToOutput(stringBuilder);

            OutputVectorWithEmptyLine(resultVector);
        }

        public static void OutputOscillationResult(Matrix V1, Matrix V2)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Reached 2-point oscillation!");
            stringBuilder.AppendLine("V1:");

            PushToOutput(stringBuilder);

            OutputVectorWithEmptyLine(V1);

            stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("V2:");

            PushToOutput(stringBuilder);

            OutputVectorWithEmptyLine(V2);
        }

        public static void OutputVectorWithEmptyLine(Matrix vector)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(vector.GetElement(0, 0).ToString());
            stringBuilder.AppendLine(vector.GetElement(1, 0).ToString());
            stringBuilder.AppendLine(vector.GetElement(2, 0).ToString());

            stringBuilder.AppendLine();

            PushToOutput(stringBuilder);
        }

        private static void PushToOutput(StringBuilder stringBuilder)
        {
            Console.WriteLine(stringBuilder);
            streamWriter.WriteLine(stringBuilder);
        }
    }
}
