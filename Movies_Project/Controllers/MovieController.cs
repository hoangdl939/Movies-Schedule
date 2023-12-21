using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Project.Models;

namespace Movies_Project.Controllers
{
    public class MovieController : Controller
    {
        private readonly PE_PRN_Fall2023B1Context db;

        public MovieController(PE_PRN_Fall2023B1Context _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var listMovies = db.Movies.Include(m => m.Genres).Include(m => m.Director).ToList();
            return View(listMovies);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {

            // get movie by id
            var movie = db.Movies.Include(m => m.Genres).Include(m => m.MovieStars).ThenInclude(ms => ms.Star).Include(m => m.Director).FirstOrDefault(m => m.Id == id);


            var schedules = db.Schedules.Where(s => s.MovieId == id).Include(s => s.Movie).Include(s => s.Room).Include(s => s.TimeSlot).ToList();

            ViewBag.schedules = schedules;
            return View(movie);
        }
    }
}
