using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace KameraMyszkaEmguCV
{
    public partial class KameraMyszka : Form
    {
        Capture capture = null;
        Image<Bgr, Byte> image;
        Image<Gray, Byte> imageGray;

        double defaultBrightness, defaultContrast, defaultSharpness;
        double defaultSaturation, defaultWhiteBlueBalance, defaultWhiteRedBalance, defaultHue, defaultGain, defaultGamma;

        public KameraMyszka()
        {
            InitializeComponent();            
        }

        /*
         * Zapisanie domyślnych ustawień kamery i uruchomienie odbioru obrazu
         * */
        private void KameraMyszka_Load(object sender, EventArgs e)
        {
            try
            {
                capture = new Capture();
                defaultBrightness = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS);
                defaultContrast = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST);
                defaultSharpness = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SHARPNESS);
                defaultSaturation = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SATURATION);
                defaultWhiteBlueBalance = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_BLUE_U);
                defaultWhiteRedBalance = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_RED_V);
                defaultHue = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_HUE);
                defaultGain = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAIN);
                defaultGamma = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAMMA);
                nudBrightness.Value = (decimal)defaultBrightness;
                nudContrast.Value = (decimal)defaultContrast;
                nudSharpness.Value = (decimal)defaultSharpness;
                nudSaturation.Value = (decimal)defaultSaturation;
                nudWhiteBlue.Value = (decimal)defaultWhiteBlueBalance;
                nudWhiteRed.Value = (decimal)defaultWhiteRedBalance;
                nudHue.Value = (decimal)defaultHue;
                nudGain.Value = (decimal)defaultGain;
                nudGamma.Value = (decimal)defaultGamma;
                Application.Idle += RefreshWindow;
            }
            catch (NullReferenceException ex) { Console.WriteLine("ERROR!"+ex.StackTrace); } 
        }

        /*
         * Odświerzanie okna z obrazem
         * */
        void RefreshWindow(object sender, EventArgs arg)
        {           
            //Pobieranie ramki
            image = capture.QueryFrame();
            //imageBox1.Image = image;

            //YCbCr or Bgr(RGB)
            if (radioButton1.Checked) imageGray = image.Resize((double)nupScale.Value,INTER.CV_INTER_CUBIC).Convert<Ycc, Byte>().InRange(new Ycc((double)nudW1.Value, (double)nudW3.Value, (double)nudW2.Value), new Ycc((double)nudW4.Value, (double)nudW6.Value, (double)nudW5.Value));
            else imageGray = image.InRange(new Bgr((double)nudW3.Value, (double)nudW2.Value, (double)nudW1.Value), new Bgr((double)nudW6.Value, (double)nudW5.Value, (double)nudW4.Value));

            imageGray = imageGray.Dilate((int)nudDilate.Value);
            imageGray = imageGray.Erode((int)nudErode.Value);
            if (medianCB.Checked) imageGray = imageGray.SmoothMedian((int)nudMedian.Value);
            imageBox1.Image = imageGray;
            imageBox2.Image = OnlyHandOnImage(imageGray);
            //imageBox1.Image = test(image);
        }

        /*
         * Włączenie ręcznego ustawiania kamery
         * */
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            nudBrightness.Enabled = !nudBrightness.Enabled;
            nudContrast.Enabled = !nudContrast.Enabled;
            nudSharpness.Enabled = !nudSharpness.Enabled;
            nudSaturation.Enabled = !nudSaturation.Enabled;
            nudWhiteBlue.Enabled = !nudWhiteBlue.Enabled;
            nudWhiteRed.Enabled = !nudWhiteRed.Enabled;
            nudHue.Enabled = !nudHue.Enabled;
            nudGain.Enabled = !nudGain.Enabled;
            nudGamma.Enabled = !nudGamma.Enabled;

            //Camera Settings
            if (!autoCB.Checked)
            {
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS, (double)nudBrightness.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST, (double)nudContrast.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SHARPNESS, (double)nudSharpness.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SATURATION, (double)nudSaturation.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_BLUE_U, (double)nudWhiteBlue.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_RED_V, (double)nudWhiteRed.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_HUE, (double)nudHue.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAIN, (double)nudGain.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAMMA, (double)nudGamma.Value);
            }
            else
            {
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS, defaultBrightness);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST, defaultContrast);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SHARPNESS, defaultSharpness);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SATURATION, defaultSaturation);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_BLUE_U, defaultWhiteBlueBalance);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_RED_V, defaultWhiteRedBalance);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_HUE, defaultHue);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAIN, defaultGain);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAMMA, defaultGamma);
            }
        }

        /*
         * Włączenie filtru medianowego
         * */
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            nudMedian.Enabled = !nudMedian.Enabled;
        }

        /**
         * Powrót do defaultowych ustawień kamery po zamknięciu programu
         * */
        private void KameraMyszka_FormClosing(object sender, FormClosingEventArgs e)
        {
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS, defaultBrightness);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST, defaultContrast);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SHARPNESS, defaultSharpness);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SATURATION, defaultSaturation);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_BLUE_U, defaultWhiteBlueBalance);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_RED_V, defaultWhiteRedBalance);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_HUE, defaultHue);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAIN, defaultGain);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAMMA, defaultGamma);
        }

        private Image<Gray, Byte> OnlyHandOnImage(Image<Gray, Byte> image)
        {
            Image<Gray, Byte> img = image.Copy();
            int rows = img.Rows;
            int cols = img.Cols;
            Console.WriteLine(rows+" "+cols);
            byte[, ,] data = img.Data;

            //Indeksacja
            int[,] imgIndex = new int[cols , rows];
            for (int i = 0; i < cols; i++)
			{
			    imgIndex[i,0] = 0;
                imgIndex[i,rows-1] = 0;
			}
            for (int j = 0; j < rows; j++)
            {
                imgIndex[0, j] = 0;
                imgIndex[cols - 1, j] = 0;
            }
            int precission = 2000;
            int[] tablica_sklejen = new int[precission]; //Może być za mała w skrajnych przypadkach //TODO test
            for (int i = 0; i < tablica_sklejen.Length; i++)
                tablica_sklejen[i] = i+1;
            int actualIndex = 1;

            //Pierwszy przebieg indeksacji
            for (int j = 1; j < rows-1; j++)
                for (int i = 1; i < cols-1; i++)
                {
                    if (data[j, i, 0] == 0) imgIndex[i, j] = 0;
                    else
                    {
                        int temp = precission;
                        if (imgIndex[i - 1, j] != 0 && imgIndex[i - 1, j] < temp)
                        {
                            if (temp != precission) tablica_sklejen[temp - 1] = imgIndex[i - 1, j];
                            temp = imgIndex[i - 1, j];
                        }
                        if (imgIndex[i - 1, j - 1] != 0 && imgIndex[i - 1, j - 1] < temp)
                        {
                            if (temp != precission) tablica_sklejen[temp - 1] = imgIndex[i - 1, j - 1];
                            temp = imgIndex[i - 1, j - 1];
                        }
                        if (imgIndex[i, j - 1] != 0 && imgIndex[i, j - 1] < temp)
                        {
                            if (temp != precission) tablica_sklejen[temp - 1] = imgIndex[i, j - 1];
                            temp = imgIndex[i, j - 1];
                        }
                        if (imgIndex[i + 1, j - 1] != 0 && imgIndex[i + 1, j - 1] < temp)
                        {
                            if (temp != precission) tablica_sklejen[temp - 1] = imgIndex[i + 1, j - 1];
                            temp = imgIndex[i + 1, j - 1];
                        }
                        if (temp == precission)
                        {
                            temp = actualIndex;
                            actualIndex++;
                        }
                        imgIndex[i, j] = temp;
                    }
                }

            //Drugi przebieg indeksacji - tablica sklejeń i zliczanie wielkości obiektów
            int[] tablica_wielkosci = new int[precission];
            for (int i = 0; i < tablica_wielkosci.Length; i++)
                tablica_wielkosci[i] = 0;

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                {
                    if (imgIndex[i, j] != 0)
                    {
                        imgIndex[i, j] = tablica_sklejen[imgIndex[i, j] - 1];
                        tablica_wielkosci[imgIndex[i, j] - 1]++;
                    }
                }

            ////Wyszukanie największego obiektu
            int index_max = 0;
            int actual_max = 0;

            for (int i = 0; i < tablica_wielkosci.Length; i++)
                if (tablica_wielkosci[i] > actual_max)
                {
                    actual_max = tablica_wielkosci[i];
                    index_max = i;
                }

            //Tworzenie binarnego obrazka z samym największym obiektem(ręką)
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                {
                    if (imgIndex[i, j] == index_max + 1) data[j, i, 0] = 255;
                    else data[j, i, 0] = 0;
                    //if(data[j,i,0] != 0)data[j, i, 0] = (Byte)(imgIndex[i, j]/(actual_max+1)*200+55);
                }
            return img;
        }

        /// <summary>
        /// Szybkie operacje na obrazie(rows i cols pobrane raz)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        /// 
        private Image<Bgr, Byte> test(Image<Bgr, Byte> image)
        {
            Image<Bgr, Byte> img = image.Copy();
            byte[, ,] data = img.Data;
            int rows = img.Rows;
            int cols = img.Cols;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j, 0] += 10;

                    data[i, j, 1] += 30;
                    data[i, j, 2] += 140;
                }
            }
            return img;
        }
    }
}
