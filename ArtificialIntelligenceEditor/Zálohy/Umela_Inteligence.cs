using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ArtificialIntelligenceEditor
{
    public partial class Artificial_Intelligence_Editor : Form
    {
        /*!!!Main properties!!!*/
        int[] layersNeurons = { 2, 1 };  //1.input
        double learn_rate = 0.1;
        int Epochs = 100;
        double Momentum = 0;
        double SigmoidKonstant = 1;
        //Graphic
        int column_space = 200;
        int neuronSize = 80;
        int line_space = 30;
        int MNIST_IMAGE_SIZE = 50;
        /*!!!Main properties!!!*/

        double[][][] Weights;
        double[][] Bias;
        double[][] dataset;
        double[][] correct;
        double[][] new_dataset;
        double[][] new_correct;
        double[][] original_pictures;
        Grafic grafic;
        NeuralNetwork neural_network;
        BinaryReader brLabels;
        BinaryReader brImages;
        bool act = false;
        bool bp = false;
        int MNIST_data = 1;
        int image_size;
        Panel MNIST_Panel;
        int selectedNumber=0;

        //public static BackgroundWorker _worker = null;

        Font font = new Font("Calibri", 10.0F);
        Font font_small = new Font("Calibri", 8.0F);
        Font font_big = new Font("Calibri", 12.0F);

        Panel TwoInputs_Panel;
        Panel Moving_Panel;

        TextBox Input1_TB_TwoInputs = new TextBox();         //TextBox
        TextBox Input2_TB_TwoInputs = new TextBox();
        TextBox MovedIMGcount_TB = new TextBox();

        Label Predict_L_TwoInputs = new Label();

        NumericUpDown NumberNumeric_side;
        NumericUpDown NumberNumeric_up;


        public static ProgressBar progress_bar;


        public Artificial_Intelligence_Editor()
        { 
            InitializeComponent();

            progress_bar = TrainProgressBar;

            LearnRateTextBox.Text = learn_rate.ToString();    //Tlačítka a texty
            MomentumTextBox.Text = Momentum.ToString();
            Epoch_textBox.Text = Epochs.ToString();
            SigmoidTextBox.Text = SigmoidKonstant.ToString();
            DataSizeTextBox.Text = MNIST_data.ToString();
            var str1 = string.Join(", ", layersNeurons.Select(o => o.ToString()).ToArray());
            NeuronCountTextBox.Text = str1;
            InputComboBox.Items.Add("Binarní AND");
            InputComboBox.Items.Add("Binarní OR");
            InputComboBox.Items.Add("Binarní XOR");
            InputComboBox.Items.Add("Databáze MNIST");
            InputComboBox.Items.Add("Čísla 0-9");
            ErrorChart.Visible = false;
            DataSizeTextBox.Visible = false;
            DataSizeLabel.Visible = false;

            string path = Application.StartupPath + "\\MNIST\\train-labels.idx1-ubyte";
            FileStream ifsLabels = new FileStream(path, FileMode.Open);
            path = Application.StartupPath + "\\MNIST\\train-images.idx3-ubyte";
            FileStream ifsImages = new FileStream(path, FileMode.Open);
            brLabels = new BinaryReader(ifsLabels);
            brImages = new BinaryReader(ifsImages);

            int magic1 = brImages.ReadInt32(); // discard
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();
            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();

            neural_network = new NeuralNetwork(layersNeurons, learn_rate, SigmoidKonstant, out Weights, out Bias, brImages, brLabels,ErrorChart);
            MainPanel.Paint += MainPanel_Paint;
        }


        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            if (act == true)
            {
                grafic.DrawSinapses(e, Weights);
                act = false;
            }
        }

        void MNIST_image(Panel Mnist_Panel, int image_number, double[][] Dataset, double[][] Correct, bool Out)
        {
            PictureBox Label = new PictureBox();
            Label corr = new Label();
            Label answered = new Label();

            foreach (PictureBox c in Mnist_Panel.Controls.OfType<PictureBox>())
            {
                if (c.Name == "Main_PictureBox")
                    Label = c;
            }
            foreach (Label c in Mnist_Panel.Controls.OfType<Label>())
            {
                if (c.Name == "Correct")
                    corr = c;
            }
            foreach (Label c in Mnist_Panel.Controls.OfType<Label>())
            {
                if (c.Name == "Answered")
                    answered = c;
            }

            Bitmap bm = new Bitmap(image_size, image_size);
            int collumn = 0;
            int row = 0;

            for (int i = 0; i < Dataset[image_number].Length; i++)
            {
                if (Dataset[image_number][i] == 0)
                {
                    bm.SetPixel(row, collumn, Color.FromArgb(0, 0, 0, 0));
                }
                else
                {
                    if (Out == false)
                    {
                        bm.SetPixel(row, collumn, Color.FromArgb(255, 0, 200 * (int)Dataset[image_number][i], 0));
                    }
                    else
                    {
                        bm.SetPixel(row, collumn, Color.FromArgb(255, 200 * (int)Dataset[image_number][i], 0, 0));
                    }
                }

                collumn++;
                if (collumn == image_size - 1)
                {
                    row++;
                    collumn = 0;
                }
            }
            Bitmap biger = new Bitmap(bm, MNIST_IMAGE_SIZE, MNIST_IMAGE_SIZE);

            Label.Image = biger;
            for (int k = 0; k < Correct[0].Length; k++)
            {
                if (Correct[image_number][k] == 1)
                {
                    corr.Text = "Správná: " + k;
                }
            }
            double[] anser;
            if (bp == false)
            {
                NeuralNetwork neural_network2 = new NeuralNetwork(layersNeurons, learn_rate, SigmoidKonstant, out Weights, out Bias, brImages, brLabels, ErrorChart);
                anser = neural_network2.Predict(Dataset[image_number]);
            }
            else
            {
                anser = neural_network.Predict(Dataset[image_number]);
            }
            double x = 0;
            for (int k = 0; k < anser.Length; k++)
            {
                if (anser[k] > x)
                {

                    x = anser[k];
                    answered.Text = "Odpověď: " + k + " (" + Math.Round(x, 2) + ")";
                }
            }


            Mnist_Panel.Controls.Add(Label);
            InputPanel.Controls.Add(Mnist_Panel);
        }

        void MNIST_panel(out Panel panel) //,out PictureBox P
        {
            Panel Mnist_Panel = new Panel();
            PictureBox pict = new PictureBox();
            Label label = new Label();
            Label corr = new Label();
            Label answered = new Label();
            NumericUpDown num = new NumericUpDown();

            FontFamily fam = new FontFamily("Calibri");
            Font font = new Font(fam, 10.0F);

            Mnist_Panel.BackColor = Color.Transparent;
            label.ForeColor = Color.White;
            corr.ForeColor = Color.White;
            answered.ForeColor = Color.White;
            num.ForeColor = Color.White;
            num.BackColor = Color.FromArgb(20, 0, 0);

            label.Font = font;
            corr.Font = font;
            answered.Font = font;
            num.Font = new Font(fam, 8.0F);

            corr.Name = "Correct";
            answered.Name = "Answered";
            pict.Name = "Main_PictureBox";

            Mnist_Panel.Height = 600;

            pict.Location = new Point(Mnist_Panel.Width / 2 - MNIST_IMAGE_SIZE * (35 / 20) - 10, 2);

            label.Location = new Point(0, 53);
            label.Text = "Obrázek:";
            label.Width = 70;

            answered.Location = new Point(0, 71);
            answered.Text = "Odpověď: ";
            answered.Width = 80;

            corr.Location = new Point(0, 88);
            corr.Text = "Správná: ";
            corr.Width = 100;
            corr.Height = 13;

            num.Location = new Point(80, 52);
            num.Width = 40;
            num.Height = 20;
            num.ValueChanged += num_ValueChanged;
            num.Maximum = 9912422;

            Mnist_Panel.Controls.Add(pict);
            Mnist_Panel.Controls.Add(corr);
            Mnist_Panel.Controls.Add(answered);
            Mnist_Panel.Controls.Add(label);
            Mnist_Panel.Controls.Add(num);
            InputPanel.Controls.Add(Mnist_Panel);
            panel = Mnist_Panel;
        }

        void num_ValueChanged(object sender, EventArgs e)
        {
            decimal value = ((NumericUpDown)sender).Value;
            if (value >= MNIST_data && image_size == 29)
            {
                if (new_correct == null)
                {
                    new_correct = new double[0][];  //pomocná pole
                    new_dataset = new double[0][];
                }

                if (value - MNIST_data + 1 > new_correct.Length)   //Pokud je větší než dosavadní new data... přidá do new data hodnoty až po value
                {
                    double[][] new_correct2 = new double[(int)value - dataset.Length + 1][];  //pomocná pole
                    double[][] new_dataset2 = new double[(int)value - dataset.Length + 1][];

                    for (int i = 0; i < (int)value - dataset.Length + 1; i++)
                    {
                        new_correct2[i] = new double[10];
                        new_dataset2[i] = new double[785];
                        if (i < new_correct.Length)
                        {
                            for (int k = 0; k < new_dataset[i].Length; k++)
                            {
                                new_dataset2[i][k] = new_dataset[i][k];
                            }
                            for (int k = 0; k < new_correct[i].Length; k++)
                            {
                                new_correct2[i][k] = new_correct[i][k];
                            }
                        }
                    }

                    for (int k = 0; k < (int)value - dataset.Length + 1; k++)
                    {
                        byte[][] pixels = new byte[28][];
                        for (int i = 0; i < pixels.Length; ++i)
                            pixels[i] = new byte[28];

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
                                    new_dataset2[k][h] = 1;
                                }
                                else
                                {
                                    new_dataset2[k][h] = 0;
                                }

                                h++;
                            }
                        }
                        byte lbl = brLabels.ReadByte();
                        new_correct2[k][(int)lbl] = 1;
                    }

                    new_correct = new_correct2; //snad!!!
                    new_dataset = new_dataset2;
                }

                MNIST_image(MNIST_Panel, (int)value - dataset.Length, new_dataset, new_correct, true);

                //MessageBox.Show("Zvěčte databázy", "Error",
                //MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (image_size == 101 && value > dataset.Length-1)
            {
                MessageBox.Show("Konec databáze", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                ((NumericUpDown)sender).Value = dataset.Length - 1;
            }
            else
            {
                MNIST_image(MNIST_Panel, (int)value, dataset, correct, false);
                selectedNumber = (int)value;
            }
        }

        void num_ValueChanged_side(object sender, EventArgs e)
        {
            decimal value = ((NumericUpDown)sender).Value;
            for (int i = 0; i < correct[selectedNumber].Length; i++)
            {
                if (correct[selectedNumber][i] == 1)
                {
                    dataset[selectedNumber] = MoveImage(original_pictures[i], (int)value, (int)NumberNumeric_up.Value);
                }
            }

            MNIST_image(MNIST_Panel, selectedNumber, dataset, correct, false);
        }

        void num_ValueChanged_up(object sender, EventArgs e)
        {
            decimal value = ((NumericUpDown)sender).Value;

            for (int i=0;i< correct[selectedNumber].Length;i++)
            {
                if (correct[selectedNumber][i]==1)
                {
                    dataset[selectedNumber] = MoveImage(original_pictures[i], (int)NumberNumeric_side.Value, (int)value);
                }
            }

            MNIST_image(MNIST_Panel, selectedNumber, dataset, correct, false);
        }

        private void reload_button_Click(object sender, EventArgs e)
        {           
            if (!double.TryParse(LearnRateTextBox.Text, out learn_rate))
            {

            }
            if (!int.TryParse(Epoch_textBox.Text, out Epochs))
            {

            }
            if (!double.TryParse(SigmoidTextBox.Text, out SigmoidKonstant))
            {

            }
            if (!int.TryParse(DataSizeTextBox.Text, out MNIST_data))
            {

            }
            if (!double.TryParse(MomentumTextBox.Text, out Momentum))
            {

            }                            

            string layRead = NeuronCountTextBox.Text;
            layersNeurons = layRead.Split(',').Select(n => Convert.ToInt32(n)).ToArray();

            neural_network = new NeuralNetwork(layersNeurons, learn_rate, SigmoidKonstant, out Weights, out Bias, brImages, brLabels, ErrorChart);
            if ((string)InputComboBox.SelectedItem == "Databáze MNIST")
            {
                neural_network.MNIST_LoadDataset(out dataset, out correct, MNIST_data); //785
            }
            if ((string)InputComboBox.SelectedItem == "Čísla 0-9")
            {
                int cnt;
                if (!int.TryParse(MovedIMGcount_TB.Text, out cnt))
                {

                }
                MoveImageDataset(cnt);
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();
            neural_network.Backpropagation(dataset, correct, Epochs, Momentum);
            
            SaveData("LastBPData");

            bp = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("BackProp: " + elapsedMs + " ms.");

            var str1 = string.Join(", ", layersNeurons.Select(o => o.ToString()).ToArray());
            NeuronCountTextBox.Text = str1;
            /*
            for (int d = 0; d < dataset.Length; d++)
            {
                double[] outp = neural_network.Predict(dataset[d]);
            }
            */
            MainPanel.Controls.Clear();
            grafic = new Grafic(neuronSize, line_space, column_space, layersNeurons, MainPanel);
            grafic.DrawNeurons();
            act = true;
            MainPanel.Invalidate();
        }

        private void MainPanel_Scroll(object sender, ScrollEventArgs e)
        {
            //MainPanel.Refresh();

        }

        private void InputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)InputComboBox.SelectedItem == "Binarní AND")
            {/*
                learn_rate = 0.5;
                LearnRateTextBox.Text = learn_rate.ToString();
                SigmoidKonstant = 10;
                SigmoidTextBox.Text = SigmoidKonstant.ToString();
                Epochs = 3;
                Epoch_textBox.Text = Epochs.ToString();
                */
                learn_rate = 60;
                LearnRateTextBox.Text = learn_rate.ToString();
                SigmoidKonstant = 1;
                SigmoidTextBox.Text = SigmoidKonstant.ToString();
                Epochs = 5;
                Epoch_textBox.Text = Epochs.ToString();
                layersNeurons[0] = 2;
                layersNeurons[layersNeurons.Length - 1] = 1;
                var str1 = string.Join(", ", layersNeurons.Select(o => o.ToString()).ToArray());
                NeuronCountTextBox.Text = str1;

                TwoInput_Panel();
               
                dataset = new double[4][] { new double[2] { 1, 1 }, new double[2] { 1, 0 }, new double[2] { 0, 1 }, new double[2] { 0, 0 } };
                correct = new double[4][] { new double[1] { 1 }, new double[1] { 1 }, new double[1] { 1 }, new double[1] { 0 } };

                Close_Controls();
                TwoInputs_Panel.Visible = true;
            }

            if ((string)InputComboBox.SelectedItem == "Binarní OR")
            {
                learn_rate = 1;
                LearnRateTextBox.Text = learn_rate.ToString();
                SigmoidKonstant = 10;
                SigmoidTextBox.Text = SigmoidKonstant.ToString();
                Epochs = 3;
                Epoch_textBox.Text = Epochs.ToString();

                layersNeurons[0] = 2;
                layersNeurons[layersNeurons.Length - 1] = 1;
                var str1 = string.Join(", ", layersNeurons.Select(o => o.ToString()).ToArray());
                NeuronCountTextBox.Text = str1;

                dataset = new double[4][] { new double[2] { 1, 1 }, new double[2] { 1, 0 }, new double[2] { 0, 1 }, new double[2] { 0, 0 } };
                correct = new double[4][] { new double[1] { 1 }, new double[1] { 0 }, new double[1] { 0 }, new double[1] { 0 } };

                TwoInput_Panel();
                Close_Controls();
                TwoInputs_Panel.Visible = true;
            }

            if ((string)InputComboBox.SelectedItem == "Binarní XOR")
            {
                learn_rate = 0.1;
                LearnRateTextBox.Text = learn_rate.ToString();

                layersNeurons[0] = 2;
                layersNeurons[layersNeurons.Length - 1] = 1;
                var str1 = string.Join(", ", layersNeurons.Select(o => o.ToString()).ToArray());
                NeuronCountTextBox.Text = str1;

                dataset = new double[4][] { new double[2] { 1, 1 }, new double[2] { 1, 0 }, new double[2] { 0, 1 }, new double[2] { 0, 0 } };
                correct = new double[4][] { new double[1] { 0 }, new double[1] { 1 }, new double[1] { 1 }, new double[1] { 0 } };

                TwoInput_Panel();
                Close_Controls();
                TwoInputs_Panel.Visible = true;
            }

            if ((string)InputComboBox.SelectedItem == "Databáze MNIST")
            {/*
                image_size = 29;
                learn_rate = 1000;
                LearnRateTextBox.Text = learn_rate.ToString();
                SigmoidKonstant = 0.01;
                SigmoidTextBox.Text = SigmoidKonstant.ToString();
                Epochs = 20;
                Epoch_textBox.Text = Epochs.ToString();*/
                image_size = 29;
                learn_rate = 0.3;
                LearnRateTextBox.Text = learn_rate.ToString();
                SigmoidKonstant = 1;
                SigmoidTextBox.Text = SigmoidKonstant.ToString();
                Epochs = 5;
                Epoch_textBox.Text = Epochs.ToString();

                layersNeurons[0] = 785;
                layersNeurons[layersNeurons.Length - 1] = 10;
                var str1 = string.Join(", ", layersNeurons.Select(o => o.ToString()).ToArray());
                NeuronCountTextBox.Text = str1;
                neural_network.MNIST_LoadDataset(out dataset, out correct, MNIST_data); //785              

                if (MNIST_Panel == null)
                {
                    MNIST_Panel = new Panel();
                    MNIST_panel(out MNIST_Panel);                 
                }

                Close_Controls();
                MNIST_image(MNIST_Panel, 0, dataset, correct, false);               
                MNIST_Panel.Visible = true;
                DataSizeLabel.Visible = true;
                DataSizeTextBox.Visible = true;
            }

            if ((string)InputComboBox.SelectedItem == "Čísla 0-9")
            {/*
                image_size = 101;
                learn_rate = 2000;
                LearnRateTextBox.Text = learn_rate.ToString();
                SigmoidKonstant = 0.0025;
                SigmoidTextBox.Text = SigmoidKonstant.ToString();
                Epochs = 5;
                Epoch_textBox.Text = Epochs.ToString();*/
                image_size = 101;
                learn_rate = 0.01;
                LearnRateTextBox.Text = learn_rate.ToString();
                SigmoidKonstant = 1;
                SigmoidTextBox.Text = SigmoidKonstant.ToString();
                Epochs = 5;
                Epoch_textBox.Text = Epochs.ToString();

                if (NumberNumeric_side == null)
                {
                    Moving_Panel = new Panel();
                    InputPanel.Controls.Add(Moving_Panel);

                    Moving_Panel.Location = new Point(0, 110);
                    Moving_Panel.Width = 130;
                    Moving_Panel.Height = 120;
                    Moving_Panel.BackColor = Color.Transparent;

                    Label up = new Label();         //Labels
                    Label side = new Label();
                    Label CountMovedImg = new Label();
                    up.Location = new Point(0, 0);
                    side.Location = new Point(0, 50);
                    CountMovedImg.Location= new Point(7, 190);
                    up.Width = 130;
                    side.Width = 130;
                    CountMovedImg.Width = 168;
                    up.ForeColor = Color.White;
                    side.ForeColor = Color.White;
                    CountMovedImg.ForeColor = Color.White;
                    up.Font = font;
                    side.Font = font;
                    CountMovedImg.Font = font_big;
                    up.Text = "Posun nahoru:";
                    side.Text = "Posun na stranu:";
                    CountMovedImg.Text = "Posunuté obrázky (x10):";

                    Moving_Panel.Controls.Add(up);
                    Moving_Panel.Controls.Add(side);
                    ControlPanel.Controls.Add(CountMovedImg);

                    TextBox MovImgCnt_textbox = new TextBox();   //textbox
                    MovImgCnt_textbox.Location = new Point(175, 190);
                    MovImgCnt_textbox.BackColor = Color.FromArgb(30, 0, 0);
                    MovImgCnt_textbox.ForeColor = Color.White;
                    MovImgCnt_textbox.Font = font_big;
                    MovImgCnt_textbox.BorderStyle = BorderStyle.None;
                    ControlPanel.Controls.Add(MovImgCnt_textbox);

                    MovedIMGcount_TB = MovImgCnt_textbox;


                    NumericUpDown num = new NumericUpDown();  //Numerics
                    num.ForeColor = Color.White;
                    num.BackColor = Color.FromArgb(20, 0, 0);
                    num.Font = font_small;
                    num.Location = new Point(10,75);
                    num.Width = 40;
                    num.ValueChanged += num_ValueChanged_side;
                    num.Maximum = 100;
                    num.Minimum = -100;
                    NumberNumeric_side = num;

                    NumericUpDown num_up = new NumericUpDown();
                    num_up.ForeColor = Color.White;
                    num_up.BackColor = Color.FromArgb(20, 0, 0);
                    num_up.Font = font_small;
                    num_up.Location = new Point(10, 25);
                    num_up.Width = 40;
                    num_up.ValueChanged += num_ValueChanged_up;
                    num_up.Maximum = 100;
                    num_up.Minimum = -100;
                    NumberNumeric_up = num_up;

                    Moving_Panel.Controls.Add(num_up);
                    Moving_Panel.Controls.Add(num);
                }

                neural_network.Numbers_Load(out dataset, out correct,out original_pictures);
                int[] x = { 10000, 10 };
                layersNeurons = x;
                var str1 = string.Join(", ", layersNeurons.Select(o => o.ToString()).ToArray());
                NeuronCountTextBox.Text = str1;

                if (MNIST_Panel == null)
                {
                    MNIST_Panel = new Panel();
                    MNIST_panel(out MNIST_Panel);
                }
                Close_Controls();
                MNIST_image(MNIST_Panel, 0, dataset, correct, false);               
                MNIST_Panel.Visible = true;
                Moving_Panel.Visible = true;

                MovedIMGcount_TB.Text = "1";
                MoveImageDataset(1);
            }
        }

        double[] MoveImage(double[] image, int move, int up_move)
        {
            double[] result = new double[image.Length];
            double size = Math.Pow(image.Length, 1.0 / 2.0); //delka radku/sloupce
            int x;
            int y = 0;

            if (move < 0)
            {
                x = (-move) * (int)size;
            }
            else
            {
                x = 0;
            }

            for (int w = 0; w < size; w++)
            {
                for (int h = 0; h < size; h++)  //projde výšku
                {
                    if (w < (size + move) && move < 0)
                    {
                        if ((up_move >= 0 && h + up_move < size)|| (up_move < 0 && h + up_move > 0))
                        {
                            result[y] = image[x + up_move];
                        }
                        x++;
                    }
                    else if (move >= 0 && w >= move)
                    {
                        if ((up_move >= 0 && h + up_move < size)|| (up_move < 0 && h + up_move > 0))
                        {
                            result[y] = image[x + up_move];
                        }
                        x++;
                    }
                    else
                    {
                        result[y] = 0;
                    }
                    y++;
                }
            }

            return result;
        }

        void MoveImageDataset(int EveryImageCount)
        {
            double[][] _dataset;
            double[][] _correct;

            _dataset = new double[10*EveryImageCount][];
            _correct = new double[10*EveryImageCount][];

            Random rnd = new Random();

            for (int image = 0; image < 10; image++)
            {
                for (int newImage = 0; newImage < EveryImageCount; newImage++)
                {
                    _correct[EveryImageCount * image + newImage] = new double[10];

                    _dataset[EveryImageCount * image + newImage] = MoveImage(original_pictures[image], rnd.Next(-40, 40), rnd.Next(-30, 30));
                    for (int i = 0; i < 10; i++)
                    {
                        if (i==image)
                        {
                            _correct[EveryImageCount * image + newImage][i] = 1;
                        }
                        else
                        {
                            _correct[EveryImageCount * image + newImage][i] = 0;
                        }
                    }
                }
            }

            dataset = _dataset;
            correct = _correct;
        }

        void Close_Controls()
        {
            bp = false;
            if (TwoInputs_Panel != null)
            {
                TwoInputs_Panel.Visible = false;
            }         

            if (MNIST_Panel != null)
            {
                MNIST_Panel.Visible = false;
                DataSizeTextBox.Visible = false;
                DataSizeLabel.Visible = false;
            }
            if(Moving_Panel != null)
            {
                Moving_Panel.Visible = false;
            }

            DataSizeTextBox.Visible = false;
            DataSizeLabel.Visible = false;
        }

        void TwoInput_Panel()
        {
            if (TwoInputs_Panel == null)     //Panel definition
            {
                Panel TwoInput = new Panel();
                InputPanel.Controls.Add(TwoInput);
                TwoInputs_Panel = TwoInput;

                TwoInputs_Panel.Location = new Point(3, 3);
                TwoInputs_Panel.Width = 130;
                TwoInputs_Panel.Height = 120;
                TwoInputs_Panel.BackColor = Color.Transparent;


                Label Input1 = new Label();         //Labels
                Label Input2 = new Label();
                Label Predict = new Label();

                Input1.Location = new Point(3, 0);
                Input2.Location = new Point(3, 30);
                Predict.Location = new Point(3, 60);

                Input1.Width = 80;
                Input2.Width = 80;
                Predict.Width = 100;

                Input1.ForeColor = Color.White;
                Input2.ForeColor = Color.White;
                Predict.ForeColor = Color.White;

                Input1.Font = font;
                Input2.Font = font;
                Predict.Font = font;

                Input1.Text = "První vstup: ";
                Input2.Text = "Druhý vstup: ";
                Predict.Text = "Odhad: --";

                TwoInputs_Panel.Controls.Add(Input1);
                TwoInputs_Panel.Controls.Add(Input2);
                TwoInputs_Panel.Controls.Add(Predict);

                Predict_L_TwoInputs = Predict;


                TextBox Input1_TextBox = new TextBox();       //TextBoxs
                TextBox Input2_TextBox = new TextBox();

                Input1_TextBox.Location = new Point(82, 0);
                Input2_TextBox.Location = new Point(82, 30);

                Input1_TextBox.BackColor = Color.FromArgb(20, 0, 0);
                Input2_TextBox.BackColor = Color.FromArgb(20, 0, 0);

                Input1_TextBox.ForeColor = Color.White;
                Input2_TextBox.ForeColor = Color.White;

                Input1_TextBox.Font = font;
                Input2_TextBox.Font = font;

                Input1_TextBox.BorderStyle = BorderStyle.None;
                Input2_TextBox.BorderStyle = BorderStyle.None;

                TwoInputs_Panel.Controls.Add(Input1_TextBox);
                TwoInputs_Panel.Controls.Add(Input2_TextBox);

                Input1_TB_TwoInputs = Input1_TextBox;
                Input2_TB_TwoInputs = Input2_TextBox;


                Button butt = new Button();         //button
                butt.Location = new Point(50, 90);

                butt.FlatStyle = FlatStyle.Flat;
                butt.Font = font;
                butt.ForeColor = Color.White;
                butt.Size = new Size(50, 25);
                butt.Text = "Zkus";
                butt.Click += new EventHandler(TryInputsButton_Click_TwoInputs);

                TwoInputs_Panel.Controls.Add(butt);
            }
        }

        private void TryInputsButton_Click_TwoInputs(object sender, EventArgs e)
        {
            double[] input = new double[2];

            if (!double.TryParse(Input1_TB_TwoInputs.Text, out input[0]))
            {

            }
            if (!double.TryParse(Input2_TB_TwoInputs.Text, out input[1]))
            {

            }

            double[] reason = neural_network.Predict(input);
            double r = Math.Round(reason[0], 2);
            Predict_L_TwoInputs.Text = "Odhad: " + r.ToString();
        }

        void SaveData(string path)
        {
            //string cesta = "";
            string cesta = path;
            try
            { /*
                cesta = Directory.GetCurrentDirectory();     //Folder
                if (!Directory.Exists(Path.Combine(cesta, "Data")))
                    Directory.CreateDirectory(Path.Combine(cesta, "Data"));
                cesta=Path.Combine(cesta, "Data");
                cesta= Path.Combine(cesta, name+".txt");
                */

                using (StreamWriter sw = File.CreateText(cesta))         //TextFile
                {
                    sw.WriteLine("L" + layersNeurons.Length.ToString());
                    for (int i = 0; i < layersNeurons.Length; i++)
                    {
                        sw.WriteLine("N" + layersNeurons[i].ToString());
                    }
                    for (int l = 0; l < layersNeurons.Length-1; l++)
                    {
                        sw.WriteLine("W"+(l+1).ToString()+"/"+ (layersNeurons.Length - 1).ToString() + "L" );
                        for (int n = 0; n < layersNeurons[l+1]; n++)
                        {
                            sw.WriteLine("W" + (n+1).ToString() +"/"+layersNeurons[l + 1].ToString() + "N");
                            for (int c = 0; c < layersNeurons[l]; c++)
                            {
                                sw.WriteLine("C"+(c+1).ToString()+"/"+layersNeurons[l].ToString()+"_"+Weights[l][n][c].ToString());
                            }
                        }
                    }

                    for (int l = 1; l < layersNeurons.Length; l++)
                    {
                        sw.WriteLine("B" + (l).ToString()+"/"+(layersNeurons.Length-1).ToString() + "L");
                        for (int n = 0; n < layersNeurons[l]; n++)
                        {                    
                            sw.WriteLine("C"+(n+1).ToString()+"/"+layersNeurons[l].ToString()+"_"+ Bias[l-1][n].ToString());                     
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Nepodařilo se vytvořit složku {0}, zkontrolujte prosím svá oprávnění.", cesta);
            }        
        }

        void LoadData(out double [][][] _weights, out double[][] _bias,out int []layNeur)
        {
            _weights = new double[1][][];
            _bias = new double[1][];
            layNeur = new int[1];
            //int[] _layersNeurons;

            string Path="";
            OpenFileDialog F_dialog = new OpenFileDialog();
            F_dialog.InitialDirectory = Application.StartupPath;
            F_dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (F_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Path = F_dialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                try
                {
                    using (StreamReader sr = new StreamReader(Path))
                    {
                        String line = sr.ReadLine();
                        layNeur = new int [Int32.Parse(""+ line[1])];
                        _weights = new double[layNeur.Length-1][][];
                        _bias = new double[layNeur.Length - 1][];

                        for (int i = 0; i < layNeur.Length; i++)
                        {
                            line = sr.ReadLine();
                            layNeur[i] = Convert.ToInt32(line.Substring(1)); ;
                        }

                        for (int l = 0; l < layNeur.Length-1;l++)
                        {
                            _weights[l] = new double[layNeur[l+1]][];
                            _bias[l] = new double[layNeur[l + 1]];

                            line = sr.ReadLine();
                            for (int n = 0; n < layNeur[l+1]; n++)
                            {
                                _weights[l][n] = new double[layNeur[l]];

                                line = sr.ReadLine();
                                for (int c = 0; c < layNeur[l]; c++)
                                {
                                    line = sr.ReadLine();
                                    _weights[l][n][c]= Convert.ToDouble(line.Substring(line.IndexOf("_") + 1));
                                }
                            }
                        }

                        for (int l = 0; l < layNeur.Length-1; l++)
                        {
                            line = sr.ReadLine();
                            for (int n = 0; n < layNeur[l + 1]; n++)
                            {
                                 line = sr.ReadLine();
                                 _bias[l][n] = Convert.ToDouble(line.Substring(line.IndexOf("_") + 1)); 
                            }
                        }

                        //List<List<List<double>>> L = new List<List<List<double>>>();
                        //_weights = L.Select(a => a.Select(b=>b.ToArray()).ToArray()).ToArray();  //Pro každá a v L uděla "to array"
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Soubor nejde přečíst:");
                    Console.WriteLine(e.Message);
                }
            } 
        }

        private void SaveData_button_Click(object sender, EventArgs e)
        {
            string Path = "";
            string name = "";

            SaveFileDialog s_dialog = new SaveFileDialog();
            s_dialog.InitialDirectory = Application.StartupPath;
            s_dialog.Filter = "Text File | *.txt";

            if (s_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Path = s_dialog.FileName;
                    //name=s_dialog.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }


            SaveData(Path);
        }

        private void LoadData_button_Click(object sender, EventArgs e)
        {
            LoadData(out Weights, out Bias,out layersNeurons);
            /*
            NeuralNetwork.WEIGHTS = Weights;
            NeuralNetwork.Bias = Bias;
            NeuralNetwork.layers_neurons = layersNeurons;*/
            var str1 = string.Join(", ", layersNeurons.Select(o => o.ToString()).ToArray());
            NeuronCountTextBox.Text = str1;
        }
    }
}
/* int layer = 0;
                        int neuron = 0;
                        int conection = 0;
                        bool B = false;
                        line = "0001";
                        List<List<List<double>>> L = new List<List<List<double>>>();

                        while (layer<Int32.Parse("0"+line[3])) 
                        {
                            Console.WriteLine("L: " + line[3]);

                            line = sr.ReadLine();
                            L.Add(new List<List<double>>());
                            
                            while (neuron < Int32.Parse("0" + line[3]))
                            {
                                Console.WriteLine("N: " + line[3]);

                                L[layer].Add(new List<double>());
                                line = sr.ReadLine();                           
                                if (line[0] == 'B')
                                {
                                    B = true;
                                }

                                while (conection < Int32.Parse("0" + line[3]))
                                {
                                    Console.WriteLine("C: " + line[3]);

                                    line = sr.ReadLine();
                                    Console.WriteLine(line);
                                    L[layer][neuron].Add(Convert.ToDouble(line.Substring(line.IndexOf("_") + 1)));

                                    //L[layer][neuron][conection] = Convert.ToDouble(line.Substring(line.IndexOf("_") + 1));
                                    //Console.WriteLine(line);
                                    //Console.WriteLine(L[layer][neuron][conection]);
                                    conection++;
                                }
                                conection = 0;
                                neuron++;
                            }
                            neuron = 0;
                            layer++;
                        }

                        _weights = L.Select(a => a.Select(b=>b.ToArray()).ToArray()).ToArray();  //Pro každá a v L uděla "to array"
*/