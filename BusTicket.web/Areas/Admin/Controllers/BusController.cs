using BusTicket.DataAcess.Infrastructure;
using BusTicket.DataAcess.Repositories;
using BusTickets.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicket.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusController(IUnitOfWork unitOfWork)
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
        public IActionResult GetAllBus()
        {
            var bus = _unitOfWork.BusRepository.GetAll();
            return Json(new { data = bus });
        }
        #endregion
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult CreateUpdate(int? id)
        {
            BusVm vm = new BusVm();
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Bus = _unitOfWork.BusRepository.GetFirstOrDefault(x => x.Id == id);
            }
            if (vm.Bus == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUpdate(BusVm vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Bus.Id == 0)
                {
                    _unitOfWork.BusRepository.Insert(vm.Bus);
                    TempData["Sucess"] = "Bus created done!";
                }
                else
                {
                    _unitOfWork.BusRepository.Update(vm.Bus);
                    TempData["Sucess"] = "Bus Update done!";

                }
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");


        }

        #region DeleteAPICALL
        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int?id )
        {
            var bus = _unitOfWork.BusRepository.GetFirstOrDefault(x => x.Id == id);
            if (bus == null)
            {
                return Json(new { success = false, message = "Error in fetching data" });
            }
            else
            {
                _unitOfWork.BusRepository.Delete(bus);
                _unitOfWork.save();
                return Json(new { success = true, message = "Successfully deleted" });
            }
        }
        
        #endregion
    }
}
