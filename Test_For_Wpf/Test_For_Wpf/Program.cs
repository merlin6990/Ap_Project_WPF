
using iTextSharp.text.pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace my
{
    public class program
    {
        static void Main()
        {
            iTextSharp.text.Document oDoc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(oDoc, new FileStream("HelloWorld.pdf", FileMode.Create));
            oDoc.Open();
            oDoc.Add(new iTextSharp.text.Paragraph("Hello World!"));
            oDoc.Close();
        }
    }
}




