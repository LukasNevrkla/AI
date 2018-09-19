namespace ArtificialIntelligenceEditor
{
    partial class Artificial_Intelligence_Editor
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Artificial_Intelligence_Editor));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.reload_button = new System.Windows.Forms.Button();
            this.LearnRateName = new System.Windows.Forms.Label();
            this.Epoch_text = new System.Windows.Forms.Label();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.LoadData_button = new System.Windows.Forms.Button();
            this.SaveData_button = new System.Windows.Forms.Button();
            this.TrainProgressBar = new System.Windows.Forms.ProgressBar();
            this.MomentumTextBox = new System.Windows.Forms.TextBox();
            this.MomentumLabel = new System.Windows.Forms.Label();
            this.LearnRateTextBox = new System.Windows.Forms.TextBox();
            this.DataSizeTextBox = new System.Windows.Forms.TextBox();
            this.Epoch_textBox = new System.Windows.Forms.TextBox();
            this.DataSizeLabel = new System.Windows.Forms.Label();
            this.SigmoidTextBox = new System.Windows.Forms.TextBox();
            this.InputComboBox = new System.Windows.Forms.ComboBox();
            this.InputName = new System.Windows.Forms.Label();
            this.NeuronCountTextBox = new System.Windows.Forms.TextBox();
            this.NeuronCountLabel = new System.Windows.Forms.Label();
            this.SigmoidKonstantLabel = new System.Windows.Forms.Label();
            this.ErrorChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.InputPanel = new System.Windows.Forms.Panel();
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChart)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.AutoScroll = true;
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.Location = new System.Drawing.Point(192, 8);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1169, 800);
            this.MainPanel.TabIndex = 0;
            this.MainPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainPanel_Scroll);
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // reload_button
            // 
            this.reload_button.BackColor = System.Drawing.Color.Transparent;
            this.reload_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.reload_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reload_button.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.reload_button.ForeColor = System.Drawing.Color.White;
            this.reload_button.Location = new System.Drawing.Point(11, 733);
            this.reload_button.Name = "reload_button";
            this.reload_button.Size = new System.Drawing.Size(130, 32);
            this.reload_button.TabIndex = 1;
            this.reload_button.Text = "Spustit BP";
            this.reload_button.UseVisualStyleBackColor = false;
            this.reload_button.Click += new System.EventHandler(this.reload_button_Click);
            // 
            // LearnRateName
            // 
            this.LearnRateName.AutoSize = true;
            this.LearnRateName.BackColor = System.Drawing.Color.Transparent;
            this.LearnRateName.Font = new System.Drawing.Font("Calibri", 12F);
            this.LearnRateName.ForeColor = System.Drawing.Color.White;
            this.LearnRateName.Location = new System.Drawing.Point(3, 0);
            this.LearnRateName.Name = "LearnRateName";
            this.LearnRateName.Size = new System.Drawing.Size(111, 19);
            this.LearnRateName.TabIndex = 3;
            this.LearnRateName.Text = "Rychlost učení: ";
            // 
            // Epoch_text
            // 
            this.Epoch_text.AutoSize = true;
            this.Epoch_text.Font = new System.Drawing.Font("Calibri", 12F);
            this.Epoch_text.ForeColor = System.Drawing.Color.White;
            this.Epoch_text.Location = new System.Drawing.Point(3, 38);
            this.Epoch_text.Name = "Epoch_text";
            this.Epoch_text.Size = new System.Drawing.Size(92, 19);
            this.Epoch_text.TabIndex = 4;
            this.Epoch_text.Text = "Počet epoch:";
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.Color.Transparent;
            this.ControlPanel.Controls.Add(this.LoadData_button);
            this.ControlPanel.Controls.Add(this.SaveData_button);
            this.ControlPanel.Controls.Add(this.TrainProgressBar);
            this.ControlPanel.Controls.Add(this.MomentumTextBox);
            this.ControlPanel.Controls.Add(this.MomentumLabel);
            this.ControlPanel.Controls.Add(this.LearnRateTextBox);
            this.ControlPanel.Controls.Add(this.DataSizeTextBox);
            this.ControlPanel.Controls.Add(this.Epoch_textBox);
            this.ControlPanel.Controls.Add(this.DataSizeLabel);
            this.ControlPanel.Controls.Add(this.SigmoidTextBox);
            this.ControlPanel.Controls.Add(this.InputComboBox);
            this.ControlPanel.Controls.Add(this.InputName);
            this.ControlPanel.Controls.Add(this.NeuronCountTextBox);
            this.ControlPanel.Controls.Add(this.NeuronCountLabel);
            this.ControlPanel.Controls.Add(this.SigmoidKonstantLabel);
            this.ControlPanel.Controls.Add(this.ErrorChart);
            this.ControlPanel.Controls.Add(this.LearnRateName);
            this.ControlPanel.Controls.Add(this.reload_button);
            this.ControlPanel.Controls.Add(this.Epoch_text);
            this.ControlPanel.Location = new System.Drawing.Point(1368, 15);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(204, 792);
            this.ControlPanel.TabIndex = 7;
            // 
            // LoadData_button
            // 
            this.LoadData_button.BackColor = System.Drawing.Color.Transparent;
            this.LoadData_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.LoadData_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoadData_button.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LoadData_button.ForeColor = System.Drawing.Color.White;
            this.LoadData_button.Location = new System.Drawing.Point(70, 675);
            this.LoadData_button.Name = "LoadData_button";
            this.LoadData_button.Size = new System.Drawing.Size(130, 32);
            this.LoadData_button.TabIndex = 21;
            this.LoadData_button.Text = "Načíst data";
            this.LoadData_button.UseVisualStyleBackColor = false;
            this.LoadData_button.Click += new System.EventHandler(this.LoadData_button_Click);
            // 
            // SaveData_button
            // 
            this.SaveData_button.BackColor = System.Drawing.Color.Transparent;
            this.SaveData_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveData_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveData_button.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SaveData_button.ForeColor = System.Drawing.Color.White;
            this.SaveData_button.Location = new System.Drawing.Point(70, 637);
            this.SaveData_button.Name = "SaveData_button";
            this.SaveData_button.Size = new System.Drawing.Size(130, 32);
            this.SaveData_button.TabIndex = 20;
            this.SaveData_button.Text = "Uložït data";
            this.SaveData_button.UseVisualStyleBackColor = false;
            this.SaveData_button.Click += new System.EventHandler(this.SaveData_button_Click);
            // 
            // TrainProgressBar
            // 
            this.TrainProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TrainProgressBar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.TrainProgressBar.Location = new System.Drawing.Point(11, 771);
            this.TrainProgressBar.Name = "TrainProgressBar";
            this.TrainProgressBar.Size = new System.Drawing.Size(184, 14);
            this.TrainProgressBar.TabIndex = 19;
            // 
            // MomentumTextBox
            // 
            this.MomentumTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MomentumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MomentumTextBox.Font = new System.Drawing.Font("Calibri", 12F);
            this.MomentumTextBox.ForeColor = System.Drawing.Color.White;
            this.MomentumTextBox.Location = new System.Drawing.Point(90, 21);
            this.MomentumTextBox.Name = "MomentumTextBox";
            this.MomentumTextBox.Size = new System.Drawing.Size(72, 20);
            this.MomentumTextBox.TabIndex = 17;
            this.MomentumTextBox.Text = "xxx";
            // 
            // MomentumLabel
            // 
            this.MomentumLabel.AutoSize = true;
            this.MomentumLabel.Font = new System.Drawing.Font("Calibri", 12F);
            this.MomentumLabel.ForeColor = System.Drawing.Color.White;
            this.MomentumLabel.Location = new System.Drawing.Point(3, 19);
            this.MomentumLabel.Name = "MomentumLabel";
            this.MomentumLabel.Size = new System.Drawing.Size(87, 19);
            this.MomentumLabel.TabIndex = 18;
            this.MomentumLabel.Text = "Momentum:";
            // 
            // LearnRateTextBox
            // 
            this.LearnRateTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LearnRateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LearnRateTextBox.Font = new System.Drawing.Font("Calibri", 12F);
            this.LearnRateTextBox.ForeColor = System.Drawing.Color.White;
            this.LearnRateTextBox.Location = new System.Drawing.Point(117, 3);
            this.LearnRateTextBox.Name = "LearnRateTextBox";
            this.LearnRateTextBox.Size = new System.Drawing.Size(45, 20);
            this.LearnRateTextBox.TabIndex = 2;
            this.LearnRateTextBox.Text = "xxx";
            // 
            // DataSizeTextBox
            // 
            this.DataSizeTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DataSizeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataSizeTextBox.Font = new System.Drawing.Font("Calibri", 12F);
            this.DataSizeTextBox.ForeColor = System.Drawing.Color.White;
            this.DataSizeTextBox.Location = new System.Drawing.Point(169, 190);
            this.DataSizeTextBox.Name = "DataSizeTextBox";
            this.DataSizeTextBox.Size = new System.Drawing.Size(36, 20);
            this.DataSizeTextBox.TabIndex = 16;
            this.DataSizeTextBox.Text = "xxx";
            // 
            // Epoch_textBox
            // 
            this.Epoch_textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Epoch_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Epoch_textBox.Font = new System.Drawing.Font("Calibri", 12F);
            this.Epoch_textBox.ForeColor = System.Drawing.Color.White;
            this.Epoch_textBox.Location = new System.Drawing.Point(101, 38);
            this.Epoch_textBox.Name = "Epoch_textBox";
            this.Epoch_textBox.Size = new System.Drawing.Size(70, 20);
            this.Epoch_textBox.TabIndex = 5;
            this.Epoch_textBox.Text = "xxx";
            // 
            // DataSizeLabel
            // 
            this.DataSizeLabel.AutoSize = true;
            this.DataSizeLabel.Font = new System.Drawing.Font("Calibri", 12F);
            this.DataSizeLabel.ForeColor = System.Drawing.Color.White;
            this.DataSizeLabel.Location = new System.Drawing.Point(7, 191);
            this.DataSizeLabel.Name = "DataSizeLabel";
            this.DataSizeLabel.Size = new System.Drawing.Size(156, 19);
            this.DataSizeLabel.TabIndex = 15;
            this.DataSizeLabel.Text = "Prvky MNIST databáze:";
            // 
            // SigmoidTextBox
            // 
            this.SigmoidTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SigmoidTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SigmoidTextBox.Font = new System.Drawing.Font("Calibri", 12F);
            this.SigmoidTextBox.ForeColor = System.Drawing.Color.White;
            this.SigmoidTextBox.Location = new System.Drawing.Point(124, 58);
            this.SigmoidTextBox.Name = "SigmoidTextBox";
            this.SigmoidTextBox.Size = new System.Drawing.Size(70, 20);
            this.SigmoidTextBox.TabIndex = 11;
            this.SigmoidTextBox.Text = "xxx";
            // 
            // InputComboBox
            // 
            this.InputComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.InputComboBox.Font = new System.Drawing.Font("Calibri", 10F);
            this.InputComboBox.ForeColor = System.Drawing.Color.White;
            this.InputComboBox.FormattingEnabled = true;
            this.InputComboBox.Location = new System.Drawing.Point(61, 163);
            this.InputComboBox.Name = "InputComboBox";
            this.InputComboBox.Size = new System.Drawing.Size(121, 23);
            this.InputComboBox.TabIndex = 0;
            this.InputComboBox.SelectedIndexChanged += new System.EventHandler(this.InputComboBox_SelectedIndexChanged);
            // 
            // InputName
            // 
            this.InputName.AutoSize = true;
            this.InputName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.InputName.ForeColor = System.Drawing.Color.White;
            this.InputName.Location = new System.Drawing.Point(7, 165);
            this.InputName.Name = "InputName";
            this.InputName.Size = new System.Drawing.Size(48, 17);
            this.InputName.TabIndex = 14;
            this.InputName.Text = "Vstup:";
            // 
            // NeuronCountTextBox
            // 
            this.NeuronCountTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NeuronCountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NeuronCountTextBox.Font = new System.Drawing.Font("Calibri", 12F);
            this.NeuronCountTextBox.ForeColor = System.Drawing.Color.White;
            this.NeuronCountTextBox.Location = new System.Drawing.Point(7, 144);
            this.NeuronCountTextBox.Name = "NeuronCountTextBox";
            this.NeuronCountTextBox.Size = new System.Drawing.Size(188, 20);
            this.NeuronCountTextBox.TabIndex = 13;
            this.NeuronCountTextBox.Text = "-";
            // 
            // NeuronCountLabel
            // 
            this.NeuronCountLabel.AutoSize = true;
            this.NeuronCountLabel.Font = new System.Drawing.Font("Calibri", 12F);
            this.NeuronCountLabel.ForeColor = System.Drawing.Color.White;
            this.NeuronCountLabel.Location = new System.Drawing.Point(7, 123);
            this.NeuronCountLabel.Name = "NeuronCountLabel";
            this.NeuronCountLabel.Size = new System.Drawing.Size(105, 19);
            this.NeuronCountLabel.TabIndex = 12;
            this.NeuronCountLabel.Text = "Počty neuronů:";
            // 
            // SigmoidKonstantLabel
            // 
            this.SigmoidKonstantLabel.AutoSize = true;
            this.SigmoidKonstantLabel.Font = new System.Drawing.Font("Calibri", 12F);
            this.SigmoidKonstantLabel.ForeColor = System.Drawing.Color.White;
            this.SigmoidKonstantLabel.Location = new System.Drawing.Point(3, 57);
            this.SigmoidKonstantLabel.Name = "SigmoidKonstantLabel";
            this.SigmoidKonstantLabel.Size = new System.Drawing.Size(120, 19);
            this.SigmoidKonstantLabel.TabIndex = 10;
            this.SigmoidKonstantLabel.Text = "Náklon sigmoidy:";
            // 
            // ErrorChart
            // 
            this.ErrorChart.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 6;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LabelAutoFitMaxFontSize = 6;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.ErrorChart.ChartAreas.Add(chartArea1);
            this.ErrorChart.Location = new System.Drawing.Point(-1, 322);
            this.ErrorChart.Name = "ErrorChart";
            series1.BorderColor = System.Drawing.Color.White;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.White;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F);
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Name = "Series1";
            this.ErrorChart.Series.Add(series1);
            this.ErrorChart.Size = new System.Drawing.Size(201, 213);
            this.ErrorChart.TabIndex = 8;
            this.ErrorChart.Text = "chart1";
            title1.BorderWidth = 2;
            title1.ForeColor = System.Drawing.Color.White;
            title1.Name = "Title1";
            title1.ShadowOffset = 1;
            title1.Text = "Celková chyba";
            this.ErrorChart.Titles.Add(title1);
            // 
            // InputPanel
            // 
            this.InputPanel.BackColor = System.Drawing.Color.Transparent;
            this.InputPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.InputPanel.Location = new System.Drawing.Point(13, 9);
            this.InputPanel.Name = "InputPanel";
            this.InputPanel.Size = new System.Drawing.Size(173, 799);
            this.InputPanel.TabIndex = 8;
            // 
            // Artificial_Intelligence_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.BackgroundImage = global::ArtificialIntelligenceEditor.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(1584, 820);
            this.Controls.Add(this.InputPanel);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.MainPanel);
            this.Font = new System.Drawing.Font("Modern No. 20", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Artificial_Intelligence_Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Umělá inteligence";
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button reload_button;
        private System.Windows.Forms.Label LearnRateName;
        private System.Windows.Forms.Label Epoch_text;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart ErrorChart;
        private System.Windows.Forms.Label SigmoidKonstantLabel;
        private System.Windows.Forms.TextBox NeuronCountTextBox;
        private System.Windows.Forms.Label NeuronCountLabel;
        private System.Windows.Forms.ComboBox InputComboBox;
        private System.Windows.Forms.Label InputName;
        private System.Windows.Forms.TextBox DataSizeTextBox;
        private System.Windows.Forms.Label DataSizeLabel;
        private System.Windows.Forms.Panel InputPanel;
        private System.Windows.Forms.Label MomentumLabel;
        private System.Windows.Forms.TextBox SigmoidTextBox;
        private System.Windows.Forms.TextBox Epoch_textBox;
        private System.Windows.Forms.TextBox MomentumTextBox;
        private System.Windows.Forms.TextBox LearnRateTextBox;
        private System.Windows.Forms.ProgressBar TrainProgressBar;
        private System.Windows.Forms.Button LoadData_button;
        private System.Windows.Forms.Button SaveData_button;
    }
}

