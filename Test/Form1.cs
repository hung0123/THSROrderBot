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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Captcha c = new Captcha();
            code = c.GenCode(5);

            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Captcha c = new Captcha();
            code = c.GenCode(5);
        }
    }
}
