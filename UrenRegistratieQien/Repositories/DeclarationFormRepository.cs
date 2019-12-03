using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.GlobalClasses;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public class DeclarationFormRepository : IDeclarationFormRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHourRowRepository hourRowRepo;

        public DeclarationFormRepository(ApplicationDbContext context, IHourRowRepository hourRowRepo)
        {
            this.context = context;
            this.hourRowRepo = hourRowRepo;
        }


        public string GenerateUniqueId()
        {

            string URL = "";
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            List<char> characters = new List<char>()
                {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'};

            Random rand = new Random();
            for (int i = 0; i < 21; i++)
            {
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    random = rand.Next(0, numbers.Count);
                    URL += numbers[random].ToString();
                }
                else
                {
                    random = rand.Next(0, characters.Count);
                    URL += characters[random].ToString();
                }
            }
            return URL;
        }


        public void CreateFormForUser(string EmployeeId, string month, int year)
        {
            var newForm = new DeclarationForm
            {
                EmployeeId = EmployeeId,
                Month = month,
                Year = year,
                uniqueId = GenerateUniqueId(),
                Approved = "Pending",
                Submitted = false,
                TotalWorkedHours = 0,
                TotalOvertime = 0,
                TotalSickness = 0,
                TotalVacation = 0,
                DateCreated = DateTime.Now
            };

            context.DeclarationForms.Add(newForm);
            context.SaveChanges();
        }


        public void CreateForm(string employeeId)
        {
            var entities = context.DeclarationForms.Where(p => p.EmployeeId == employeeId).ToList();
            if (entities.Count() > 0)
            {
                var entitiesIndex = entities.Count() - 1;
                var entity = entities[entitiesIndex];

                var month = entity.Month;
                var monthInt = MonthConverter.ConvertMonthToInt(month) + 1;
                if (monthInt == 13)
                {
                    monthInt = 1;
                }
                var monthString = MonthConverter.ConvertIntToMonth(monthInt);
                var year = entity.Year;
                if (monthString == "Januari")
                {
                    year = year + 1;
                }

                if (monthInt == 1) 
                {
                    monthInt = 13;
                }
                if (monthInt == 2 && DateTime.Now.Month != 1)
                {
                    monthInt = 14;
                }
                if (monthInt <= DateTime.Now.Month +1)
                {
                    var form = new DeclarationForm
                    {
                        EmployeeId = employeeId,
                        Month = monthString,
                        Year = year,
                        uniqueId = GenerateUniqueId(),
                        Approved = "Pending",
                        Submitted = false,
                        TotalWorkedHours = 0,
                        TotalOvertime = 0,
                        TotalSickness = 0,
                        TotalVacation = 0,
                        DateCreated = DateTime.Now
                    };
                context.DeclarationForms.Add(form);
                }
                else
                {
                    return;
                }
            }
            else
            {
                var monthInt = DateTime.Now.Month;
                var monthString = MonthConverter.ConvertIntToMonth(monthInt);
                var year = DateTime.Now.Year;
                var form = new DeclarationForm
                {
                    EmployeeId = employeeId,
                    Month = monthString,
                    Year = year,
                    uniqueId = GenerateUniqueId(),
                    Approved = "Pending",
                    Submitted = false,
                    TotalWorkedHours = 0,
                    TotalOvertime = 0,
                    TotalSickness = 0,
                    TotalVacation = 0,
                    DateCreated = DateTime.Now
                };

                context.DeclarationForms.Add(form);
            }

            context.SaveChanges();
        }

        public void ApproveForm(int formId)
        {
            var form = context.DeclarationForms.Single(p => p.DeclarationFormId == formId);
            form.Approved = "Approved";
            context.SaveChanges();
        }

        public void RejectForm(int formId, string comment)
        {
            var form = context.DeclarationForms.Single(p => p.DeclarationFormId == formId);
            form.Approved = "Rejected";
            form.Comment = comment;
            context.SaveChanges();
        }

        public DeclarationFormModel GetFormModelFromEntity(DeclarationForm entity)
        {
            List<HourRowModel> ListOfHourRowModels = new List<HourRowModel>();
            foreach (HourRow hourRow in entity.HourRows)
            {
                HourRowModel newHourRowModel = new HourRowModel
                {
                    HourRowId = hourRow.HourRowId,
                    EmployeeId = entity.EmployeeId,
                    Date = hourRow.Date,
                    Worked = hourRow.Worked,
                    Overtime = hourRow.Overtime,
                    Sickness = hourRow.Sickness,
                    Vacation = hourRow.Vacation,
                    Holiday = hourRow.Holiday,
                    Training = hourRow.Training,
                    Other = hourRow.Other,
                    OtherExplanation = hourRow.OtherExplanation
                    
                };

                ListOfHourRowModels.Add(newHourRowModel);
            }

            var selectedEmployee = context.Users.Single(p => p.Id == entity.EmployeeId);
            var castedEmployee = (Employee)selectedEmployee;
            var employeeName = castedEmployee.FirstName + " " + castedEmployee.LastName;

            var newModel = new DeclarationFormModel
            {
                FormId = entity.DeclarationFormId,
                HourRows = ListOfHourRowModels,
                EmployeeId = entity.EmployeeId,
                EmployeeName = employeeName,
                Month = entity.Month,
                Approved = entity.Approved,
                Submitted = entity.Submitted,
                Comment = entity.Comment,
                uniqueId = entity.uniqueId,
                TotalWorkedHours = entity.TotalWorkedHours,
                TotalOvertime = entity.TotalOvertime,
                TotalSickness = entity.TotalSickness,
                TotalVacation = entity.TotalVacation,
                DateCreated = entity.DateCreated
            };
            return newModel;

        }

        public List<DeclarationFormModel> GetFormModelsFromEntities(List<DeclarationForm> entities)
        {
            var forms = new List<DeclarationFormModel>();
            foreach (var form in entities)
            {
                List<HourRowModel> ListOfHourRowModels = new List<HourRowModel>();

                foreach (HourRow hourRow in form.HourRows)
                {
                    HourRowModel newHourRowModel = new HourRowModel
                    {
                        HourRowId = hourRow.HourRowId,
                        EmployeeId = form.EmployeeId,
                        Date = hourRow.Date,
                        Worked = hourRow.Worked,
                        Overtime = hourRow.Overtime,
                        Sickness = hourRow.Sickness,
                        Vacation = hourRow.Vacation,
                        Holiday = hourRow.Holiday,
                        Training = hourRow.Training,
                        Other = hourRow.Other,
                        OtherExplanation = hourRow.OtherExplanation
                    };

                    ListOfHourRowModels.Add(newHourRowModel);
                }

                var selectedEmployee = context.Users.Single(p => p.Id == form.EmployeeId);
                var castedEmployee = (Employee)selectedEmployee;
                var employeeName = castedEmployee.FirstName + " " + castedEmployee.LastName;

                var newModel = new DeclarationFormModel
                {
                    FormId = form.DeclarationFormId,
                    HourRows = ListOfHourRowModels,
                    EmployeeId = form.EmployeeId,
                    EmployeeName = employeeName,
                    Month = form.Month,
                    Approved = form.Approved,
                    Submitted = form.Submitted,
                    Comment = form.Comment,
                    Year = form.Year,
                    uniqueId = form.uniqueId,
                    TotalWorkedHours = form.TotalWorkedHours,
                    TotalOvertime = form.TotalOvertime,
                    TotalSickness = form.TotalSickness,
                    TotalVacation = form.TotalVacation,
                    DateCreated = form.DateCreated
                };

                forms.Add(newModel);
            }
            return forms;
        }

        public DeclarationFormModel GetForm(int formId)
        {
            var entity = context.DeclarationForms.Include(df => df.HourRows).Single(d => d.DeclarationFormId == formId);
            return GetFormModelFromEntity(entity);
        }



        public List<DeclarationFormModel> GetNotApprovedForms()
        {
            var allForms = GetAllForms();
            var notApprovedForms = new List<DeclarationFormModel>();
            foreach(DeclarationFormModel form in allForms)
            {
                if(form.Approved != "Rejected")
                {
                    notApprovedForms.Add(form);
                }
            }
            return notApprovedForms;
        }

        public List<DeclarationFormModel> GetFilteredForms(string year, string employeeId, string month, string approved, string submitted, string sortDate)
        {
            if(approved == "Goedgekeurd")
            {
                approved = "Approved";
            }
            if(approved == "Niet goedgekeurd")
            {
                approved = "Rejected";
            }
            if (submitted == "Ingediend")
            {
                submitted = "true";
            }
            if (submitted == "Niet ingediend")
            {
                submitted = "false";
            }

            var entities = new List<DeclarationForm>();
            if(sortDate == "Ascending")
            {
                entities = context.DeclarationForms.Include(df => df.HourRows)
                .OrderBy(df => df.DeclarationFormId).ToList();
            } else
            {
                entities = context.DeclarationForms.Include(df => df.HourRows)
                .OrderByDescending(df => df.DeclarationFormId).ToList();
            }
           

            List<DeclarationForm> holderList = new List<DeclarationForm>();

            if (year != null)
            {
                var yearAsInt = Convert.ToInt32(year);
                foreach (DeclarationForm entity in entities)
                {
                    if (entity.Year != yearAsInt)
                    {
                        holderList.Add(entity);
                    }
                }
            }

            if (employeeId != null){

                foreach (DeclarationForm entity in entities)
                {
                    if (entity.EmployeeId != employeeId)
                    {
                        holderList.Add(entity);
                    }
                }
            }
            foreach(DeclarationForm declarationForm in holderList)
            {
                if (entities.Contains(declarationForm)){
                    entities.Remove(declarationForm);
                }
            }
            if (month != null) {

                foreach (DeclarationForm entity in entities)
                {
                    if (entity.Month != month)
                    {
                        holderList.Add(entity);
                    }
                }

            }
            foreach (DeclarationForm declarationForm in holderList)
            {
                if (entities.Contains(declarationForm))
                {
                    entities.Remove(declarationForm);
                }
            }
            if (approved != null){

                foreach (DeclarationForm entity in entities)
                    {
                    
                        if (entity.Approved == "Rejected")
                        {
                            holderList.Add(entity);
                        }
                    }

                }
            foreach (DeclarationForm declarationForm in holderList)
            {
                if (entities.Contains(declarationForm))
                {
                    entities.Remove(declarationForm);
                }
            }
            if (submitted != null){

                bool boolSubmitted = Convert.ToBoolean(submitted);
                    foreach (DeclarationForm entity in entities)
                    {
                        if (entity.Submitted != boolSubmitted)
                        {
                            holderList.Add(entity);
                        }
                    }

                }
            foreach (DeclarationForm declarationForm in holderList)
            {
                if (entities.Contains(declarationForm))
                {
                    entities.Remove(declarationForm);
                }
            }

            var forms = GetFormModelsFromEntities(entities);
            return forms;
        }

        public List<DeclarationFormModel> GetAllForms()
        {
            var entities = context.DeclarationForms.Include(df => df.HourRows).OrderByDescending(df => df.DeclarationFormId).ToList();
            var forms = GetFormModelsFromEntities(entities);
            return forms;
        }


        public List<DeclarationFormModel> GetAllFormsOfUser(string userId)
        {
            var entities = context.DeclarationForms.Include(df => df.HourRows).Where(d => d.EmployeeId == userId).ToList();
            var forms = GetFormModelsFromEntities(entities);
            return forms;
        }

        public List<DeclarationFormModel> GetAllFormsOfMonth(int month)
        {
            var monthString = MonthConverter.ConvertIntToMonth(month);
            var entities = context.DeclarationForms.Include(df => df.HourRows).Where(d => d.Month == monthString).ToList();
            var forms = GetFormModelsFromEntities(entities);
            return forms;
        }

        public void EditDeclarationForm(DeclarationFormModel formModel)
        {
            var form = context.DeclarationForms.Single(d => d.DeclarationFormId == formModel.FormId);
            var hourList = new List<HourRow>();

            foreach (var row in formModel.HourRows)
            {
                var entity = context.HourRows.Single(h => h.HourRowId == row.HourRowId);
                entity.Worked = row.Worked;
                entity.Overtime = row.Overtime;
                entity.Sickness = row.Sickness;
                entity.Vacation = row.Vacation;
                entity.Holiday = row.Holiday;
                entity.Training = row.Training;
                entity.Other = row.Other;
                entity.OtherExplanation = row.OtherExplanation;
            }
            context.SaveChanges();
        }

        public void SubmitDeclarationForm(DeclarationFormModel formModel)
        {
            var form = context.DeclarationForms.Single(d => d.DeclarationFormId == formModel.FormId);
            form.Submitted = true;
            form.Approved = "Pending";
            context.SaveChanges();
        }
        public int TotalHoursWorked(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {

            int counter = 0;
            if(Month == null)
            {
                foreach(var Form in DeclarationFormList)
                {
                    if(Form.Year == Year)
                    {
                        foreach (var HourRow in Form.HourRows)
                        {
                            counter += HourRow.Worked;
                        }
                    }

                }
            } else
            {
                foreach (var Form in DeclarationFormList)
                {
                    if(Form.Year == Year)
                    {
                        if (Form.Month == Month)
                        {
                            foreach (var HourRow in Form.HourRows)
                            {
                                counter += HourRow.Worked;

                            }
                        }
                    }

                }
            }

            return counter;

        }

        public int TotalHoursOvertime(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {

            int counter = 0;
            if (Month == null)
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        foreach (var HourRow in Form.HourRows)
                        {
                            counter += HourRow.Overtime;
                        }
                    }

                }
            }
            else
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        if (Form.Month == Month)
                        {
                            foreach (var HourRow in Form.HourRows)
                            {
                                counter += HourRow.Overtime;

                            }
                        }
                    }

                }
            }

            return counter;

        }

        public int TotalHoursSickness(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {

            int counter = 0;
            if (Month == null)
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        foreach (var HourRow in Form.HourRows)
                        {
                            counter += HourRow.Sickness;
                        }
                    }

                }
            }
            else
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        if (Form.Month == Month)
                        {
                            foreach (var HourRow in Form.HourRows)
                            {
                                counter += HourRow.Sickness;

                            }
                        }
                    }

                }
            }

            return counter;

        }

        public int TotalHoursVacation(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {

            int counter = 0;
            if (Month == null)
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        foreach (var HourRow in Form.HourRows)
                        {
                            counter += HourRow.Vacation;
                        }
                    }

                }
            }
            else
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        if (Form.Month == Month)
                        {
                            foreach (var HourRow in Form.HourRows)
                            {
                                counter += HourRow.Vacation;

                            }
                        }
                    }

                }
            }

            return counter;

        }
        public int TotalHoursHoliday(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {

            int counter = 0;
            if (Month == null)
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        foreach (var HourRow in Form.HourRows)
                        {
                            counter += HourRow.Holiday;
                        }
                    }

                }
            }
            else
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        if (Form.Month == Month)
                        {
                            foreach (var HourRow in Form.HourRows)
                            {
                                counter += HourRow.Holiday;

                            }
                        }
                    }

                }
            }

            return counter;

        }
        public int TotalHoursTraining(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {

            int counter = 0;
            if (Month == null)
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        foreach (var HourRow in Form.HourRows)
                        {
                            counter += HourRow.Training;
                        }
                    }

                }
            }
            else
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        if (Form.Month == Month)
                        {
                            foreach (var HourRow in Form.HourRows)
                            {
                                counter += HourRow.Training;

                            }
                        }
                    }

                }
            }

            return counter;

        }
        public int TotalHoursOther(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {

            int counter = 0;
            if (Month == null)
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        foreach (var HourRow in Form.HourRows)
                        {
                            counter += HourRow.Other;
                        }
                    }

                }
            }
            else
            {
                foreach (var Form in DeclarationFormList)
                {
                    if (Form.Year == Year)
                    {
                        if (Form.Month == Month)
                        {
                            foreach (var HourRow in Form.HourRows)
                            {
                                counter += HourRow.Other;

                            }
                        }
                    }

                }
            }

            return counter;

        }

        public bool CheckIfIdMatches(string uniqueId)
        {
            try
            {
                var form = context.DeclarationForms.Include(df => df.HourRows).Single(p => p.uniqueId == uniqueId);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void CalculateTotalHours(DeclarationFormModel decModel)
        {
            foreach (var HourRow in decModel.HourRows)
            {
                var declarationformEntity = context.DeclarationForms.Single(df => df.DeclarationFormId == decModel.FormId);
                declarationformEntity.TotalWorkedHours += HourRow.Worked;
                declarationformEntity.TotalOvertime += HourRow.Overtime;
                declarationformEntity.TotalSickness += HourRow.Sickness;
                declarationformEntity.TotalVacation += HourRow.Vacation;
            }
            context.SaveChanges();
        }

        public void ReopenForm(int formId)
        {
            var entity = context.DeclarationForms.Single(d => d.DeclarationFormId == formId);
            entity.Submitted = false;
            context.SaveChanges();
        }
    }
}