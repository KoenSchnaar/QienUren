using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    public class RejectFormModel
    {
        public DeclarationFormModel declarationFormModel { get; set; }

        [Required]
        public string comment { get; set; }
        public bool commentNotValid { get; set; }

    }
}
