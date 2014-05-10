namespace KameraMyszkaEmguCV
{
    partial class KameraMyszka
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.nudW2 = new System.Windows.Forms.NumericUpDown();
            this.nudW3 = new System.Windows.Forms.NumericUpDown();
            this.nudW5 = new System.Windows.Forms.NumericUpDown();
            this.nudW6 = new System.Windows.Forms.NumericUpDown();
            this.nudW1 = new System.Windows.Forms.NumericUpDown();
            this.nudW4 = new System.Windows.Forms.NumericUpDown();
            this.nudBrightness = new System.Windows.Forms.NumericUpDown();
            this.nudContrast = new System.Windows.Forms.NumericUpDown();
            this.nudSharpness = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.medianCB = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.nudMedian = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.nudErode = new System.Windows.Forms.NumericUpDown();
            this.nudDilate = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.nudWhiteRed = new System.Windows.Forms.NumericUpDown();
            this.nudWhiteBlue = new System.Windows.Forms.NumericUpDown();
            this.nudSaturation = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudGamma = new System.Windows.Forms.NumericUpDown();
            this.nudGain = new System.Windows.Forms.NumericUpDown();
            this.nudHue = new System.Windows.Forms.NumericUpDown();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.autoCB = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.nupScale = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSharpness)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMedian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudErode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDilate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWhiteRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWhiteBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupScale)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.Location = new System.Drawing.Point(12, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(428, 286);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // imageBox2
            // 
            this.imageBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox2.Location = new System.Drawing.Point(446, 12);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(428, 286);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox2.TabIndex = 3;
            this.imageBox2.TabStop = false;
            // 
            // nudW2
            // 
            this.nudW2.Location = new System.Drawing.Point(193, 12);
            this.nudW2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudW2.Name = "nudW2";
            this.nudW2.Size = new System.Drawing.Size(52, 20);
            this.nudW2.TabIndex = 2;
            this.nudW2.Value = new decimal(new int[] {
            80,//155,
            0,
            0,
            0});
            // 
            // nudW3
            // 
            this.nudW3.Location = new System.Drawing.Point(251, 12);
            this.nudW3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudW3.Name = "nudW3";
            this.nudW3.Size = new System.Drawing.Size(52, 20);
            this.nudW3.TabIndex = 3;
            this.nudW3.Value = new decimal(new int[] {
            60,//30,
            0,
            0,
            0});
            // 
            // nudW5
            // 
            this.nudW5.Location = new System.Drawing.Point(193, 38);
            this.nudW5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudW5.Name = "nudW5";
            this.nudW5.Size = new System.Drawing.Size(52, 20);
            this.nudW5.TabIndex = 5;
            this.nudW5.Value = new decimal(new int[] {
            255,//225,
            0,
            0,
            0});
            // 
            // nudW6
            // 
            this.nudW6.Location = new System.Drawing.Point(251, 38);
            this.nudW6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudW6.Name = "nudW6";
            this.nudW6.Size = new System.Drawing.Size(52, 20);
            this.nudW6.TabIndex = 6;
            this.nudW6.Value = new decimal(new int[] {
            100,//120,
            0,
            0,
            0});
            // 
            // nudW1
            // 
            this.nudW1.Location = new System.Drawing.Point(135, 12);
            this.nudW1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudW1.Name = "nudW1";
            this.nudW1.Size = new System.Drawing.Size(52, 20);
            this.nudW1.TabIndex = 1;
            this.nudW1.Value = new decimal(new int[] {
            40,//1,
            0,
            0,
            0});
            // 
            // nudW4
            // 
            this.nudW4.Location = new System.Drawing.Point(135, 38);
            this.nudW4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudW4.Name = "nudW4";
            this.nudW4.Size = new System.Drawing.Size(52, 20);
            this.nudW4.TabIndex = 4;
            this.nudW4.Value = new decimal(new int[] {
            150,//254,
            0,
            0,
            0});
            // 
            // nudBrightness
            // 
            this.nudBrightness.DecimalPlaces = 2;
            this.nudBrightness.Enabled = false;
            this.nudBrightness.Location = new System.Drawing.Point(102, 104);
            this.nudBrightness.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBrightness.Name = "nudBrightness";
            this.nudBrightness.Size = new System.Drawing.Size(62, 20);
            this.nudBrightness.TabIndex = 10;
            this.nudBrightness.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
            // 
            // nudContrast
            // 
            this.nudContrast.DecimalPlaces = 2;
            this.nudContrast.Enabled = false;
            this.nudContrast.Location = new System.Drawing.Point(102, 130);
            this.nudContrast.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudContrast.Name = "nudContrast";
            this.nudContrast.Size = new System.Drawing.Size(62, 20);
            this.nudContrast.TabIndex = 11;
            this.nudContrast.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // nudSharpness
            // 
            this.nudSharpness.DecimalPlaces = 2;
            this.nudSharpness.Enabled = false;
            this.nudSharpness.Location = new System.Drawing.Point(102, 156);
            this.nudSharpness.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSharpness.Name = "nudSharpness";
            this.nudSharpness.Size = new System.Drawing.Size(62, 20);
            this.nudSharpness.TabIndex = 12;
            this.nudSharpness.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.nupScale);
            this.panel1.Controls.Add(this.medianCB);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.nudMedian);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.nudErode);
            this.panel1.Controls.Add(this.nudDilate);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.nudWhiteRed);
            this.panel1.Controls.Add(this.nudWhiteBlue);
            this.panel1.Controls.Add(this.nudSaturation);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.nudGamma);
            this.panel1.Controls.Add(this.nudGain);
            this.panel1.Controls.Add(this.nudHue);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.autoCB);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nudW1);
            this.panel1.Controls.Add(this.nudSharpness);
            this.panel1.Controls.Add(this.nudW2);
            this.panel1.Controls.Add(this.nudContrast);
            this.panel1.Controls.Add(this.nudW3);
            this.panel1.Controls.Add(this.nudBrightness);
            this.panel1.Controls.Add(this.nudW5);
            this.panel1.Controls.Add(this.nudW4);
            this.panel1.Controls.Add(this.nudW6);
            this.panel1.Location = new System.Drawing.Point(12, 304);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(862, 190);
            this.panel1.TabIndex = 13;
            // 
            // medianCB
            // 
            this.medianCB.AutoSize = true;
            this.medianCB.Location = new System.Drawing.Point(542, 70);
            this.medianCB.Name = "medianCB";
            this.medianCB.Size = new System.Drawing.Size(15, 14);
            this.medianCB.TabIndex = 39;
            this.medianCB.UseVisualStyleBackColor = true;
            this.medianCB.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(558, 69);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 13);
            this.label17.TabIndex = 38;
            this.label17.Text = "Filtr Medianowy:";
            // 
            // nudMedian
            // 
            this.nudMedian.Enabled = false;
            this.nudMedian.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudMedian.Location = new System.Drawing.Point(648, 66);
            this.nudMedian.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nudMedian.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudMedian.Name = "nudMedian";
            this.nudMedian.Size = new System.Drawing.Size(62, 20);
            this.nudMedian.TabIndex = 37;
            this.nudMedian.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(558, 43);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Erozja:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(558, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 13);
            this.label16.TabIndex = 35;
            this.label16.Text = "Dylatacja:";
            // 
            // nudErode
            // 
            this.nudErode.Location = new System.Drawing.Point(648, 38);
            this.nudErode.Name = "nudErode";
            this.nudErode.Size = new System.Drawing.Size(62, 20);
            this.nudErode.TabIndex = 34;
            // 
            // nudDilate
            // 
            this.nudDilate.Location = new System.Drawing.Point(648, 12);
            this.nudDilate.Name = "nudDilate";
            this.nudDilate.Size = new System.Drawing.Size(62, 20);
            this.nudDilate.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(431, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(121, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Operacje morfologiczne:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(299, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "WhiteBalanceRed:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(299, 132);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "WhiteBalanceBlue:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(299, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Saturation:";
            // 
            // nudWhiteRed
            // 
            this.nudWhiteRed.DecimalPlaces = 2;
            this.nudWhiteRed.Enabled = false;
            this.nudWhiteRed.Location = new System.Drawing.Point(402, 156);
            this.nudWhiteRed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudWhiteRed.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudWhiteRed.Name = "nudWhiteRed";
            this.nudWhiteRed.Size = new System.Drawing.Size(62, 20);
            this.nudWhiteRed.TabIndex = 28;
            this.nudWhiteRed.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // nudWhiteBlue
            // 
            this.nudWhiteBlue.DecimalPlaces = 2;
            this.nudWhiteBlue.Enabled = false;
            this.nudWhiteBlue.Location = new System.Drawing.Point(402, 130);
            this.nudWhiteBlue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudWhiteBlue.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudWhiteBlue.Name = "nudWhiteBlue";
            this.nudWhiteBlue.Size = new System.Drawing.Size(62, 20);
            this.nudWhiteBlue.TabIndex = 27;
            this.nudWhiteBlue.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // nudSaturation
            // 
            this.nudSaturation.DecimalPlaces = 2;
            this.nudSaturation.Enabled = false;
            this.nudSaturation.Location = new System.Drawing.Point(402, 104);
            this.nudSaturation.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSaturation.Name = "nudSaturation";
            this.nudSaturation.Size = new System.Drawing.Size(62, 20);
            this.nudSaturation.TabIndex = 26;
            this.nudSaturation.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(175, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Gamma:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(175, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Gain:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(175, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Hue:";
            // 
            // nudGamma
            // 
            this.nudGamma.DecimalPlaces = 2;
            this.nudGamma.Enabled = false;
            this.nudGamma.Location = new System.Drawing.Point(226, 156);
            this.nudGamma.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudGamma.Name = "nudGamma";
            this.nudGamma.Size = new System.Drawing.Size(62, 20);
            this.nudGamma.TabIndex = 22;
            this.nudGamma.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // nudGain
            // 
            this.nudGain.DecimalPlaces = 2;
            this.nudGain.Enabled = false;
            this.nudGain.Location = new System.Drawing.Point(226, 130);
            this.nudGain.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudGain.Name = "nudGain";
            this.nudGain.Size = new System.Drawing.Size(62, 20);
            this.nudGain.TabIndex = 21;
            this.nudGain.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // nudHue
            // 
            this.nudHue.DecimalPlaces = 2;
            this.nudHue.Enabled = false;
            this.nudHue.Location = new System.Drawing.Point(226, 104);
            this.nudHue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudHue.Name = "nudHue";
            this.nudHue.Size = new System.Drawing.Size(62, 20);
            this.nudHue.TabIndex = 20;
            this.nudHue.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(343, 36);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(41, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.Text = "Bgr";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(343, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(55, 17);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "YCbCr";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // autoCB
            // 
            this.autoCB.AutoSize = true;
            this.autoCB.Checked = true;
            this.autoCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoCB.Location = new System.Drawing.Point(117, 84);
            this.autoCB.Name = "autoCB";
            this.autoCB.Size = new System.Drawing.Size(47, 17);
            this.autoCB.TabIndex = 9;
            this.autoCB.Text = "auto";
            this.autoCB.UseVisualStyleBackColor = true;
            this.autoCB.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Sharpness:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Contrast:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Brightness:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Ustawienia kamery:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Do:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Od:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Wsp. binaryzacji:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(558, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 13);
            this.label18.TabIndex = 41;
            this.label18.Text = "Skala obrazka:";
            // 
            // nupScale
            // 
            this.nupScale.DecimalPlaces = 2;
            this.nupScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nupScale.Location = new System.Drawing.Point(648, 125);
            this.nupScale.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nupScale.Name = "nupScale";
            this.nupScale.Size = new System.Drawing.Size(62, 20);
            this.nupScale.TabIndex = 40;
            this.nupScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // KameraMyszka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 506);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.imageBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "KameraMyszka";
            this.Text = "KameraMyszka";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KameraMyszka_FormClosing);
            this.Load += new System.EventHandler(this.KameraMyszka_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSharpness)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMedian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudErode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDilate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWhiteRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWhiteBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupScale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.NumericUpDown nudW2;
        private System.Windows.Forms.NumericUpDown nudW3;
        private System.Windows.Forms.NumericUpDown nudW5;
        private System.Windows.Forms.NumericUpDown nudW6;
        private System.Windows.Forms.NumericUpDown nudW1;
        private System.Windows.Forms.NumericUpDown nudW4;
        private System.Windows.Forms.NumericUpDown nudBrightness;
        private System.Windows.Forms.NumericUpDown nudContrast;
        private System.Windows.Forms.NumericUpDown nudSharpness;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox autoCB;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudWhiteRed;
        private System.Windows.Forms.NumericUpDown nudWhiteBlue;
        private System.Windows.Forms.NumericUpDown nudSaturation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudGamma;
        private System.Windows.Forms.NumericUpDown nudGain;
        private System.Windows.Forms.NumericUpDown nudHue;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown nudMedian;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown nudErode;
        private System.Windows.Forms.NumericUpDown nudDilate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox medianCB;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nupScale;
    }
}

