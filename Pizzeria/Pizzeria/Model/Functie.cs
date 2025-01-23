using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Model
{
    public class Functie
    {
        public int Id { get; set; } // Primary key
        public string Naam { get; set; } // Bijvoorbeeld 'Manager', 'Bezorger', 'Medewerker'
                                         // Navigatie-eigenschap voor de relatie
       
    }
}
