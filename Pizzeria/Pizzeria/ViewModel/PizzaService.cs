using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzeria.Model;

namespace Pizzeria.ViewModel
{
    public class PizzaService
    {
        private readonly AppDbContext _context;

        public PizzaService()
        {
            _context = new AppDbContext();
        }

        public List<Pizza> GetAllPizzas()
        {
            return _context.Pizzas.ToList();
        }
    }
}
