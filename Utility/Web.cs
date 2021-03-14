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
        public byte[] GetData(string uri)
        {
            byte[] result = null;
            using (WebClient webClient = new WebClient())
            {
                var a = webClient.DownloadString(uri);
                Stream stream = webClient.OpenRead(uri);
                Bitmap bitmap;
                bitmap = new Bitmap(stream);

                if (bitmap != null)
                {
                    //bitmap.Save(filename, format);
                }

                stream.Flush();
                stream.Close();
            }

            return result;
        }
    }
}
