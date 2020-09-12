using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Configuration;


namespace Utility
{
    public class Captcha
    {
        public byte[] GenCaptcha(string code)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(300, 40))
                {
                    Graphics g = Graphics.FromImage(bmp);

                    g.Clear(Color.Gray);

                    int x = 0;
                    foreach(char a in code)
                    {
                        int margin = random.Next(25, 35);
                        int y = random.Next(0, 15);
                        x += margin;
                        g.DrawString(a.ToString(), new Font("Arial", 18.0F), new SolidBrush(Color.AliceBlue), new PointF(x, y));
                    }
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                return ms.ToArray();
            }
        }

        public string GenCode(int num)
        {
            string result = "";
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string a = ConfigurationManager.AppSettings.Get("CaptchaTable");
            for(int i=0;i<num;i++)
            {
                int x = random.Next(0, a.Length);
                result += a[x];
            }
            return result;
            
        }
    }
}
