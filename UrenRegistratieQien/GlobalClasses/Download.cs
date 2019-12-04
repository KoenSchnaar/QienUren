
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//



namespace UrenRegistratieQien.GlobalClasses
{
    public class Download
    {
        public HttpResponse Response { get; set; }

        public void MakeCSV(List<string> downloadList, string fileName)
        {
            string downloadString = "";
            for(var i=0; i<downloadList.Count(); i++)
            {
                if(i != downloadList.Count())
                {
                    downloadString += downloadList[i] + ",";
                } else
                {
                    downloadString += downloadList[i];
                }
                
            }
            string filePath = "Downloads/" + fileName;

            using (StreamWriter file = new StreamWriter(@filePath, false))
            {
                file.WriteLine(downloadString);
            }

        }
    }
}
