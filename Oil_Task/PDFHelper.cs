using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil_Task
{
    public static class PdfHelper
    {
        public static PdfPCell CreateNewCell(string cellName, int colSpan)
        {
            return new PdfPCell(new Phrase(cellName))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                MinimumHeight = 22f,
                Colspan = colSpan
            };
        }

    }
}
