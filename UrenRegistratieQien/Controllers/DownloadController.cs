using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrenRegistratieQien.GlobalClasses;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQien.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IDeclarationFormRepository declarationFormRepo;

        public DownloadController(IDeclarationFormRepository declarationFormRepo)
        {
            this.declarationFormRepo = declarationFormRepo;
        }


        public async Task<FileContentResult> DownloadExcel(int formId)
        {
            Download download = new Download();
            DeclarationFormModel declarationForm = await declarationFormRepo.GetForm(formId);

            download.MakeExcel(Convert.ToString(formId), declarationForm.HourRows);
            var fileName = Convert.ToString(formId) + ".xlsx";

            byte[] fileBytes = System.IO.File.ReadAllBytes("Downloads/" + fileName);
            return File(fileBytes, "Application/x-msexcel", fileName);
        }


        public FileContentResult DownloadAttachments(int formId)
        {
            var fileName = $"{formId}.zip";
            byte[] fileBytes = System.IO.File.ReadAllBytes("wwwroot/Uploads/" + fileName);
            return File(fileBytes, "application/zip", fileName);
        }

        public FileContentResult DownloadTotalHoursCSV(int totalWorked, int totalOvertime, int totalSickness, int totalVacation, int totalHoliday, int totalTraining, int totalOther) //eventueel filters meenemen..
        {
            List<string> downloadableList = new List<string>
            {
                Convert.ToString(totalWorked),
                Convert.ToString(totalOvertime),
                Convert.ToString(totalSickness),
                Convert.ToString(totalVacation),
                Convert.ToString(totalHoliday),
                Convert.ToString(totalTraining),
                Convert.ToString(totalOther)
            };
            Download download = new Download();
            string fileName = "Totalhours.txt";
            string header = "gewerkt, overuren, ziekte, vakantie, feestdagen, training, anders";
            download.MakeCSV(header, downloadableList, fileName);


            byte[] fileBytes = System.IO.File.ReadAllBytes("Downloads/" + fileName);
            return File(fileBytes, "text/plain", fileName);
        }
    }
}