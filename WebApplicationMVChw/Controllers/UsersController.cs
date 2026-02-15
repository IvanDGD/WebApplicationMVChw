using Microsoft.AspNetCore.Mvc;
using WebApplicationMVChw.Models;

namespace WebApplicationMVChw.Controllers
{
    public class UsersController : Controller
    {
        private List<User> users = new List<User>
    {
        new User { Id = 1, Name = "Ivan", Position = "Manager", Age = 30, Salary = 2000 },
        new User { Id = 2, Name = "Anna", Position = "Developer", Age = 25, Salary = 3000 },
        new User { Id = 3, Name = "Oleg", Position = "Designer", Age = 28, Salary = 2500 },
        new User { Id = 4, Name = "Maria", Position = "Developer", Age = 35, Salary = 4000 }
    };

        public IActionResult Index(string name, string position, string sort)
        {
            var result = users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                result = result.Where(u => u.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(position))
                result = result.Where(u => u.Position.Contains(position));

            switch (sort)
            {
                case "age_asc":
                    result = result.OrderBy(u => u.Age);
                    break;
                case "age_desc":
                    result = result.OrderByDescending(u => u.Age);
                    break;
                case "salary_asc":
                    result = result.OrderBy(u => u.Salary);
                    break;
                case "salary_desc":
                    result = result.OrderByDescending(u => u.Salary);
                    break;
            }

            ViewBag.Name = name;
            ViewBag.Position = position;
            ViewBag.Sort = sort;

            return View(result.ToList());
        }
    }
}
