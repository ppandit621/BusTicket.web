using BusTicket.DataAcess.Infrastructure;
using BusTickets.Models;
using BusTickets.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace BusTicket.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeatController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SeatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        #region APICALL
        [HttpGet]
        public IActionResult GetAllSeat()
        {
            var seat = _unitOfWork.SeatRepository.GetAll("Bus");
            return Json(new { data = seat });
        }
        [HttpGet]
        public IActionResult GetAllSeatByBusId(int id)
        {
            var seat = _unitOfWork.SeatRepository.GetByBusId(id);
            return Json(new { data = seat });
        }
        #endregion
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUpdate(int? id)
        {
            SeatDetails tm = new SeatDetails();
            var bus = _unitOfWork.BusRepository.GetAll().ToList();
            ViewData["bus"] = bus;
            if (id == null || id == 0)
            {

                return View(tm);
            }
            else
            {
                tm = _unitOfWork.SeatRepository.GetFirstOrDefault(x => x.Id == id);
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUpdate(SeatDetails seat)
        {
            seat.Bus= _unitOfWork.BusRepository.GetFirstOrDefault(x => x.Id == seat.BusId);
            if (ModelState.IsValid)
            {
                if (seat.Id == 0)
                {
                    _unitOfWork.SeatRepository.Insert(seat);
                    TempData["Sucess"] = "Seat created done!";
                }
                else
                {
                    _unitOfWork.SeatRepository.Update(seat);
                    TempData["Sucess"] = "Seat Update done!";

                }
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");


        }


        #region DeleteAPICALL
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            var seat = _unitOfWork.SeatRepository.GetFirstOrDefault(x => x.Id == id);
            if (seat == null)
            {
                return Json(new { success = false, message = "Error in fetching data" });
            }
            else
            {
                _unitOfWork.SeatRepository.Delete(seat);
                _unitOfWork.save();
                return Json(new { success = true, message = "Successfully Deleted" });

            }
        }

        #endregion
    }
}
