﻿using System;
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

using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using AForge.Math.Geometry;

namespace KameraMyszkaEmguCV
{
    public partial class KameraMyszka : Form
    {
        /* wspolczynniki ksztaltu okreslonych gestow, kolejno:
           compactness, blair, mal, malzmod, feret */
        private readonly double[] 
            slayer  = {.2603, .6256, .8372, .5443, .2808},
            fist    = {.4279, .7861, .4785, .6764, .4435},
            victory = {.2843, .6215, .8111, .5521, .3609},
            vopen   = {.3178, .6015, .6788, .5957, .3768},
            hopen   = {.5899, .8121, .4594, .6852, 2.096};
        private const int COEFF_COUNT = 5;

        private int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        private double sensitivity = 1;

        private Hotkeys globalHotkeys;

        private bool blockMouseControl = false;

        private enum G {
            COMPACT, BLAIR, MAL, MALZMOD, FERET
        }

        private readonly BlobCounter bc;
        
        /* shape coefficients */
        double[,] observed;
            
        Capture capture = null;
        Image<Bgr, Byte> image;
        Image<Gray, Byte> imageGray;

        double defaultBrightness, defaultContrast, defaultSharpness;
        double defaultSaturation, defaultWhiteBlueBalance, defaultWhiteRedBalance, defaultHue, defaultGain, defaultGamma;

        public KameraMyszka()
        {
            bc = new BlobCounter();
            bc.FilterBlobs = true;
            bc.MinWidth = 50;
            bc.MinHeight = 50;
            bc.ObjectsOrder = ObjectsOrder.Size;

            /* coefficients for each hand */
            observed = new double[COEFF_COUNT, 2];
            /*compactness = new double[2];
            blair = new double[2];
            mal = new double[2];
            malzmod = new double[2];
            feret = new double[2];*/
            
            InitializeComponent();
            globalHotkeys = new Hotkeys(HotkeysConstants.CTRL + HotkeysConstants.ALT + HotkeysConstants.SHIFT, Keys.B, this);
        }

        /* Zapisanie domyślnych ustawień kamery i uruchomienie odbioru obrazu */
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
                try {
                    nudWhiteBlue.Value = (decimal)defaultWhiteBlueBalance;
                } catch (ArgumentOutOfRangeException ex) { }
 
                nudWhiteRed.Value = (decimal)defaultWhiteRedBalance;
                nudHue.Value = (decimal)defaultHue;
                nudGain.Value = (decimal)defaultGain;
                nudGamma.Value = (decimal)defaultGamma;
                Application.Idle += RefreshWindow;
            }
            catch (NullReferenceException ex) { Console.WriteLine("ERROR! "+ex.StackTrace); }

