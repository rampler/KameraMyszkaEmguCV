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
            //Camera Settings
            if (!autoCB.Checked) {
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS, (double)nudBrightness.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST, (double)nudContrast.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SHARPNESS, (double)nudSharpness.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SATURATION, (double)nudSaturation.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_BLUE_U, (double)nudWhiteBlue.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_WHITE_BALANCE_RED_V, (double)nudWhiteRed.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_HUE, (double)nudHue.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAIN, (double)nudGain.Value);
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_GAMMA, (double)nudGamma.Value);
            } else {
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

            //Pobieranie ramki
            image = capture.QueryFrame();
            imageBox1.Image = image;

            //YCbCr or Bgr(RGB)
            if (radioButton1.Checked)
                imageGray = image.Resize((double)nupScale.Value, INTER.CV_INTER_CUBIC).Convert<Ycc, Byte>().
                                  InRange(new Ycc((double)nudW1.Value, (double)nudW3.Value, (double)nudW2.Value), new Ycc((double)nudW4.Value, (double)nudW6.Value, (double)nudW5.Value));
            else
                imageGray = image.InRange(new Bgr((double)nudW3.Value, (double)nudW2.Value, (double)nudW1.Value), new Bgr((double)nudW6.Value, (double)nudW5.Value, (double)nudW4.Value));


            //imageGray = imageGray.Erode((int)nudErode.Value);
            //imageGray = imageGray.Dilate((int)nudDilate.Value);

            if (medianCB.Checked)
                imageGray = imageGray.SmoothMedian((int)nudMedian.Value);

            //imageBox1.Image = imageGray;
            SimpleShapeChecker ssc = new SimpleShapeChecker();

            Image<Gray, Byte> sgm = new Image<Gray, Byte>(imageGray.Size);
            //Image<Gray, Byte> sgm = (Image<Bgr, Byte>) imageGray.;
            Bitmap bmp = imageGray.ToBitmap();
            bc.ProcessImage(bmp);
            try {
                Blob blob = bc.GetObjectsInformation().First();
                //foreach (Blob blob in bc.GetObjectsInformation()) {
                IntPoint minXY, maxXY;
                PointsCloud.GetBoundingRectangle(bc.GetBlobsEdgePoints(blob), out minXY, out maxXY);
                Rectangle rect = new Rectangle(minXY.X, minXY.Y, maxXY.X - minXY.X, maxXY.Y - minXY.Y);
                Bitmap clonimage = (Bitmap)bmp.Clone();
                BitmapData data = bmp.LockBits(new Rectangle(0, 0, sgm.Width, sgm.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                Drawing.Rectangle(data, rect, Color.White);
                bmp.UnlockBits(data);
                Console.WriteLine ("fullness " + blob.Fullness);

                //List<IntPoint> leftPoints, rightPoints, edgePoints;
                //edgePoints = new List<IntPoint>();
                /*
                // get blob's edge points
                bc.GetBlobsLeftAndRightEdges(blob, out leftPoints, out rightPoints);

                edgePoints.AddRange(leftPoints);
                edgePoints.AddRange(rightPoints);

                // blob's convex hull
                List<IntPoint> hull = hullFinder.FindHull(edgePoints);
                hulls.Add(new Polygon(hull));
                 */
                //}
                /*            int[] glue = new int[image.Cols*image.Rows];
                            byte last = 1;
                            for (int i = imageGray.Rows - 2; i > 0; --i)
                                for (int j = imageGray.Cols - 2; j > 0; --j)
                                    if (0 < imageGray.Data[i, j, 0]) {
                                        //sgm.Data[i, j, 0] = last;
                                        byte nr = last;// sgm.Data[i, j, 0]; //glue[nr] = nr;
                                        if (sgm.Data[i, j + 1, 0] > 0 && sgm.Data[i, j + 1, 0] < nr) {
                                            glue[nr] = sgm.Data[i, j + 1, 0];
                                            nr = sgm.Data[i, j + 1, 0];
                                        }
                                        if (sgm.Data[i + 1, j - 1, 0] > 0 && sgm.Data[i + 1, j - 1, 0] < nr) {
                                            glue[nr] = sgm.Data[i+1, j - 1, 0];
                                            nr = sgm.Data[i + 1, j - 1, 0];
                                        }
                                        if (sgm.Data[i + 1, j, 0] > 0 && sgm.Data[i + 1, j, 0] < nr) {
                                            glue[nr] = sgm.Data[i + 1, j, 0]; 
                                            nr = sgm.Data[i + 1, j, 0];
                                        }
                                        if (sgm.Data[i + 1, j + 1, 0] > 0 && sgm.Data[i + 1, j + 1, 0] < nr) {
                                            glue[nr] = sgm.Data[i + 1, j + 1, 0]; 
                                            nr = sgm.Data[i + 1, j + 1, 0];
                                        }
                                        //int tmp = (byte)(nr > 0 ? nr : last++);
                                        if (nr < last)
                                            sgm.Data[i, j, 0] = nr;
                                        else {
                                            sgm.Data[i, j, 0] = last;
                                            ++last;
                                        }
                                        //glue[last] = sgm.Data[i, j, 0];
                                        //glue[] = sgm.Data[i, j, 0];
                        
                                        //0 < sgm.Data[i, j + 1, 0] + sgm.Data[i + 1, j - 1, 0] + sgm.Data[i + 1, j, 0] + sgm.Data[i + 1, j + 1, 0]) {
                                        //int min = imageGray.Data[i, j, 0];
                                    }
                            for (int i = imageGray.Rows - 2; i > 0; --i)
                                for (int j = imageGray.Cols - 2; j > 0; --j) {
                                    sgm.Data[i, j, 0] = (byte)glue[sgm.Data[i, j, 0]];

                                    if (sgm.Data[i, j, 0] > 0) sgm.Data[i, j, 0] = 255;
                                }
                            //Console.Error.WriteLine(last);

                        /*    for (int i = imageGray.Rows - 2; i > 0; --i)
                                for (int j = imageGray.Cols - 2; j > 0; --j)
                                    if (0 < imageGray.Data[i, j, 0]) {

                                    }
                                        int nr = sgm.Data[i, j, 0];
                                        if (sgm.Data[i, j + 1, 0] > 0 && sgm.Data[i, j + 1, 0] < nr) nr = sgm.Data[i, j + 1, 0];
                                        if (sgm.Data[i + 1, j - 1, 0] > 0 && sgm.Data[i + 1, j - 1, 0] < nr) nr = sgm.Data[i + 1, j - 1, 0];
                                        if (sgm.Data[i + 1, j, 0] > 0 && sgm.Data[i + 1, j, 0] < nr) nr = sgm.Data[i + 1, j, 0];
                                        if (sgm.Data[i + 1, j + 1, 0] > 0 && sgm.Data[i + 1, j + 1, 0] < nr) nr = sgm.Data[i + 1, j + 1, 0];
                                        sgm.Data[i, j, 0] = (byte)(nr > 0 ? nr : last++);
                                        //             sgm.Data[i, j, 0] = (byte) (sgm.Data[i, j, 0] > 0 ? 255 : 0);
                                        //0 < sgm.Data[i, j + 1, 0] + sgm.Data[i + 1, j - 1, 0] + sgm.Data[i + 1, j, 0] + sgm.Data[i + 1, j + 1, 0]) {
                                        //int min = imageGray.Data[i, j, 0];
                                    }*/
                // imageGray.Data[i, j, 0] = 0;

                //imageBox2.Image = test(image);
            } catch (InvalidOperationException) {
                //no blob detected
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
    }
}
