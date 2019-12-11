using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Exceptions;
using UrenRegistratieQien.GlobalClasses;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public class DeclarationFormRepository : IDeclarationFormRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHourRowRepository hourRowRepo;
        public object ViewBag { get; set; }

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


        public async Task CreateFormForUser(string EmployeeId, string month, int year)
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
                TotalHoliday = 0,
                TotalTraining = 0,
                TotalOther = 0,
                DateCreated = DateTime.Now
            };

            await context.DeclarationForms.AddAsync(newForm);
            await context.SaveChangesAsync();
        }


        public async Task CreateForm(string employeeId)
        {
            var entities = context.DeclarationForms.Where(p => p.EmployeeId == employeeId).ToList();
            if (entities.Count() > 0)
            {
                var entity = entities[entities.Count() - 1];
                var monthInt = MonthConverter.ConvertMonthToInt(entity.Month) + 1;
                if (monthInt == 13)
                {
                    monthInt = 1;
                }
                var monthString = MonthConverter.ConvertIntToMonth(monthInt);
                var year = entity.Year;
                if (monthString == "Januari")
                {
                    year += 1;
                }

                if (monthInt == 1)
                {
                    monthInt = 13;
                }
                if (monthInt == 2 && DateTime.Now.Month != 1)
                {
                    monthInt = 14;
                }
                if (monthInt <= DateTime.Now.Month + 1)
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
                        TotalHoliday = 0,
                        TotalTraining = 0,
                        TotalOther = 0,
                        DateCreated = DateTime.Now
                    };
                    await context.DeclarationForms.AddAsync(form);
                } else {
                    return;
                }
            }
            else
            {
                var form = new DeclarationForm
                {
                    EmployeeId = employeeId,
                    Month = MonthConverter.ConvertIntToMonth(DateTime.Now.Month),
                    Year = DateTime.Now.Year,
                    uniqueId = GenerateUniqueId(),
                    Approved = "Pending",
                    Submitted = false,
                    TotalWorkedHours = 0,
                    TotalOvertime = 0,
                    TotalSickness = 0,
                    TotalVacation = 0,
                    TotalHoliday = 0,
                    TotalTraining = 0,
                    TotalOther = 0,
                    DateCreated = DateTime.Now
                };
                await context.DeclarationForms.AddAsync(form);
            }
            await context.SaveChangesAsync();
        }

        public async Task ApproveForm(int formId)
        {
            var form = await context.DeclarationForms.SingleAsync(p => p.DeclarationFormId == formId);
            form.Approved = "Approved";
            await context.SaveChangesAsync();
        }

        public async Task RejectForm(int formId, string comment)
        {
            var form = await context.DeclarationForms.SingleAsync(p => p.DeclarationFormId == formId);
            form.Approved = "Rejected";
            form.Comment = comment;
            await context.SaveChangesAsync();
        }

        public async Task<DeclarationFormModel> GetFormModelFromEntity(DeclarationForm entity)
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

            var selectedEmployee = await context.Users.SingleAsync(p => p.Id == entity.EmployeeId);
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
                TotalHoliday = entity.TotalHoliday,
                TotalTraining = entity.TotalTraining,
                TotalOther = entity.TotalOther,
                DateCreated = entity.DateCreated
            };
            return newModel;
        }

        public async Task<List<DeclarationFormModel>> GetFormModelsFromEntities(List<DeclarationForm> entities)
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
                var selectedEmployee = await context.Users.SingleAsync(p => p.Id == form.EmployeeId);
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
                    TotalHoliday = form.TotalHoliday,
                    TotalTraining = form.TotalTraining,
                    TotalOther = form.TotalOther,
                    DateCreated = form.DateCreated
                };

                forms.Add(newModel);
            }
            return forms;
        }

        public async Task<DeclarationFormModel> GetForm(int formId)
        {
            var entity = await context.DeclarationForms.Include(df => df.HourRows).SingleAsync(d => d.DeclarationFormId == formId);
            return await GetFormModelFromEntity(entity);
        }

        public DeclarationFormModel GetFormNotAsync(int formId)
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
                Comment = entity.Comment,
                uniqueId = entity.uniqueId,
                TotalWorkedHours = entity.TotalWorkedHours,
                TotalOvertime = entity.TotalOvertime,
                TotalSickness = entity.TotalSickness,
                TotalVacation = entity.TotalVacation,
                TotalHoliday = entity.TotalHoliday,
                TotalTraining = entity.TotalTraining,
                TotalOther = entity.TotalOther,
                DateCreated = entity.DateCreated
            };

            return newModel;
        }


        public async Task<List<DeclarationFormModel>> GetFilteredForms(string year, string employeeId, string month, string approved, string submitted, string sortDate)
        {
            approved = FormStatusConverter.ConvertApproved(approved);
            submitted = FormStatusConverter.ConvertSubmitted(submitted);

            var entities = new List<DeclarationForm>();
            if (sortDate == "Ascending")
            {
                entities = await context.DeclarationForms.Include(df => df.HourRows)
                .OrderBy(df => df.DeclarationFormId).ToListAsync();
            }
            else
            {
                entities = await context.DeclarationForms.Include(df => df.HourRows)
                .OrderByDescending(df => df.DeclarationFormId).ToListAsync();
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
            if (employeeId != null)
            {
                foreach (DeclarationForm entity in entities)
                {
                    if (entity.EmployeeId != employeeId)
                    {
                        holderList.Add(entity);
                    }
                }
            }
            if (month != null)
            {
                foreach (DeclarationForm entity in entities)
                {
                    if (entity.Month != month)
                    {
                        holderList.Add(entity);
                    }
                }
            }
            if (approved != null)
            {
                foreach (DeclarationForm entity in entities)
                {
                    if (entity.Approved != approved)
                    {
                        holderList.Add(entity);
                    }
                }
            }
            if (submitted != null)
            {
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

            var forms = await GetFormModelsFromEntities(entities);
            return forms;
        }

        public async Task<List<DeclarationFormModel>> GetAllForms()
        {
            var entities = await context.DeclarationForms.Include(df => df.HourRows).OrderByDescending(df => df.DeclarationFormId).ToListAsync();
            var forms = await GetFormModelsFromEntities(entities);
            return forms;
        }


        public async Task<List<DeclarationFormModel>> GetAllFormsOfUser(string userId)
        {
            var entities = await context.DeclarationForms.Include(df => df.HourRows).Where(d => d.EmployeeId == userId).ToListAsync();
            var forms = await GetFormModelsFromEntities(entities);
            return forms;
        }

        public async Task<List<DeclarationFormModel>> GetAllFormsOfMonth(int month)
        {
            var monthString = MonthConverter.ConvertIntToMonth(month);
            var entities = await context.DeclarationForms.Include(df => df.HourRows).Where(d => d.Month == monthString).ToListAsync();
            var forms = await GetFormModelsFromEntities(entities);
            return forms;
        }

        public async Task EditDeclarationForm(DeclarationFormModel formModel)
        {
            var form = await context.DeclarationForms.SingleAsync(d => d.DeclarationFormId == formModel.FormId);
            var hourList = new List<HourRow>();
            
            foreach (var row in formModel.HourRows)
            {
                var entity = await context.HourRows.SingleAsync(h => h.HourRowId == row.HourRowId);
                entity.Worked = row.Worked;
                entity.Overtime = row.Overtime;
                entity.Sickness = row.Sickness;
                entity.Vacation = row.Vacation;
                entity.Holiday = row.Holiday;
                entity.Training = row.Training;
                entity.Other = row.Other;
                entity.OtherExplanation = row.OtherExplanation;
                row.TotalRow = row.Worked + row.Overtime + row.Sickness + row.Vacation + row.Holiday + row.Training + row.Other;

                if (row.TotalRow > 24)
                {
                    throw new MoreThan24HoursException();
                }
                else
                {
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task SubmitDeclarationForm(DeclarationFormModel formModel)
        {
            var form = await context.DeclarationForms.SingleAsync(d => d.DeclarationFormId == formModel.FormId);
            form.Submitted = true;
            form.Approved = "Pending";
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfIdMatches(string uniqueId)
        {
            try
            {
                var form = await context.DeclarationForms.Include(df => df.HourRows).SingleAsync(p => p.uniqueId == uniqueId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task CalculateTotalHours(DeclarationFormModel decModel)
        {
            var declarationformEntity = await context.DeclarationForms.SingleAsync(df => df.DeclarationFormId == decModel.FormId);
            declarationformEntity.TotalWorkedHours = 0;
            declarationformEntity.TotalOvertime = 0;
            declarationformEntity.TotalSickness = 0;
            declarationformEntity.TotalVacation = 0;
            declarationformEntity.TotalHoliday = 0;
            declarationformEntity.TotalTraining = 0;
            declarationformEntity.TotalOther = 0;
            foreach (var HourRow in decModel.HourRows)
            {
                declarationformEntity.TotalWorkedHours += HourRow.Worked;
                declarationformEntity.TotalOvertime += HourRow.Overtime;
                declarationformEntity.TotalSickness += HourRow.Sickness;
                declarationformEntity.TotalVacation += HourRow.Vacation;
                declarationformEntity.TotalHoliday += HourRow.Holiday;
                declarationformEntity.TotalTraining += HourRow.Training;
                declarationformEntity.TotalOther += HourRow.Other;
            }
            await context.SaveChangesAsync();
        }

        public TotalsModel CalculateTotalHoursOfAll(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            var declarationForms = DeclarationFormList.Where(df => df.Year == Year);
            if (!string.IsNullOrEmpty(Month))
                declarationForms = declarationForms.Where(df => df.Month == Month);
            var totalsmodel = new TotalsModel
            {
                TotalWorkedHours = declarationForms.Sum(df => df.TotalWorkedHours),
                TotalOvertime = declarationForms.Sum(df => df.TotalOvertime),
                TotalSickness = declarationForms.Sum(df => df.TotalSickness),
                TotalVacation = declarationForms.Sum(df => df.TotalVacation),
                TotalHoliday = declarationForms.Sum(df => df.TotalHoliday),
                TotalTraining = declarationForms.Sum(df => df.TotalTraining),
                TotalOther = declarationForms.Sum(df => df.TotalOther)
            };
            return totalsmodel;
        }
                      

        public async Task ReopenForm(int formId)
        {
            var entity = await context.DeclarationForms.SingleAsync(d => d.DeclarationFormId == formId);
            entity.Submitted = false;
            await context.SaveChangesAsync();
        }
        public async Task DeleteDeclarationForm(int FormId)
        {
            var form = await context.DeclarationForms.SingleAsync(df => df.DeclarationFormId == FormId);
            context.DeclarationForms.Remove(form);
            await context.SaveChangesAsync();
        }

        public async Task<List<TotalsForChartModel>> TotalHoursForCharts(int year)
        {
            var totalHoursList = new List<TotalsForChartModel>();
            for (int i = 1; i < 13; i++)
            {
                var totalsModel = new TotalsForChartModel
                {
                    Month = MonthConverter.ConvertIntToMonth(i)
                };
                totalHoursList.Add(totalsModel);
            }

            foreach(var model in totalHoursList)
            {
                var entities = await context.DeclarationForms.Where(d => d.Month == model.Month && d.Year == year).ToListAsync();
                foreach(var entity in entities)
                {
                    model.TotalHoliday += entity.TotalHoliday;
                    model.TotalOther += entity.TotalOther;
                    model.TotalOvertime += entity.TotalOvertime;
                    model.TotalSickness += entity.TotalSickness;
                    model.TotalTraining += entity.TotalTraining;
                    model.TotalVacation += entity.TotalVacation;
                    model.TotalWorkedHours += entity.TotalWorkedHours;
                };
            }
            return totalHoursList;
        }

        public async Task<List<int>> GetAllYears()
        {
            var entities = await context.DeclarationForms.ToListAsync();
            var years = new List<int>();
            foreach (var entity in entities)
            {
                if (!years.Contains(entity.Year))
                {
                    years.Add(entity.Year);
                }
            }
            return years;
        }
    }
}


        
