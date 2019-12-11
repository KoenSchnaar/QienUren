
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrenRegistratieQien.Models;



namespace UrenRegistratieQien.GlobalClasses
{
    public class Download
    {
        public HttpResponse Response { get; set; }

        public void MakeCSV(string header, List<string> downloadList, string fileName)
        {
            string downloadString = "";
            for(var i=0; i<downloadList.Count(); i++)
            {
                if(i != downloadList.Count())
                {
                    downloadString += downloadList[i] + ",";
                } else {
                    downloadString += downloadList[i];
                }
            }
            string filePath = "Downloads/" + fileName;
            using (StreamWriter file = new StreamWriter(@filePath, false))
            {
                file.WriteLine(header);
                file.WriteLine(downloadString);
            }
        }

        public void MakeExcel(string fileName, List<HourRowModel> hourRows)
        {
            using (var fs = new FileStream($"Downloads/{fileName}.xlsx", FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1 = workbook.CreateSheet("Sheet1");
                var rowIndex = 0;
                IRow headerRow = sheet1.CreateRow(rowIndex);
                headerRow.CreateCell(0).SetCellValue("Datum");
                headerRow.CreateCell(1).SetCellValue("Gewerkt");
                headerRow.CreateCell(2).SetCellValue("Overwerk");
                headerRow.CreateCell(3).SetCellValue("Ziek");
                headerRow.CreateCell(4).SetCellValue("Vakantie");
                headerRow.CreateCell(5).SetCellValue("Feestdagen");
                headerRow.CreateCell(6).SetCellValue("Training");
                headerRow.CreateCell(7).SetCellValue("Anders");
                headerRow.CreateCell(8).SetCellValue("Uitleg voor anders");
                rowIndex += 1;
                foreach (HourRowModel hourRow in hourRows)
                {
                    IRow row = sheet1.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(hourRow.Date);
                    row.CreateCell(1).SetCellValue(Convert.ToString(hourRow.Worked));
                    row.CreateCell(2).SetCellValue(Convert.ToString(hourRow.Overtime));
                    row.CreateCell(3).SetCellValue(Convert.ToString(hourRow.Sickness));
                    row.CreateCell(4).SetCellValue(Convert.ToString(hourRow.Vacation));
                    row.CreateCell(5).SetCellValue(Convert.ToString(hourRow.Holiday));
                    row.CreateCell(6).SetCellValue(Convert.ToString(hourRow.Training));
                    row.CreateCell(7).SetCellValue(Convert.ToString(hourRow.Other));
                    row.CreateCell(8).SetCellValue(Convert.ToString(hourRow.OtherExplanation));
                    rowIndex += 1;
                }
                workbook.Write(fs);
            }
        }
    }
}
