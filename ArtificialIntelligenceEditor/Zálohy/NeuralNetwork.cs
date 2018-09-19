using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Windows.Forms;




namespace ArtificialIntelligenceEditor
{
    class NeuralNetwork
    {
        bool Inertia = true;
        double Momentum=0.5;
        double Sigmoid_k;
        double LearnRate;
        public static double[][][] WEIGHTS; //vrstva,předchozí neuron, neuron vstvy
        public static double[][] Bias;
        double TotalError = 0;
        public static int[]layers_neurons;
        double[][] Net;  //Pro každý neuron v každý vrstvě má výstupní hodnotu z predict
        double[][] Activation;
        double [] EpochError;
        BinaryReader brLabels;
        BinaryReader brImages;
        int EpochCount; 
        Chart ErrChart;
        
        public NeuralNetwork (int [] layersNeurons,double learnRate,double SigmoidKonstant, out double[][][] weights, out double [][]bias,BinaryReader im, BinaryReader label, Chart errChart) //layer,neuron,conection
        {
            brImages = im;
            brLabels = label;
            this.LearnRate = learnRate;          
            Sigmoid_k = SigmoidKonstant;
            weights = new double[layersNeurons.Length-1][][];
            bias = new double[layersNeurons.Length-1][];
            ErrChart = errChart;

            for (int a = 0; a < layersNeurons.Length-1; a++)
            {
                bias[a] =new double[layersNeurons[a+1]];
                weights[a] = new double[layersNeurons[a+1]][];
                for (int b = 0; b < layersNeurons[a+1]; b++)
                {
                    bias[a][b] = 0;
                    weights[a][b] = new double[layersNeurons[a]];

                    for (int c = 0; c < layersNeurons[a]; c++)
                    {
                        //weights[a][b][c] = 1;
                        double average = (layersNeurons[a] + layersNeurons[a + 1])/2;
                        weights[a][b][c] = 1/average;   //Xavier inicialization 
                    }
                }
            }

            Bias = bias;
            WEIGHTS = weights;
            layers_neurons = layersNeurons;
        }

        public double []Predict (double[]inputs)
        {           
            double[] result=new double[layers_neurons[layers_neurons.Length-1]]; //Delka výstupu
            double[][] net = new double[layers_neurons.Length][];         //Pomocná proměnná (výsledky z neuronů)
            double[][] activation = new double[layers_neurons.Length][];  //Přes act. fce

            for (int i = 0; i < layers_neurons.Length; i++)
            {
                net[i] = new double[layers_neurons[i]];
                activation[i] = new double[layers_neurons[i]];
            }

            for (int n = 0; n < inputs.Length; n++)              //vstupy
            {
                net[0][n] = inputs[n];
                activation[0][n] = inputs[n];
            }

            for (int i = 1; i < layers_neurons.Length ; i++)
            {
                for (int n=0;n<layers_neurons[i];n++)
                {
                    for (int b=0;b<layers_neurons[i-1];b++)
                    {
                        //net[i][n] += WEIGHTS[i-1][n][b] * net[i-1][b] + Bias[i-1][n];       
                        net[i][n] += WEIGHTS[i - 1][n][b] * net[i - 1][b];
                    }
                    //net[i][n]+= Bias[i - 1][n];  ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //activation[i][n] = Activation_function(net[i][n],layers_neurons[i-1]);
                    activation[i][n] = Activation_function(net[i][n], layers_neurons[i - 1], Bias[i - 1][n]);
                }
            }

            for (int r=0;r<result.Length;r++)
            {
                // result[r] = net[layers_neurons.Length - 1][r];
                result[r] = activation[layers_neurons.Length - 1][r];
            }
            Net = net;
            Activation = activation;

            return result;
        }
        
