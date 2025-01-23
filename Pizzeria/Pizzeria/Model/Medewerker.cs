using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pizzeria.Model
{
    public class Medewerker
    {
        public int Id { get; set; }
        public string Naam { get; set; } // Naam van de medewerker
        public string Email { get; set; } // Emailadres van de medewerker
        public string Wachtwoord { get; set; } // Gehashte wachtwoord
        public int FunctieId { get; set; } // Relatie naar de Functie
        public Functie Functie { get; set; } // Navigatie-eigenschap
    }
}
