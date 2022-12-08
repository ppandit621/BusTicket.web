using BusTicket.DataAcess.Infrastructure;
using BusTicket.DataAcess.Repositories;
using BusTickets.Models;
using BusTickets.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusTicket.web.Areas.Customs.Controllers
{
    [Area("Customs")]
    [Authorize(Roles ="User")]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly UserManager<IdentityUser> _userManager;

        public BookingController(IUnitOfWork unitofWork, UserManager<IdentityUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllBooking()
        {
            var user = User;
            var booking = _unitofWork.BookingRepository.GetAll("Bus,Seat");
            return Json(new { data = booking });
        }

        public IActionResult CreateUpdate(int? id)
        {
            Booking tm = new Booking();
            var bus = _unitofWork.BusRepository.GetAll().ToList();
            ViewData["bus"] = bus;
            if (id == null || id == 0)
            {

                return View(tm);
            }
            else
            {
                tm = _unitofWork.BookingRepository.GetFirstOrDefault(x => x.Id == id);
            }
            if (tm == null)
            {
                return NotFound();
            }
            else
            {
                return View(tm);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdate(Booking vm)
        {
            var user =await  _userManager.FindByEmailAsync(User.Identity.Name);
            vm.UserId = user.Id;
            vm.User = user;
            vm.Bus = _unitofWork.BusRepository.GetFirstOrDefault(x => x.Id == vm.BusId);
            vm.Seat = _unitofWork.SeatRepository.GetFirstOrDefault(x => x.Id == vm.SeatId);
            if (ModelState.IsValid)
            {
                if (vm.Id == 0)
                {
                    _unitofWork.BookingRepository.Insert(vm);
                    TempData["Sucess"] = "Bus created done!";
                }
                else
                {
                    _unitofWork.BookingRepository.Update(vm);
                    TempData["Sucess"] = "Bus Update done!";

                }
                _unitofWork.save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");


        }

        public IActionResult Delete(int? id)
        {
            var booking = _unitofWork.BookingRepository.GetFirstOrDefault(x => x.Id == id);
            if (booking == null)
            {
                return Json(new { success = false, message = "Error in fetching data" });
            }
            else
            {
                _unitofWork.BookingRepository.Delete(booking);
                _unitofWork.save();
                return Json(new { success = true, message = "Successfully Deleted" });

            }
        }

        
    }
}
