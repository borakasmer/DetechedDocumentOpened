using GemBox.Spreadsheet;
using System;
using System.Linq;

namespace SaveImageToExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {  
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            bool hasFile = System.IO.File.Exists(@"C:\Projects\Images.xlsx");
            ExcelFile workbook = null;
            if (hasFile)
            {
                workbook = ExcelFile.Load(@"C:\Projects\Images.xlsx");
            }
            else
            {
                workbook = new ExcelFile();
            }
            ExcelWorksheet worksheet;
            if (workbook.Worksheets.Count == 0)
            {
                worksheet = workbook.Worksheets.Add("Images");
            }
            else
            {
                worksheet = workbook.Worksheets.First(ws => ws.Name == "Images");
            }

            worksheet.Cells[0, 0].Value = "Gerekli Dökümanlar:";
            worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, 1).Merged = true;
            worksheet.Cells[0, 2].Value = "Diploma, Banka Hesap Defteri, Kimlik";
            worksheet.Cells.GetSubrangeAbsolute(0, 2, 0, 6).Merged = true;

            worksheet.Cells[1, 0].Value = "Son başvuru tarihi:";
            worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, 1).Merged = true;

            worksheet.Cells[1, 2].Value = DateTime.Now.AddDays(7).ToShortDateString();
            worksheet.Cells.GetSubrangeAbsolute(1, 2, 1, 5).Merged = true;

            worksheet.Cells[2, 0].Value = "Çin Mesaj:";
            worksheet.Cells[2, 1].Value = new string(new char[] { '\u4f60', '\u597d' });

            worksheet.Cells[4, 0].Value = "Gerekli işlemlerin zamanında yapılabilmesi için tüm istenen evrakların eksiksiz getirilmesi gerekmektedir.";
            worksheet.Cells.GetSubrangeAbsolute(4, 0, 4, 10).Merged = true;        
       
            var picture = worksheet.Pictures.Add("http://192.168.1.7:5094/Document?clientID=1&documentID=2&actionTypeID=1&type=.jpg", "B9",
                720, 340, LengthUnit.Pixel, ExcelObjectSourceType.Link);
        
            picture.Hyperlink.Location = "http://192.168.1.7:5094/Document?clientID=1&documentID=2&actionTypeID=2";
            picture.Hyperlink.IsExternal = true;   
            
            workbook.Save(@"C:\Projects\Images.xlsx");           
        }
    }
}
