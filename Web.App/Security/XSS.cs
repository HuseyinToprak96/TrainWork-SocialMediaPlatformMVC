using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App.Security
{
    public class XSS
    {
            public static string CleanInput(string input)
            {
                // HTML etiketlerini temizle
               // string cleanInput = System.Web.HttpUtility.HtmlEncode(input);

                // Zararlı kodları etkisiz hale getir
                // Örneğin, <script> etiketlerini kaldır veya etkisiz hale getir
                input = input.Replace("<script>", "&lt;script&gt;")
                                       .Replace("</script>", "&lt;/script&gt;");

                return input;
            }
        public string SanitizeHtml(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            // HtmlAgilityPack kullanarak HTML içeriğini güvenli bir şekilde işle
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(input);
            // Güvenli HTML içeriği elde etmek için gerekli işlemleri yap
            // Örneğin, yasak etiketleri veya özellikleri kaldırabilirsin veya belirli güvenli etiketlere izin verebilirsin
            string sanitizedInput = htmlDocument.DocumentNode.OuterHtml;
            return sanitizedInput;
        }
    }
    }