        public void Backpropagation (double [][] data, double [][]correct,int Epoch, double momentum) //data 1(1,2...), 2(1,2...) ... 
        {                                                                         //corect 1,2...
            Momentum = momentum;
            if (momentum==0)
            {
                Inertia = false;
            }
            else
            {
                Inertia = true;
            }
            EpochError = new double[Epoch];
            EpochCount = Epoch;
            double [][][]lastChange; //weights

            //double[][][] new_weights = new double[WEIGHTS.Length][][];
            lastChange = new double[WEIGHTS.Length][][];
            for (int a = 0; a < WEIGHTS.Length; a++)  //Uložení weights do newweights
            {
                //new_weights[a] = new double[WEIGHTS[a].Length][];
                lastChange[a] = new double[WEIGHTS[a].Length][];
                for (int b = 0; b < WEIGHTS[a].Length; b++)
                {
                    //new_weights[a][b] = new double[WEIGHTS[a][b].Length];
                    lastChange[a][b] = new double[WEIGHTS[a][b].Length];
                    for (int c = 0; c < WEIGHTS[a][b].Length; c++)
                    {
                        //new_weights[a][b][c] = WEIGHTS[a][b][c];
                    }
                }
            }

            for (int epoch = 0; epoch < EpochCount; epoch++)
            {
                for (int dat = 0; dat < data.GetLength(0); dat++) //Pro každá data určí nové váhy (Sochastic)
                {
                    double[][][] new_weights = new double[WEIGHTS.Length][][];
                    //lastChange = new double[WEIGHTS.Length][][];
                    for (int a = 0; a < WEIGHTS.Length; a++)  //Uložení weights do newweights
                    {
                        new_weights[a] = new double[WEIGHTS[a].Length][];
                        //lastChange[a] = new double[WEIGHTS[a].Length][];
                        for (int b = 0; b < WEIGHTS[a].Length; b++)
                        {
                            new_weights[a][b] = new double[WEIGHTS[a][b].Length];
                            //lastChange[a][b] = new double[WEIGHTS[a][b].Length];
                            for (int c = 0; c < WEIGHTS[a][b].Length; c++)
                            {
                                new_weights[a][b][c] = WEIGHTS[a][b][c];
                            }
                        }
                    }

                    double[] output = Predict(data[dat]);           //určení chyby
                    double[] error = new double[correct[0].Length];
                    for (int e = 0; e < correct[0].Length; e++)
                    {
                        error[e] = output[e] - correct[dat][e];
                        TotalError += error[e] * error[e];
                        //Console.WriteLine(error[e]);
                    }
                    TotalError /= 2 * error.Length;

                    List<double> deltas = new List<double>();
                    List<double> new_deltas = new List<double>();

                    for (int weights = WEIGHTS.GetLength(0) - 1; weights >= 0; weights--) //Projde vrstvy
                    {
                        for (int neurons = 0; neurons < layers_neurons[weights + 1]; neurons++) //Projde všechny neurony ve vrstvě
                        {
                            if (weights == WEIGHTS.GetLength(0) - 1) //Pokud je to poslední váha
                            {
                                //deltas.Add(Der_Activation_function(Net[weights+1][neurons]) * error[neurons]);
                                deltas.Add(Der_Activation_function(Activation[weights + 1][neurons], layers_neurons[weights]) * error[neurons]); //Z posledního neuronu
                                new_deltas = deltas;
                            }


                            else
                            {
                                int x = 0;
                                double sum = 0;
                                foreach (double delta in deltas)
                                {
                                    sum = WEIGHTS[weights + 1][x][neurons] * delta;
                                    x++;
                                }
                                //sum *= Der_Activation_function(Net[weights+1][neurons]); 
                                sum *= Der_Activation_function(Activation[weights + 1][neurons], layers_neurons[weights]);
                                new_deltas.Add(sum);
                            }
                            if (Inertia == true)
                            {
                                for (int conection = 0; conection < layers_neurons[weights]; conection++) //Projde všechny spojení s tímto neuronem
                                {
                                    /*1--*/
                                    /*
                                    double change = LearnRate * new_deltas[neurons] * Activation[weights][conection];
                                    //change = (1 - Momentum) * change + Momentum * lastChange[weights][neurons][conection];
                                    new_weights[weights][neurons][conection] -= (1 - Momentum) * change + Momentum * lastChange[weights][neurons][conection];
                                    //lastChange[weights][neurons][conection] = change;
                                    lastChange[weights][neurons][conection] = change;*/
                                    /*2--*/
                                    double change = LearnRate * new_deltas[neurons] * Activation[weights][conection];
                                    new_weights[weights][neurons][conection] -=  change + Momentum * lastChange[weights][neurons][conection];
                                    lastChange[weights][neurons][conection] = change;
                                }
                            }
                            else
                            {
                                for (int conection = 0; conection < layers_neurons[weights]; conection++) //Projde všechny spojení s tímto neuronem
                                {
                                    new_weights[weights][neurons][conection] -= LearnRate * new_deltas[neurons] * Activation[weights][conection];
                                }
                            }

                            Bias[weights][neurons] -= LearnRate * new_deltas[neurons];
                        }
                        deltas = new_deltas.ToList();
                        new_deltas.Clear();
                    }
                    for (int a = 0; a < WEIGHTS.Length; a++)  //Zapsání new weights do weights
                    {
                        for (int b = 0; b < WEIGHTS[a].Length; b++)
                        {
                            for (int c = 0; c < WEIGHTS[a][b].Length; c++)
                            {
                                WEIGHTS[a][b][c] = new_weights[a][b][c];
                            }
                        }
                    }
                }

                EpochError[epoch] = TotalError;
                TotalError = 0;

                double value = (((double)epoch + 1) / (double)EpochCount) * 100;
                Artificial_Intelligence_Editor.progress_bar.Value = (int)value;

                ErrChart.Series[0].Points.DataBindY(EpochError);
                if (epoch == 0)
                {
                    ErrChart.Visible = true;
                }
                ErrChart.Update();              
            }
        }