            globalHotkeys.Register();
        }

        /* Odświezanie okna z obrazem */
        void RefreshWindow(object sender, EventArgs arg) {

            //Pobieranie ramki
            image = capture.QueryFrame();
            imageBox1.Image = image;

            //YCbCr or Bgr(RGB)
            //Warto zwrócić uwagę na to że Ycc to Y,Cr,Cb a nie Y,Cb,Cr, oraz Bgr to Blue,Green,Red
            if (radioButton1.Checked)
                imageGray = image.Resize((double)nupScale.Value, INTER.CV_INTER_CUBIC).Convert<Ycc, Byte>().
                                  InRange(new Ycc((double)nudW1.Value, (double)nudW3.Value, (double)nudW2.Value), new Ycc((double)nudW4.Value, (double)nudW6.Value, (double)nudW5.Value));
            else
                imageGray = image.InRange(new Bgr((double)nudW3.Value, (double)nudW2.Value, (double)nudW1.Value), new Bgr((double)nudW6.Value, (double)nudW5.Value, (double)nudW4.Value));

            if (medianCB.Checked)
                imageGray = imageGray.SmoothMedian((int)nudMedian.Value);

            //Image<Gray, Byte> sgm = new Image<Gray, Byte>(imageGray.Size);
            Bitmap bmp = imageGray.ToBitmap();
            bc.ProcessImage(bmp);

            Blob[] blob = bc.GetObjectsInformation();
            //TODO make work for both hands
            //lewa reka to ta z prawej strony obrazu (zwierciadlo), nie zakladamy ze user gestykuluje na krzyz, keep it simple
            int iters = bc.ObjectsCount > 2 ? 2 : bc.ObjectsCount;
            //int iters = bc.ObjectsCount > 1 ? 1 : bc.ObjectsCount;

            int centerOfGravityLHandX = 0, centerOfGravityLHandY = 0, centerOfGravityRHandX = 0, centerOfGravityRHandY = 0;
            Dictionary<double,string> gestures;

            string[] gestureTable = new string[2];
            int i = 0;
            for (; i < iters; ++i) {
                IntPoint minXY, maxXY;
                PointsCloud.GetBoundingRectangle(bc.GetBlobsEdgePoints(blob[i]), out minXY, out maxXY);
                Bitmap clonimage = (Bitmap)bmp.Clone();
                BitmapData data = bmp.LockBits(new Rectangle(0, 0, imageGray.Width, imageGray.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                Drawing.Rectangle(data, blob[i].Rectangle, Color.White);
                bmp.UnlockBits(data);

                int X = maxXY.X, Y = maxXY.Y;
                int x = minXY.X, y = minXY.Y;
                int blobh = Y - y;

                observed[0,i] = blob[i].Fullness;

                /* malinowska kryjaka liczy obwod ze wzoru na prostokąt, nasza liczy piksele krawedziowe */
                //Malinowska(i) = (2*bb(3)+2*bb(4))/(2*sqrt(pi*S)) - 1;
                observed[2,i] = (double)(bc.GetBlobsEdgePoints(blob[i]).Count) / 2 / Math.Sqrt(Math.PI * blob[i].Area) - 1;
                //MalinowskaZ(i) = 2*sqrt(pi*S)/(2*bb(3)+2*bb(4));
                observed[3,i] = 2 * Math.Sqrt(Math.PI * blob[i].Area) / (double)(bc.GetBlobsEdgePoints(blob[i]).Count);

                int gx = (int)blob[i].CenterOfGravity.X, gy = (int)blob[i].CenterOfGravity.Y;
                
                //Sprawdzenie która ręka prawa, a która lewa
                if (gx > centerOfGravityRHandX)
                {
                    centerOfGravityLHandX = centerOfGravityRHandX;
                    centerOfGravityLHandY = centerOfGravityRHandY;
                    centerOfGravityRHandX = gx;
                    centerOfGravityRHandY = gy;
                }
                else
                {
                    centerOfGravityLHandX = gx;
                    centerOfGravityLHandY = gy;
                }

                double blairsum = 0;
                int ftx = 0, ftxMax = 0;

                byte[, ,] dd = imageGray.Data;
                for (int j = y; j < Y; ++j) {
                    if (ftx > ftxMax) ftxMax = ftx;
                    ftx = 0;//bo moze sie zdazyc ze zliczy wiecej linii naraz, patrz: idealny prostokat
                    for (int k = x; k < X; ++k) {
                        if (dd[j, k, 0] != 0) {
                            ++ftx;
                            blairsum += (k - gx) * (k - gx) + (j - gy) * (j - gy);//distance squared
                        } else {
                            if (ftx > ftxMax) ftxMax = ftx;
                            ftx = 0;
                        }
                        dd[j, k, 0] = 255;
                    }
                }
                /*    aby policzyc ftyMax trzeba puscic jeszcze jedna petle tak aby wewnetrzna szla po y-kach
                    * ale mozna tez aproksymowac ftYmax przez boundingbox.Y, wtedy
                    * przewidywalem najwieksze rozbieznosci przy skosnych lub dziurawych obiektach;
                    * ale blad byl ponizej procenta, wiec szkoda tracic czas na kolejne O(n*n)
                 
                int fty = 0, ftyMax = 0;
                for (int j = x; j < X; ++j) {
                    if (fty > ftyMax) ftyMax = fty;
                    fty = 0;
                    for (int i = y; i < Y; ++i)
                        if (dd[i, j, 0] != 0)
                            ++fty;
                        else {
                            if (fty > ftyMax) ftyMax = fty;
                            fty = 0;
                        }
                }
                feret = (double)ftxMax / ftyMax; */
                observed[4,i] = (double)ftxMax / blobh;//feret
                observed[1,i] = (double)(blob[i].Area) / Math.Sqrt(2 * Math.PI * blairsum);//blair
                //System.Console.WriteLine("mal {0:f}, zmal {1:f}, feret {2:f}, blair {3:f}", mal, malzmod, feret, blair);

                //Drukowanie wsp na gui
                //TODO osobne pola dla obu obiektów (dłoni)

                                              

                Dictionary<string, double> gestChance = new Dictionary<string, double>();
                gestChance.Add("slayer", dist(slayer, i));
                gestChance.Add("fist", dist(fist, i));
                gestChance.Add("victory", dist(victory, i));
                gestChance.Add("vopen", dist(vopen, i));
                gestChance.Add("hopen", dist(hopen, i));
                //fold jak od matyasika - get key of minimal value

                
                string gesture = gestChance.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                gestureTable[i] = gesture;
            }

                g1value.Text = gestureTable[0];
                g2value.Text = gestureTable[1];

                compactnessLbl.Text = observed[0, 0].ToString().Length > 6 ? observed[0, 0].ToString().Substring(0, 6) : observed[0, 0].ToString();
                blairLbl.Text = observed[1, 0].ToString().Length > 6 ? observed[1, 0].ToString().Substring(0, 6) : observed[1, 0].ToString();
                malinowskaLbl.Text = observed[2, 0].ToString().Length > 6 ? observed[2, 0].ToString().Substring(0, 6) : observed[2, 0].ToString();
                malzmodLbl.Text = observed[3, 0].ToString().Length > 6 ? observed[3, 0].ToString().Substring(0, 6) : observed[3, 0].ToString();
                feretLbl.Text = observed[4, 0].ToString().Length > 6 ? observed[4, 0].ToString().Substring(0, 6) : observed[4, 0].ToString();

                comp2.Text = observed[0, 1].ToString().Length > 6 ? observed[0, 1].ToString().Substring(0, 6) : observed[0, 1].ToString();
                blair2.Text = observed[1, 1].ToString().Length > 6 ? observed[1, 1].ToString().Substring(0, 6) : observed[1, 1].ToString();
                mal2.Text = observed[2, 1].ToString().Length > 6 ? observed[2, 1].ToString().Substring(0, 6) : observed[2, 1].ToString();
                malz2.Text = observed[3, 1].ToString().Length > 6 ? observed[3, 1].ToString().Substring(0, 6) : observed[3, 1].ToString();
                feret2.Text = observed[4, 1].ToString().Length > 6 ? observed[4, 1].ToString().Substring(0, 6) : observed[4, 1].ToString();


            for (; i < 2; ++i) {
                /* for blobs not detected */
                observed[0, i] = observed[1, i] = observed[2, i] = observed[3, i] = observed[4, i] = -404f;
                //compactness[i] = blair[i] = mal[i] = malzmod[i] = feret[i] = -404f;
            }

            imageGray = new Image<Gray, Byte>(bmp);
            imageGray = imageGray.Erode((int)nudErode.Value);
            imageGray = imageGray.Dilate((int)nudDilate.Value);
            imageBox2.Image = imageGray;

            //Zmiana pozycji myszki od środka ciężkości lewej ręki
            if(centerOfGravityRHandX != 0 && centerOfGravityRHandY != 0 && !blockMouseControl)
                MouseSimulating.SetMousePosition((int)((((double)centerOfGravityRHandX / (double)(imageGray.Width-(100-sensitivity))) * (double)screenWidth)), (int)(sensitivity * (((double)centerOfGravityRHandY / (double)imageGray.Height) * (double)screenHeight)));
            
        }
        private double dist(double[] gesture, int idx) {
            double d = 0;
            //double[] observed = { compactness[idx], blair[idx], mal[idx], malzmod[idx], feret[idx] };
            for (int i = 0; i < COEFF_COUNT; ++i) {
                d += (observed[i,idx] - gesture[i]) * (observed[i,idx] - gesture[i]);
            }
            return d;
        }

        /*
         * Włączenie ręcznego ustawiania kamery
         * */
        private void autoCB_CheckedChanged(object sender, EventArgs e)
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

            if (autoCB.Checked)
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
            else CameraValueChanged(null, null);
        }

        private void CameraValueChanged(object sender, EventArgs e)
        {
            //Camera Settings
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

        /*
         * Włączenie filtru medianowego
         * */
        private void medianCB_CheckedChanged(object sender, EventArgs e)
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
            globalHotkeys.Unregiser();
        }

        /// <summary>
        /// Ustawienie progów binaryzacji poprzez kliknięcie na obrazie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs ev = (MouseEventArgs)e;
            int posX = (int)((double)ev.X / (double)imageBox1.Width * (double)image.Width);
            int posY = (int)((double)ev.Y / (double)imageBox1.Height * (double)image.Height);
            int S1, S2, S3;
            int odchylenie = 10; //TODO można zmienić wedle uznań tą 10 na co innego
            if (radioButton1.Checked)
            {
                Image<Ycc, Byte> temp = image.Convert<Ycc, Byte>();
                S1 = (int) temp[posY, posX].Y;
                S2 = (int)temp[posY, posX].Cb;
                S3 = (int)temp[posY, posX].Cr;
                nudW1.Value = (S1 - odchylenie > 0)?S1 - odchylenie:0; 
                nudW2.Value = (S2 - odchylenie > 0)?S2 - odchylenie:0;
                nudW3.Value = (S3 - odchylenie > 0)?S3 - odchylenie:0;
                nudW4.Value = (S1 + odchylenie < 255)?S1 + odchylenie:255;
                nudW5.Value = (S2 + odchylenie < 255)?S2 + odchylenie:255;
                nudW6.Value = (S3 + odchylenie < 255)?S3 + odchylenie:255;
            }
            else
            {
                S3 = (int) image[posY, posX].Blue;
                S2 = (int) image[posY, posX].Green;
                S1 = (int) image[posY, posX].Red;
                nudW1.Value = (S3 - odchylenie > 0)?S3 - odchylenie:0;
                nudW2.Value = (S2 - odchylenie > 0)?S2 - odchylenie:0;
                nudW3.Value = (S1 - odchylenie > 0)?S1 - odchylenie:0;
                nudW4.Value = (S3 + odchylenie < 255)?S3 + odchylenie:255;
                nudW5.Value = (S2 + odchylenie < 255)?S2 + odchylenie:255;
                nudW6.Value = (S1 + odchylenie < 255)?S1 + odchylenie:255;
            }
            //MessageBox.Show(string.Format("X: {0} Y: {1}\n{2}, {3}, {4}", posX, posY,S1,S2,S3));
        }

        private void returnToDefault_Click(object sender, EventArgs e)
        {
            //TODO
        }

        /// <summary>
        /// Obsługa globalnych klawiszy CTRL+ALT+SHIFT+B
        /// </summary>
        private void HandleHotkeyCtAlShB()
        {
            blockMouseControl = !blockMouseControl;
        }

        /// <summary>
        /// Nadpisanie funkcji odbioru skrótów klawiczowych globalnych
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == HotkeysConstants.WM_HOTKEY_MSG_ID)
                HandleHotkeyCtAlShB();
            base.WndProc(ref m);
        }

    }
}
