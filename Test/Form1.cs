using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace Test
{
    public partial class Form1 : Form
    {
        private string code = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Captcha c = new Captcha();
            var image = c.GenCaptcha(code);
            Bitmap bmp;
            using (MemoryStream stream = new MemoryStream(image))
            {
                bmp = new Bitmap(stream);
            }
            g.DrawImage(bmp, new PointF(0, 0));


           
            
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    int grayValue = GetGrayValue(bmp.GetPixel(j, i));
                    bmp.SetPixel(j, i, Color.FromArgb(grayValue, grayValue, grayValue));
                }
            }
            var gra = pictureBox2.CreateGraphics();
            gra.DrawImage(bmp, new PointF(0, 0));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Captcha c = new Captcha();
            code = c.GenCode(5);

            pictureBox1.Refresh();
            //pictureBox2.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Captcha c = new Captcha();
            code = c.GenCode(5);

            
        }

        /// <summary>
        /// 計算灰階值
        /// </summary>
        /// <param name="pColor">color-像素色彩</param>
        /// <returns></returns>
        private int GetGrayValue(Color pColor)
        {
            return Convert.ToInt32(pColor.R * 0.299 + pColor.G * 0.587 + pColor.B * 0.114); // 灰階公式
        }
    }
}
