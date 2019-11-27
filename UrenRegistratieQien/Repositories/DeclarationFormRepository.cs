using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public DeclarationFormModel GetForm(int declarationFormId, string userId)
        {
            var entity = context.DeclarationForms.Single(d => d.DeclarationFormId == declarationFormId);
            var form = new DeclarationFormModel
            {
                FormId = entity.DeclarationFormId,
                HourRows = hourRowRepo.GetHourRows(userId, declarationFormId), //niet heel netjes om en andere repo te gebruiken
                EmployeeId = entity.EmployeeId,
                Month = entity.Month,
                Approved = entity.Approved,
                Submitted = entity.Submitted,
                Comment = entity.Comment,
                Year = entity.Year
            };
            return form;
        }

        public DeclarationFormModel GetFormByFormId(int formId)
        {
            var entity = context.DeclarationForms.Include(df => df.HourRows).Single(d => d.DeclarationFormId == formId);
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
                Comment = entity.Comment
            };

            return newModel;

        }
        //get forms die niet goedgekeurd zijn
        public List<DeclarationFormModel> GetNotApprovedForms()
        {
            var entities = context.DeclarationForms.Include(df => df.HourRows).OrderByDescending(df => df.DeclarationFormId).Where(df => df.Approved == false).ToList();

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
                    Year = form.Year
                };

                forms.Add(newModel);
            }
            return forms;
        }

        public List<DeclarationFormModel> GetFilteredForms(string employeeId, string month, string approved, string submitted)
        {
            if(approved == "Goedgekeurd")
            {
                approved = "true";
            }
            if(approved == "Niet goedgekeurd")
            {
                approved = "false";
            }
            if (submitted == "Ingediend")
            {
                submitted = "true";
            }
            if (submitted == "Niet ingediend")
            {
                submitted = "false";
            }

            var entities = context.DeclarationForms.Include(df => df.HourRows)
                .OrderByDescending(df => df.DeclarationFormId).ToList();

            List<DeclarationForm> holderList = new List<DeclarationForm>();

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

                bool boolApproved = Convert.ToBoolean(approved);
                foreach (DeclarationForm entity in entities)
                    {
                    
                        if (entity.Approved != boolApproved)
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
                    Year = form.Year
                };

                forms.Add(newModel);
            }
            return forms;

        }

        public List<DeclarationFormModel> GetAllForms()
        {
            var entities = context.DeclarationForms.Include(df => df.HourRows).OrderByDescending(df => df.DeclarationFormId).ToList();
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
                    Year = form.Year
                };

                forms.Add(newModel);
            }
            return forms;
        }


        public List<DeclarationFormModel> GetAllFormsOfUser(string userId)
        {
            var entities = context.DeclarationForms.Include(df => df.HourRows).Where(d => d.EmployeeId == userId).ToList();
            var forms = new List<DeclarationFormModel>();

            

            foreach (var form in entities)
            {
                List<HourRowModel> ListOfHourRowModels = new List<HourRowModel>();

                foreach(HourRow hourRow in form.HourRows)
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
                var employee = context.Users.Single(m => m.Id == form.EmployeeId);
                var employeeCasted = (Employee)employee;
                var newModel = new DeclarationFormModel
                {


                    EmployeeName = employeeCasted.FirstName + " " + employeeCasted.LastName,
                    FormId = form.DeclarationFormId,
                    HourRows = ListOfHourRowModels,
                    EmployeeId = form.EmployeeId,
                    Month = form.Month,
                    Approved = form.Approved,
                    Submitted = form.Submitted,
                    Comment = form.Comment,
                    Year = form.Year
                };
                forms.Add(newModel);
            }
            return forms;
        }

        public List<DeclarationFormModel> GetAllFormsOfMonth(int month)
        {
            var monthString = MonthConverter.ConvertIntToMonth(month);
            var entities = context.DeclarationForms.Include(df => df.HourRows).Where(d => d.Month == monthString).ToList();
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
                var employee = context.Users.Single(m => m.Id == form.EmployeeId);
                var employeeCasted = (Employee)employee;
                var newModel = new DeclarationFormModel
                {


                    EmployeeName = employeeCasted.FirstName + " " + employeeCasted.LastName,
                    FormId = form.DeclarationFormId,
                    HourRows = ListOfHourRowModels,
                    EmployeeId = form.EmployeeId,
                    Month = form.Month,
                    Approved = form.Approved,
                    Submitted = form.Submitted,
                    Comment = form.Comment,
                    Year = form.Year
                };
                forms.Add(newModel);
            }
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
    }
}