        public void MNIST_LoadDataset(out double [][]Dataset, out double[][] Correct, int data)
        {                   
            double[][] dataset = new double[data][];
            double[][] correct= new double[data][];

            for (int i = 0; i < data; i++)
            {
                correct[i] = new double[10];
                dataset[i] = new double[785];
            }
            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            for (int d = 0; d < data; d++)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {                      
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                    }
                }

                int h = 0;
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        if (pixels[j][i] > 0)
                        {
                            dataset[d][h] = 1;
                        }
                        else
                        {
                            dataset[d][h] = 0;
                        }

                        h++;
                    }
                }
                byte lbl = brLabels.ReadByte();
                correct[d][(int)lbl] = 1;
            }
            Dataset = dataset;
            Correct = correct;
        }

        public void Numbers_Load(out double[][] Dataset, out double[][] Correct, out double[][] Orignial)
        {
            double[][] dataset = new double[10][];
            double[][] correct = new double[10][];
            double[][] original = new double[10][];

            for (int i = 0; i < 10; i++)
            {
                correct[i] = new double[10];
                
                for (int d = 0; d < 10; d++)
                {
                    if (d != i)
                    {
                        correct[i][d] = 0;
                    }
                    else
                    {
                        correct[i][d] = 1;
                    }
                }

                string path = Application.StartupPath + "\\Images\\Numbers\\";              
                path += i.ToString() + ".PNG";
                Bitmap number = new Bitmap(path, true);

                dataset[i] = new double[number.Width*number.Height];
                original[i] = new double[number.Width * number.Height];

                int x = 0;

                for (int w=0;w<number.Width; w++)
                {
                    for (int h = 0; h < number.Height; h++)
                    {
                        Color c = number.GetPixel(w,h);

                        int sum;/*= c.R;
                        sum+= c.G;
                        sum+=c.B;  */                    
                        if (c.A==0)
                        {
                            sum = 0;
                        }
                        else
                        {
                            sum = 1;
                        }

                        dataset[i][x] = sum;
                        original[i][x] = sum;
                        x++;
                    }
                }
            }

            Dataset = dataset;
            Correct = correct;
            Orignial = original;
        }

        double Activation_function(double input,int prev_n_cnt,double bias)  //Počet napojených neuronů (hodnota konstanty)
        {
            double logistic;
            //logistic = (1) / (1 + Math.Pow(Math.E, -input*Sigmoid_k));
            logistic = (1) / (1 + Math.Pow(Math.E, -input * Sigmoid_k-bias));

            //logistic = (1) / (1 + Math.Pow(Math.E, -input * (Sigmoid_k/(prev_n_cnt*10))));
            //logistic = input;

            return logistic;
        }

        double Der_Activation_function (double activ, int prev_n_cnt)  //!!!!!!!!!!Derivace sigmoidy -> f(x)(1-f(x)), ale f(x) je výsledek ze sigmoidy!!
        {
            double der = activ * (1 - activ)*Sigmoid_k;
            //double der = activ * (1 - activ) * (Sigmoid_k/ (prev_n_cnt * 10));
            //double der = 1;

            return der;
        }
    }
}