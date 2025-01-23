using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Model
{
    public class Bestelling
    {
        public int Id { get; set; }
        public int? user_id { get; set; } // Als User optioneel is
        public int status_id { get; set; } // Verwijst naar je status tabel
        public string telefoonnummer { get; set; }
        public string adres { get; set; }
        public string postcode { get; set; }
        public double totaalBedrag { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Status Status { get; set; } // Navigatie naar status
    }
}
