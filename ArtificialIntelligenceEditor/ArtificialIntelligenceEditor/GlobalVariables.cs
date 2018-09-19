using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArtificialIntelligenceEditor
{
    class GlobalVariables
    {
        public static double[][][] Weights;
        public static double[][] Bias;
        public static double[][] Dataset;
        public static double[][] Correct;
        public static int[] LayersNeurons;

        public static double LearnRate;
        public static double Momentum;
        public static double SigmoidKonstant;
        public static int EpochCount;

        BinaryReader MnistLabels;
        BinaryReader MnistImages;
    }
}
