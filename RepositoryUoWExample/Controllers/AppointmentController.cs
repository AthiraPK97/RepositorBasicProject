using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RepositoryUoWExample.Data;
using RepositoryUoWExample.Models;
using RepositoryUoWExample.Repository;

namespace RepositoryUoWExample.Controllers
{
    //public class AppointmentController : Controller
    //{
    //    private readonly  AppDbContext db;

    //    public AppointmentController(AppDbContext context)
    //    {
    //        db = context;
    //    }

    //    // GET: Appointment
    //    public ActionResult Index()
    //    {
    //        var appointments = db.Appointments.OrderBy(a => a.AppointmentDateTime).ToList();
    //        return View(appointments);
    //    }

    //    // GET: Appointment/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Appointment/Create
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create(Appointment appointment)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Appointments.Add(appointment);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(appointment);
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}

    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Appointment
        public ActionResult Index()
        {
            var appointments = _unitOfWork.Appointments.GetAll();
            return View(appointments);
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Appointments.Add(appointment);
                _unitOfWork.Complete(); // Saves all in one go
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }

}
