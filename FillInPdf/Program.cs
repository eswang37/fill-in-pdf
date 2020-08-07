using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Image;
using iText.Kernel.Geom;

namespace FillInPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            PdfWriter writer = new PdfWriter("C:\\demo.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("HEADER")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            document.Add(header);
            document.Close();
            */

            string pdfIn = "C:\\pdf.template\\addendum.pdf";
            string pdfOut = "C:\\pdf.template\\signed_addendum.pdf";
            
            PdfDocument signedPdf = new PdfDocument(new PdfReader(pdfIn), new PdfWriter(pdfOut));
            Document document = new Document(signedPdf);

            PdfAcroForm form = PdfAcroForm.GetAcroForm(signedPdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;
            fields.TryGetValue("single_address", out toSet);
            toSet.SetValue("200 main st, Irvine, CA 92694");
            form.FlattenFields();

            string signature1 = "C:\\pdf.template\\signature1.png";
            ImageData imageData1 = ImageDataFactory.Create(signature1);

            string signature2 = "C:\\pdf.template\\signature2.png";
            ImageData imageData2 = ImageDataFactory.Create(signature2);


            Rectangle pageSize = document.GetPdfDocument().GetPage(3).GetPageSize();

            

            Image image = new Image(imageData1).ScaleAbsolute(180, 30).SetFixedPosition(3, pageSize.GetWidth() / 4 + 20, 175);
            Image image2 = new Image(imageData2).ScaleAbsolute(180, 30).SetFixedPosition(3, pageSize.GetWidth() / 4 + 20, 135);
            // This adds the image to the page
            document.Add(image);
            document.Add(image2);




            signedPdf.Close();


        }
    }
}
