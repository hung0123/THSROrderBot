using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
            IWebDriver driver = new ChromeDriver();//必須把對應的chromedriver加進專案，並複製到輸出目錄;
            
            try
            {
                //Captcha c = new Captcha();
                //code = c.GenCode(5);

                //先跑一次首頁的原因
                //https://stackoverflow.com/questions/41559510/selenium-chromedriver-add-cookie-invalid-domain-error/44857193
                driver.Navigate().GoToUrl("https://irs.thsrc.com.tw/IMINT/?locale=tw");

                driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("AcceptIRSCookiePolicyTime", System.DateTime.Now.ToString(), "irs.thsrc.com.tw", "/", null));

                for (int i = 0; i < 10; i++)
                {
                    driver.Navigate().Refresh();
                    
                    var captchaElement = driver.FindElement(By.Id("BookingS1Form_homeCaptcha_passCode"));



                    var captchaUrl = captchaElement.GetAttribute("src");

                    //driver.SwitchTo().NewWindow(WindowType.Tab);
                    //driver.Navigate().GoToUrl(captchaUrl);

                    Web web = new Web();
                    Image captcha = web.GetCaptcha(captchaUrl);

                    captcha.Save(string.Format(@"C:\Program Files\Tesseract-OCR\jTessBoxEditor-2.3.1\jTessBoxEditor\Thsr\{0}.tiff", i));

                    Thread.Sleep(1000);
                }
                //pictureBox3.Image = captcha;
                //driver.Close();
            }
            catch(Exception ex)
            {
                driver.Dispose();
            }
            finally
            {
               //driver.Dispose();
            }
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

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
