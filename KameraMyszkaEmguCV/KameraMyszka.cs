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
        private readonly BlobCounter bc;
        /* shape coefficients */
        double compactness,
            blair,
            mal,
            malzmod,
            feret;
            
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
        void RefreshWindow(object sender, EventArgs arg) {

            //Pobieranie ramki
            image = capture.QueryFrame();
            imageBox1.Image = image;

            //YCbCr or Bgr(RGB)
            //Wartos zwrócicć uwagę na to że Ycc to Y,Cr,Cb a nie Y,Cb,Cr, oraz Bgr to Blue,Green,Red
            if (radioButton1.Checked)
                imageGray = image.Resize((double)nupScale.Value, INTER.CV_INTER_CUBIC).Convert<Ycc, Byte>().
                                  InRange(new Ycc((double)nudW1.Value, (double)nudW3.Value, (double)nudW2.Value), new Ycc((double)nudW4.Value, (double)nudW6.Value, (double)nudW5.Value));
            else
                imageGray = image.InRange(new Bgr((double)nudW3.Value, (double)nudW2.Value, (double)nudW1.Value), new Bgr((double)nudW6.Value, (double)nudW5.Value, (double)nudW4.Value));

            //imageGray = imageGray.Erode((int)nudErode.Value);
            //imageGray = imageGray.Dilate((int)nudDilate.Value);

            if (medianCB.Checked)
                imageGray = imageGray.SmoothMedian((int)nudMedian.Value);

            //Image<Gray, Byte> sgm = new Image<Gray, Byte>(imageGray.Size);
            Bitmap bmp = imageGray.ToBitmap();
            bc.ProcessImage(bmp);
            Blob[] bs = bc.GetObjectsInformation();
            if(bs.Length>0) {
                Blob blob = bs[0];
                IntPoint minXY, maxXY;
                PointsCloud.GetBoundingRectangle(bc.GetBlobsEdgePoints(blob), out minXY, out maxXY);
                Bitmap clonimage = (Bitmap)bmp.Clone();
                BitmapData data = bmp.LockBits(new Rectangle(0, 0, imageGray.Width, imageGray.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                Drawing.Rectangle(data, blob.Rectangle, Color.White);
                bmp.UnlockBits(data);

                //compactness = (double)blob.Area / (maxXY.X - minXY.X) / (maxXY.Y - minXY.Y);
                //Console.WriteLine("fullness " + (blob.Fullness - compactness));
                /* roznica max 0.004, wiec po co liczyc dwa razy */
                int x = maxXY.X - minXY.X, y = maxXY.Y - minXY.Y;
                int X = maxXY.X;
                int Y = maxXY.Y;
                compactness = blob.Fullness;
                    
                /* malinowska kryjaka liczy obwod ze wzoru na prostokąt, nasza liczy piksele krawedziowe */
                //Malinowska(i) = (2*bb(3)+2*bb(4))/(2*sqrt(pi*S)) - 1;
                mal = (double)(bc.GetBlobsEdgePoints(blob).Count) / 2 / Math.Sqrt(Math.PI*blob.Area) - 1;
                //MalinowskaZ(i) = 2*sqrt(pi*S)/(2*bb(3)+2*bb(4));
                malzmod = 2 * Math.Sqrt(Math.PI * blob.Area) / (double)(bc.GetBlobsEdgePoints(blob).Count);
                
                int gx = (int)blob.CenterOfGravity.X, gy = (int)blob.CenterOfGravity.Y;
                double blairsum = 0;
                int ftx = 0, ftxMax = 0;

                byte[,,] dd = imageGray.Data;
                for (int i=minXY.Y; i<Y; ++i) {
                    if (ftx > ftxMax) ftxMax = ftx;
                    ftx = 0;//bo moze sie zdazyc ze zliczy wiecej linii naraz, patrz: idealny prostokat
                    for (int j=minXY.X; j<X; ++j) {
                        if (dd[i, j, 0] != 0) {
                            ++ftx;
                            blairsum += (j - gx) * (j - gx) + (i - gy) * (i - gy);//distance squared
                        } else {
                            if(ftx > ftxMax) ftxMax = ftx;
                            ftx = 0;
                        }
                        dd[i, j, 0] = 255;
                    }
                }
                /* 
                 * aby policzyc ftyMax trzeba puscic jeszcze jedna petle tak aby wewnetrzna szla po y-kach
                 * ale mozna tez aproksymowac ftYmax przez boundingbox.Y, wtedy
                 * przewidywalem najwieksze rozbieznosci przy skosnych lub dziurawych obiektach;
                 * ale blad byl ponizej procenta, wiec szkoda tracic czas na kolejne O(n*n)
                 
                int fty = 0, ftyMax = 0;
                for (int j = minXY.X; j < X; ++j) {
                    if (fty > ftyMax) ftyMax = fty;
                    fty = 0;
                    for (int i = minXY.Y; i < Y; ++i)
                        if (dd[i, j, 0] != 0)
                            ++fty;
                        else {
                            if (fty > ftyMax) ftyMax = fty;
                            fty = 0;
                        }
                }*/
                // feret = (double)ftxMax / ftyMax;
                feret = (double)ftxMax / y;
               
                blair = (double)(blob.Area) / Math.Sqrt(2 * Math.PI * blairsum  );
                System.Console.WriteLine("mal {0:f}, zmal {1:f}, feret {2:f}, blair {3:f}", mal, malzmod, feret, blair);

                //Drukowanie wsp na gui
                compactnessLbl.Text = compactness.ToString();
                malinowskaLbl.Text = mal.ToString();
                malzmodLbl.Text = malzmod.ToString();
                feretLbl.Text = feret.ToString();
                blairLbl.Text = blair.ToString();

            } else {
                /* no blob detected */
                compactness = -404f;
                blair = -404f;
                mal = -404f;
                malzmod = -404f;
                feret = -404f;
            }

            imageBox2.Image = new Image<Gray, Byte>(bmp);
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

        /// <summary>
        /// Szybkie operacje na obrazie(dekrementacja)
        /// Nie wiem czemu dokładnie jest taka różnica w obliczeniach, ale jest ok 12x szybsza
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image<Bgr, Byte> test(Image<Bgr, Byte> image)
        {
            Image<Bgr, Byte> img = image.Copy();
            byte[, ,] data = img.Data;
                 for (int i = img.Rows - 1; i >= 0; --i)
                 {
                     for (int j = img.Cols - 1; j >= 0; --j)
                     {
                         data[i, j, 0] += 10;
                         data[i, j, 1] += 30;
                         data[i, j, 2] += 140;
                     }
                 }
                 
          //  Jak ktoś nie wierzy może potestować
         /*   for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    data[i, j, 0] += 10;

                    data[i, j, 1] += 30;
                    data[i, j, 2] += 140;
                }
            }*/
            return img;
        }

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
                nudW1.Value = (S1 - odchylenie > 0)?S1 - odchylenie:S1; 
                nudW2.Value = (S2 - odchylenie > 0)?S2 - odchylenie:S2;
                nudW3.Value = (S3 - odchylenie > 0)?S3 - odchylenie:S3;
                nudW4.Value = (S1 + odchylenie < 255)?S1 + odchylenie:S1;
                nudW5.Value = (S2 + odchylenie < 255)?S2 + odchylenie:S2;
                nudW6.Value = (S3 + odchylenie < 255)?S3 + odchylenie:S3;
            }
            else
            {
                S3 = (int) image[posY, posX].Blue;
                S2 = (int) image[posY, posX].Green;
                S1 = (int) image[posY, posX].Red;
                nudW1.Value = (S3 - odchylenie > 0)?S3 - odchylenie:S3;
                nudW2.Value = (S2 - odchylenie > 0)?S2 - odchylenie:S2;
                nudW3.Value = (S1 - odchylenie > 0)?S1 - odchylenie:S1;
                nudW4.Value = (S3 + odchylenie < 255)?S3 + odchylenie:S3;
                nudW5.Value = (S2 + odchylenie < 255)?S2 + odchylenie:S2;
                nudW6.Value = (S1 + odchylenie < 255)?S1 + odchylenie:S1;
            }

            //MessageBox.Show(string.Format("X: {0} Y: {1}\n{2}, {3}, {4}", posX, posY,S1,S2,S3));
        }
    }
}
