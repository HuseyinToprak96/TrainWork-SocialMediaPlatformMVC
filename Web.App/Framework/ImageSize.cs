using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using Web.App.Controllers;

namespace Web.App.Framework
{
    public class ImageSize:BaseController
    {
        public void Execute(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Yüklenen resmi belleğe yükleyin
                var image = Image.FromStream(file.InputStream);

                // Yeni boyutu belirleyin
                int newWidth = 300;
                int newHeight = 300;

                // Yeni boyutta bir resim oluşturun
                using (var resizedImage = new Bitmap(newWidth, newHeight))
                {
                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        // Resmi yeniden boyutlandırın
                        graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                    }

                    // Yeni resmi kaydedin veya işleyin
                    var path = "../../Content/images/" + Guid.NewGuid() + "yeni.png";
                    resizedImage.Save(path, ImageFormat.Png);
                    // İşlem tamamlandıktan sonra orijinal resmi ve yeniden boyutlandırılmış resmi bellekten serbest bırakın
                    image.Dispose();
                    resizedImage.Dispose();
                }

               
            }
        }
    }
}