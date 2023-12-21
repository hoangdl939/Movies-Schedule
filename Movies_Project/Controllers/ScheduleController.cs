using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Project.Models;

namespace Movies_Project.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly PE_PRN_Fall2023B1Context db;

        public ScheduleController(PE_PRN_Fall2023B1Context _db)
        {
            db = _db;
        }

        public IActionResult Create()
        {

            var rooms = db.Rooms.Distinct().ToList();

            var timeSlots = db.TimeSlots.Distinct().ToList();

            var movies = db.Movies.Distinct().ToList();



            ViewBag.rooms = rooms;
            ViewBag.timeSlots = timeSlots;
            ViewBag.movies = movies;

            return View();
        }



        [HttpPost]
        public IActionResult Create(Schedule schedule)
        {
            DateTime timeNow = DateTime.Now;

            if(!((schedule.MovieId is int) || schedule.TimeSlotId is int || schedule.RoomId is int))
            {
                ViewBag.ErrorMessage = "Error!";
                getSetData();
                return View();
            }
            if(schedule.StartDate < timeNow || schedule.EndDate < timeNow)
            {
                ViewBag.ErrorMessage = "Time must be later than time now";
                getSetData();
                return View();
            }
            if (schedule.EndDate >= schedule.StartDate)
            {
                bool isExist = db.Schedules.Any(s => s.TimeSlotId == schedule.TimeSlotId && s.MovieId == schedule.MovieId &&( (s.StartDate <= schedule.StartDate && s.EndDate >= schedule.StartDate)));


                if (!isExist)
                {

                    db.Schedules.Add(schedule);
                    db.SaveChanges();

                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.ErrorMessage = "Duplicated Schedule!";
                    getSetData();
                    return View();
                }


            }

            //add to viewbag
            ViewBag.ErrorMessage = "End Date must be later than Start Date!";

            getSetData();

            return View();





        }

        public IActionResult List()
        {
            var schedules = db.Schedules.Include(s => s.Movie).Include(s => s.Room).Include(s => s.TimeSlot).ToList();
            return View(schedules);
        }

        public void getSetData()
        {
            var rooms = db.Rooms.Distinct().ToList();

            var timeSlots = db.TimeSlots.Distinct().ToList();

            var movies = db.Movies.Distinct().ToList();



            ViewBag.rooms = rooms;
            ViewBag.timeSlots = timeSlots;
            ViewBag.movies = movies;
        }

    }
}
