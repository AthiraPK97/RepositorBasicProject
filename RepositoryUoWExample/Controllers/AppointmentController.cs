using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepositoryUoWExample.Data;
using RepositoryUoWExample.Layer1;
using RepositoryUoWExample.Models;

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
        public async Task<ActionResult> Create()
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();

            ViewBag.PatientList = new SelectList(patients, "PatientId", "Name");
            return View();
        }


        // POST: Appointment/Create

        //[HttpPost]
        //public async Task<ActionResult> Create(Appointment appointment)
        //{
        //    await using var transaction = await _unitOfWork.BeginTransactionAsync();

        //    try
        //    {
        //        await _unitOfWork.Appointments.AddAsync(appointment);

        //        var patient = await _unitOfWork.Patients.GetAsync(appointment.PatientId);
        //        patient.Remarks = "Appointment Booked Asynchronously";

        //        await _unitOfWork.Complete();
        //        await transaction.CommitAsync();

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();
        //        ModelState.AddModelError("", "Failed to book appointment: " + ex.Message);
        //        return View(appointment);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> Create(Appointment appointment)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _unitOfWork.Appointments.AddAsync(appointment);

                var patient = await _unitOfWork.Patients.GetAsync(appointment.PatientId);
                if (patient != null)
                {
                    patient.Remarks = "Appointment Booked Asynchronously";
                }

                // Simulate an error (force rollback)
               // throw new Exception("Test rollback triggered");

                await _unitOfWork.Complete(); // This won't be reached
                await transaction.CommitAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                var patients = await _unitOfWork.Patients.GetAllAsync();
                ViewBag.PatientList = new SelectList(patients, "PatientId", "Name", appointment.PatientId);

                ModelState.AddModelError("", "Failed to book appointment: " + ex.Message);
                return View(appointment);
            }
        }


        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }

}
