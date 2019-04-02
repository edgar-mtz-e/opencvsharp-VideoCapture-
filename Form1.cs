using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace lastflow
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture;
        private Mat _frame;
        private Bitmap _image;
        private Thread _camera;
        private bool _isCameraRunning;

        public Form1()
        {
            InitializeComponent();
        }

        private void CaptureCamera()
        {
            _camera = new Thread(CaptureCameraCallback);
            _camera.Start();
        }

        private void CaptureCameraCallback()
        {
            _frame = new Mat();
            _capture = new VideoCapture();
            _capture.Open(0);

            if (!_capture.IsOpened()) return;
            while (_isCameraRunning)
            {
                _capture.Read(_frame);
                _image = _frame.ToBitmap();
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = _image;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Start"))
            {
                CaptureCamera();
                button1.Text = @"Stop";
                _isCameraRunning = true;
            }
            else
            {
                _capture.Release();
                button1.Text = @"Start";
                _isCameraRunning = false;
            }
        }
    }
}
