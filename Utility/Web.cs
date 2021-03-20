using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Utility
{
    public class Web
    {
        public Image GetCaptcha(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                webClient.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                using (Stream stream = webClient.OpenRead(url))
                {
                    return Image.FromStream(stream);
                }
            }
        }
    }
}
