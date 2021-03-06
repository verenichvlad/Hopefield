﻿using System;
using System.Collections.Generic;
using DMU.Math;

namespace HopefieldSimulator
{
    public class HopefieldNetwork
    {
        public List<Matrix> AllVectors { get; set; }
        public Matrix Matrix { get; set; }

        // 2 pow n, n = 3 (n representing bipolar value/neuron with possible values {-1, 1})
        const int maxStepsSync = 8;
        // n(2 pow n), for async mode
        const int maxStepsAsync = 24; 

        public HopefieldNetwork(Matrix matrix)
        {     
            AllVectors = new List<Matrix>
            {
                new Matrix(new double[] { -1, -1, -1 }, true),
                new Matrix(new double[] { -1, -1, 1 }, true),
                new Matrix(new double[] { -1, 1, -1 }, true),
                new Matrix(new double[] { -1, 1, 1 }, true),
                new Matrix(new double[] { 1, -1, -1 }, true),
                new Matrix(new double[] { 1, 1, -1 }, true),
                new Matrix(new double[] { 1, -1, 1 }, true),
                new Matrix(new double[] { 1, 1, 1 }, true)
            };

            Matrix = matrix;
        }

        public void StudyAllVectorsSync()
        {
            OutputRenderer.OutputModeHeader("synchronous");

            foreach (Matrix studiedVector in AllVectors)
            {
                OutputRenderer.OutputStudyHeader(studiedVector);

                var previousStepOutputPotential = studiedVector.Clone();
                double previousStepEnergy = -99999;

                var outputPotential = studiedVector.Clone();

                // Perform study steps
                for (int currentStepN = 1; currentStepN <= maxStepsSync; currentStepN++)
                {
                    Matrix inputPotential = Matrix.Multiply(Matrix, previousStepOutputPotential);
                    outputPotential = inputPotential.ToBiPolar();

                    double energy = getEnergySyncOnly(previousStepOutputPotential, outputPotential);

                    OutputRenderer.OutputCurrentStepHeader(currentStepN);
                    OutputRenderer.OutputInputPotentialSync(inputPotential);
                    OutputRenderer.OutputOutputPotential(outputPotential);
                    OutputRenderer.OutputEnergy(energy);
                    if (currentStepN > 1)
                    {
                        bool isStable = outputPotential.Equals(previousStepOutputPotential);
                        bool energiesAreEqual = previousStepEnergy == energy;

                        if (isStable)
                            OutputRenderer.OutputIterationStreakSync(outputPotential);
                        if (!isStable && energiesAreEqual)
                            OutputRenderer.OutputOscillationResult(previousStepOutputPotential, outputPotential);

                        if (isStable || energiesAreEqual) break;
                    }

                    previousStepEnergy = energy;
                    previousStepOutputPotential = outputPotential;
                }
            }
        }

        public void StudyAllVectorsAsync()
        {
            OutputRenderer.OutputModeHeader("asynchronous");
            foreach (Matrix studiedVector in AllVectors)
            {
                OutputRenderer.OutputStudyHeader(studiedVector);

                int stableIterationsStreak = 0;
                int studiedNeuronPosition = 0;
                Matrix previousOutputPotentialVector = studiedVector;

                // Perform study steps
                for (int currentStepN = 1; currentStepN < maxStepsAsync; currentStepN++)
                {
                    Matrix multipliedVector = Matrix.Multiply(Matrix, previousOutputPotentialVector);

                    double inputPotentialNeuronValue = multipliedVector.GetElement(studiedNeuronPosition, 0);
                    Matrix outputPotentialVector = GetOutputPotentialVector(previousOutputPotentialVector, multipliedVector, studiedNeuronPosition);

                    double energy = getEnergyAsyncOnly(outputPotentialVector);

                    if (previousOutputPotentialVector != null && outputPotentialVector.Equals(previousOutputPotentialVector))
                        stableIterationsStreak++;
                    else
                        stableIterationsStreak = 0;
                    
                    //  Output and streak handling
                    OutputRenderer.OutputCurrentStepHeader(currentStepN);
                    OutputRenderer.OutputInputPotentialAsync(inputPotentialNeuronValue, studiedNeuronPosition);
                    OutputRenderer.OutputOutputPotential(outputPotentialVector);
                    OutputRenderer.OutputEnergy(energy);
                    OutputRenderer.OutputIterationStreakAsync(stableIterationsStreak);

                    if (stableIterationsStreak >= 3)
                    {
                        OutputRenderer.OutputResultVector(outputPotentialVector);
                        break;
                    }
                    
                    //  Variables reset
                    previousOutputPotentialVector = outputPotentialVector;

                    if (studiedNeuronPosition < 2)
                        studiedNeuronPosition++;
                    else
                        studiedNeuronPosition = 0;
                }
            }
        }

        private Matrix GetOutputPotentialVector(Matrix studiedVector, Matrix multipliedVector, int studiedNeuronPosition)
        {
            double[] vectorArr = new double[3];

            for (int i = 0; i < 3; i++)
            {
                if (i == studiedNeuronPosition)
                    vectorArr[i] = multipliedVector.ToBiPolar().GetElement(i, 0);
                else
                    vectorArr[i] = studiedVector.GetElement(i, 0);
            }

            return new Matrix(vectorArr, true);
        }

        private double getEnergySyncOnly(Matrix previousStepVector, Matrix currentStepVector)
        {
            double result = 0;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    result += Matrix.GetElement(i, j) * currentStepVector.GetElement(i, 0) * previousStepVector.GetElement(j, 0);

            return result * -1;
        }

        private double getEnergyAsyncOnly(Matrix vector)
        {
            double result = 0;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    result += Matrix.GetElement(i, j) * vector.GetElement(i, 0) * vector.GetElement(j, 0);

            return result * -0.5;
        }
    }
}
