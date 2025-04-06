using EventEase3.Models;
using EventEase3.Services;
using Microsoft.AspNetCore.Mvc;


//controllers for create, Read, edit and delete are in here
namespace EventEase3.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext context;

        public BookingsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var bookings = context.Bookings.OrderByDescending(p => p.Id).ToList();
            return View(bookings);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookingDto bookingDto)
        {
            if (bookingDto.eventDate == null)
            {
                ModelState.AddModelError("eventDate", "Event date is required");
            }

            if (!ModelState.IsValid)
            {
                return View(bookingDto);
            }

            //to save to the database
            Booking booking = new Booking
            {
                email = bookingDto.email,
                eventName = bookingDto.eventName,
                venue = bookingDto.venue,
                eventDate = bookingDto.eventDate,
                createdAt = DateTime.Now
            };

            context.Bookings.Add(booking);
            context.SaveChanges();


            return RedirectToAction("Index", "Bookings");
        }

        public IActionResult Edit(int id)
        {
            var booking = context.Bookings.Find(id);

            if (booking == null)
            {
                return RedirectToAction("Index", "Bookings");
            }

            //create the bookingDto from booking
            var bookingDto = new BookingDto
            {

                email = booking.email,
                eventName = booking.eventName,
                venue = booking.venue,
                eventDate = booking.eventDate
            };

            ViewData["BookingId"] = booking.Id;
            ViewData["CreatedAt"] = booking.createdAt.ToString("MM/dd/yyyy");

            return View(bookingDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, BookingDto bookingDto)
        {

            var booking = context.Bookings.Find(id);

            if (booking == null)
            {
                return RedirectToAction("Index", "Bookings");
            }

            if (!ModelState.IsValid)
            {
                ViewData["BookingId"] = booking.Id;
                ViewData["CreatedAt"] = booking.createdAt.ToString("MM/dd/yyyy");
                return View(bookingDto);
            }

            //to update the datdabase with teh new booking ifo

            booking.email = bookingDto.email;
            booking.eventName = bookingDto.eventName;
            booking.venue = bookingDto.venue;
            booking.eventDate = bookingDto.eventDate;


            context.SaveChanges();

            return RedirectToAction("Index", "Bookings");


        }

        public IActionResult Delete(int id)
        {
            var booking = context.Bookings.Find(id);
            if (booking == null)
            {
                return RedirectToAction("Index", "Bookings");
            }

            context.Bookings.Remove(booking);
            context.SaveChanges(true);

            return RedirectToAction("Index", "Bookings");
        }

        public ActionResult Venues()
        {
            return View();
        }


        public ActionResult Events()
        {
            return View();
        }


    }
}
