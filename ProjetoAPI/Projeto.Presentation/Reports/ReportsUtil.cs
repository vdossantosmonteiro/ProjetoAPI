using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Projeto.Presentation.Reports
{
    public class ReportsUtil
    {
        public static byte[] ConvertToPdf(string htmlContent)
        {
            var ms = new MemoryStream();
            var reader = new StringReader(htmlContent);
            var doc = new Document(PageSize.A4, 50, 50, 50, 50);
            var writer = PdfWriter.GetInstance(doc, ms);
            var html = new HTMLWorker(doc);
            doc.Open();
            html.StartDocument();
            html.Parse(reader);
            html.EndDocument();
            html.Close();
            doc.Close();
            return ms.ToArray();
        }
    }
}
    
