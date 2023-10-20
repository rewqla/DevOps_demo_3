using Microsoft.AspNetCore.Hosting;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace OilShop.Helpers
{
    public static class ImageHelper
    {
        public static Bitmap FromBase64StringToImage(this string base64String)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(byteBuffer))
                {
                    memoryStream.Position = 0;
                    using (Image imgReturn = Image.FromStream(memoryStream))
                    {
                        memoryStream.Close();
                        byteBuffer = null;
                        return new Bitmap(imgReturn);
                    }
                }
            }
            catch { return null; }
        }

        public static string urlCreator(this string PhotoBase64, IWebHostEnvironment _env, string imageName)
        {
            if (imageName != "")
            {
                var imgPath = _env.ContentRootPath + "\\wwwroot\\" + imageName;
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }

            string base64 = PhotoBase64;
            if (base64.Contains(","))
            {
                base64 = base64.Split(',')[1];
            }

            var bmp = base64.FromBase64StringToImage();
            var serverPath = _env.ContentRootPath + "\\wwwroot";
            var folerName = "Uploads";
            var path = Path.Combine(serverPath, folerName); //
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string ext = ".jpg";
            string fileName = Path.GetRandomFileName() + ext;
            string filePathSave = Path.Combine(path, fileName);
            bmp.Save(filePathSave, ImageFormat.Jpeg);

            return folerName + "/" + fileName;
        }
    }
}
