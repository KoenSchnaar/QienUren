using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    public class ClientModel
    {
        public int ClientId { get; set; }

        [Display(Name = "Naam bedrijf")]
        public string CompanyName { get; set; }

        [Display(Name = "Naam 1e contactpersoon")]
        public string Contact1Name { get; set; }

        [Display(Name = "Naam 2e contactpersoon")]
        public string Contact2Name { get; set; }

        [Display(Name = "Telefoonnummer 1e contactpersoon")]
        public string Contact1Phone { get; set; }

        [Display(Name = "Telefoonnummer 2e contactpersoon")]
        public string Contact2Phone { get; set; }

        [Display(Name = "E-mailadres 1e contactpersoon")]
        public string Contact1Email { get; set; }

        [Display(Name = "E-mailadres 2e contactpersoon")]
        public string Contact2Email { get; set; }

        [Display(Name = "Algemeen telefoonnummer")]
        public string CompanyPhone { get; set; }
    }
}
