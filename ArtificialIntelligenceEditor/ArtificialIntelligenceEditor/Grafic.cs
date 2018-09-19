using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace ArtificialIntelligenceEditor
{
    class Grafic
    {
        int sizeNeuron=80;
        int LineSpace=30; //ve sloupcích
        int ColumnSpace=200; //sloupců

        int[] sinapses_off;
        Panel panel;
        int[][,] coordinates;
        int[] layers;

        public Grafic(Panel p)
        {
            layers = GlobalVariables.LayersNeurons;
            panel = p;
            sinapses_off = new int[layers.Length];
        }

        public void DrawNeurons()
        {
            coordinates = new int[layers.Length][,];
            int odsazeni = 0;
            int maxNeuron = panel.Height / (sizeNeuron + LineSpace); //Kolik se jich vejde na výšku

            if ((ColumnSpace+sizeNeuron) * (layers.Length - 1) < panel.Width)
            {
                if (layers.Length%2==0)
                {
                    odsazeni = panel.Width / 2 - (ColumnSpace + sizeNeuron) * (layers.Length/2)+ColumnSpace/4;
                }
                else
                {
                    odsazeni = panel.Width / 2 - (ColumnSpace + sizeNeuron) * ((layers.Length)/2) -sizeNeuron;
                }
            }
         
            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i] >maxNeuron)
                {
                    coordinates[i] = Neuron(maxNeuron, odsazeni + (ColumnSpace + sizeNeuron) * i,true);
                    sinapses_off[i] = maxNeuron;
                }
                else
                {
                    coordinates[i] = Neuron(layers[i], odsazeni + (ColumnSpace + sizeNeuron) * i,false);
                    sinapses_off[i] = 0;
                }
            }          
        }

        public int [,] Neuron(int neuronCount, int x,bool dots)
        {
            int[,] coordinates = new int[neuronCount, 2];
            string path = Application.StartupPath + "\\Images\\ThreeDots2.png";        
            Bitmap image_dots = new Bitmap(path, true);
            image_dots = new Bitmap(image_dots, sizeNeuron, sizeNeuron);        
            path = Application.StartupPath + "\\Images\\NeuronImage2.png";
            Bitmap image_neuron = new Bitmap(path, true);
            image_neuron = new Bitmap(image_neuron, sizeNeuron, sizeNeuron);

            for (int i = 0; i < neuronCount; i++)
            {
                PictureBox picturebox = new PictureBox();
                coordinates[i, 0] = sizeNeuron / 2 + x;

                if (neuronCount % 2 == 0)
                {
                    coordinates[i, 1] = panel.Height/2 - (sizeNeuron + LineSpace) * (neuronCount / 2) + (sizeNeuron + LineSpace) * i;
                }
                else
                {
                    coordinates[i, 1] = panel.Height/2 - (sizeNeuron + LineSpace) * (neuronCount / 2) + (sizeNeuron + LineSpace) * i - sizeNeuron / 2 - LineSpace / 2;
                }
                picturebox.Location = new Point(coordinates[i, 0], coordinates[i, 1]);

                if (dots == true && i == neuronCount - 2)
                {
                    picturebox.Size = new Size(image_dots.Width, image_dots.Height);

                    picturebox.Image = image_dots;
                    panel.Controls.Add(picturebox);
                }
                else
                {
                    picturebox.Size = new Size(image_neuron.Width, image_neuron.Height);

                    picturebox.Image = image_neuron;
                    panel.Controls.Add(picturebox);
                }
            }

            return coordinates;
        }

        public void DrawSinapses(PaintEventArgs e, double [][][]weights)
        { 
            Graphics g = e.Graphics;
            //Pen p = new Pen(barva, 2);
            e.Graphics.TranslateTransform(panel.AutoScrollPosition.X, panel.AutoScrollPosition.Y); //Aby se dobře vykreslovaly čáry

            for (int i = 0; i < layers.Length - 1; i++)
            {
                for (int j = 0; j < coordinates[i].GetLength(0); j++)
                {
                    for (int k = 0; k < coordinates[i + 1].GetLength(0); k++)
                    {
                        if ((sinapses_off[i+1]!= 0 && k == sinapses_off[i+1] - 2)|| (j == sinapses_off[i] - 2&& sinapses_off[i] != 0))
                        {

                        }
                        else
                        {
                            
                            Pen x;
                            double max=0;
                            for (int a = 0; a < weights.Length; a++)
                            {
                                for (int b = 0; b < weights[a].Length; b++)
                                {
                                    if (weights[a][b].Max()>max)
                                    {
                                        max = weights[a][b].Max();
                                    }
                                }
                            }
                            double linear = (255 / max) * weights[i][k][j];
                            if (linear >=0)
                            {
                                if (linear>255)
                                {
                                    linear = 255;
                                }
                                x = new Pen(Color.FromArgb(255, (int)linear, (int)linear, (int)linear), 2);
                            }
                            else
                            {
                                if (linear < -255)
                                {
                                    linear = -255;
                                }
                                x = new Pen(Color.FromArgb(255, 0, -(int)linear, 0), 2);
                            }                           

                            g.DrawLine(x, coordinates[i][j, 0] + sizeNeuron, coordinates[i][j, 1] + sizeNeuron / 2, coordinates[i + 1][k, 0], coordinates[i + 1][k, 1] + sizeNeuron / 2);
                        }
                    }
                }
            }
        }
    }
}