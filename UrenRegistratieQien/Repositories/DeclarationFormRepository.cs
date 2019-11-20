using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.DatabaseClasses;
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


        public DeclarationFormModel GetForm(string userId, string month)
        {
            var entity = context.DeclarationForms.Single(d => d.EmployeeId == userId && d.Month == month);
            var form = new DeclarationFormModel
            {
                FormId = entity.DeclarationFormId,
                HourRows = hourRowRepo.GetHourRows(userId, entity.Month), //niet heel netjes om en andere repo te gebruiken
                EmployeeId = entity.EmployeeId,
                Month = entity.Month,
                Approved = entity.Approved,
                Submitted = entity.Submitted,
                Comment = entity.Comment
            };
            return form;
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

                var newModel = new DeclarationFormModel
                {

                    FormId = form.DeclarationFormId,
                    HourRows = ListOfHourRowModels,
                    EmployeeId = form.EmployeeId,
                    Month = form.Month,
                    Approved = form.Approved,
                    Submitted = form.Submitted,
                    Comment = form.Comment
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

                var newModel = new DeclarationFormModel
                {

                    FormId = form.DeclarationFormId,
                    HourRows = ListOfHourRowModels,
                    EmployeeId = form.EmployeeId,
                    Month = form.Month,
                    Approved = form.Approved,
                    Submitted = form.Submitted,
                    Comment = form.Comment
                };
                forms.Add(newModel);
            }
            return forms;
        }


        public void AddEmptyDeclarationForm(int year, string month, string userId)
        {
            var newDeclarationForm = new DeclarationForm();
            var generatedHourRows = hourRowRepo.AddHourRows(year, month, newDeclarationForm.DeclarationFormId);
            newDeclarationForm.HourRows = generatedHourRows;
            newDeclarationForm.EmployeeId = userId;
            newDeclarationForm.Month = month;
            newDeclarationForm.Approved = false;
            newDeclarationForm.Submitted = false;

            context.SaveChanges();
        }

        public void EditDeclarationForm(DeclarationFormModel formModel)
        {
            var form = context.DeclarationForms.Single(d => d.EmployeeId == formModel.EmployeeId);
            var hourList = new List<HourRow>();

            foreach (var row in formModel.HourRows)
            {
                var newRow = new HourRow
                {
                    Worked = row.Worked,
                    Overtime = row.Overtime,
                    Sickness = row.Sickness,
                    Vacation = row.Vacation,
                    Holiday = row.Holiday,
                    Training = row.Training,
                    Other = row.Other,
                    OtherExplanation = row.OtherExplanation
                };
                hourList.Add(newRow);
            }

            form.HourRows = hourList;
            form.Approved = formModel.Approved;
            form.Submitted = formModel.Submitted;
            form.Comment = formModel.Comment;

            context.SaveChanges();
        }


    }
}
