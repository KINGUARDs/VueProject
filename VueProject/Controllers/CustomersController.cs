using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VueProject.Models;

namespace VueProject.Controllers
{
    public class CustomersController : Controller
    {
        private readonly NorthwindContext _dbContext;

        public CustomersController(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.Country = new SelectList(_dbContext.Customers.Select(c => c.Country).Distinct());
            return View();
        }
    }
}
