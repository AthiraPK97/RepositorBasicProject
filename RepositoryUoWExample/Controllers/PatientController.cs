using Microsoft.AspNetCore.Mvc;
using RepositoryUoWExample.Layer1;
using RepositoryUoWExample.Models;

namespace RepositoryUoWExample.Controllers
{
       public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var patients = _unitOfWork.Patients.GetAll();
            return View(patients);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
      
        public async Task<ActionResult> Create(Patient patient)
        { 
            if (ModelState.IsValid)
        {
                await _unitOfWork.Patients.AddAsync(patient);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }

}

