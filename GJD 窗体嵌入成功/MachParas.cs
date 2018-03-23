using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GJD
{
    public interface IMachParahandle
    {
        void richboxshow(object s);
        void richboxclear();

    }
    public partial class MachParas : Form, IMachParahandle
    {
        public static string textmedium;
        public void richboxshow(object message)
        {
            richmessagebox.AppendText((string)message + "\r\n");
        }
        public void richboxclear()
        {
            richmessagebox.Clear();
        }

        public MachParas()
        {
            InitializeComponent();
            richmessagebox.Text = "陶瓷型芯加工" + "\r\n";
        }
        
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(160, 140);
            g.ScaleTransform(40, -40);
            DrawRuler(g);
        }
        public void DrawRuler(Graphics g)//在显示位置的画板上绘制坐标图
        {
            Pen penBlack = new Pen(Color.Black, 0.05f);
            Font font = new Font("Arial", 8f / 40, FontStyle.Regular);
            g.DrawRectangle(penBlack, -3, -3, 6, 6);
            Pen penblue = new Pen(Color.DarkGray, 0.03f);
            for (int i = 0; i < 11; i++)
            {
                g.DrawLine(penblue, -3f, -2.5f + 0.5f * i, 3f, -2.5f + 0.5f * i);
            }
            for (int i = 0; i < 11; i++)
            {
                g.DrawLine(penblue, -2.5f + 0.5f * i, -3f, -2.5f + 0.5f * i, 3f);
            }
            for (float x = -3; x < 3; x += 0.5f)
            {
                g.DrawLine(penBlack, x, -3, x, -3 + 6f / 240 * 5);

            }
            g.ScaleTransform(1, -1);
            g.DrawString((-3).ToString(), font, Brushes.Black, -3.15f, 3.05f);
            g.DrawString((-2.5).ToString(), font, Brushes.Black, -2.85f, 3.05f);
            g.DrawString((-2).ToString(), font, Brushes.Black, -2.18f, 3.05f);
            g.DrawString((-1.5).ToString(), font, Brushes.Black, -1.8f, 3.05f);
            g.DrawString((-1).ToString(), font, Brushes.Black, -1.18f, 3.05f);
            g.DrawString((-0.5).ToString(), font, Brushes.Black, -0.8f, 3.05f);
            g.DrawString((0).ToString(), font, Brushes.Black, -0.13f, 3.05f);
            g.DrawString((3).ToString(), font, Brushes.Black, 2.86f, 3.05f);
            g.DrawString((2.5).ToString(), font, Brushes.Black, 2.25f, 3.05f);
            g.DrawString((2).ToString(), font, Brushes.Black, 1.86f, 3.05f);
            g.DrawString((1.5).ToString(), font, Brushes.Black, 1.25f, 3.05f);
            g.DrawString((1).ToString(), font, Brushes.Black, 0.86f, 3.05f);
            g.DrawString((0.5).ToString(), font, Brushes.Black, 0.25f, 3.05f);
            g.ScaleTransform(1, -1);
            for (float y = -3; y < 3; y += 0.5f)
            {
                g.DrawLine(penBlack, -3f, y, -3 + 6f / 240 * 5, y);
            }
            g.ScaleTransform(1, -1);
            g.DrawString((-3).ToString(), font, Brushes.Black, -3.45f, 2.85f);
            g.DrawString((-2).ToString(), font, Brushes.Black, -3.45f, 1.85f);
            g.DrawString((-1).ToString(), font, Brushes.Black, -3.45f, 0.85f);
            g.DrawString((0).ToString(), font, Brushes.Black, -3.45f, -0.15f);
            g.DrawString((1).ToString(), font, Brushes.Black, -3.35f, -1.15f);
            g.DrawString((2).ToString(), font, Brushes.Black, -3.35f, -2.15f);
            g.DrawString((3).ToString(), font, Brushes.Black, -3.35f, -3.15f);
            g.DrawString((-2.5).ToString(), font, Brushes.Black, -3.65f, 2.35f);
            g.DrawString((-1.5).ToString(), font, Brushes.Black, -3.65f, 1.35f);
            g.DrawString((-0.5).ToString(), font, Brushes.Black, -3.65f, 0.35f);
            g.DrawString((0.5).ToString(), font, Brushes.Black, -3.55f, -0.65f);
            g.DrawString((1.5).ToString(), font, Brushes.Black, -3.55f, -1.65f);
            g.DrawString((2.5).ToString(), font, Brushes.Black, -3.55f, -2.65f);
            g.ScaleTransform(1, -1);
        }

        private void timerparaUP_Tick(object sender, EventArgs e)
        {
            richmessagebox.Text = textmedium;
        }
    }
}
