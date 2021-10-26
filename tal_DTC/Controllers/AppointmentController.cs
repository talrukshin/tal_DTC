using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tal_DTC.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment

        public ActionResult Index(string sortOrder, string searchString, string startdate)
        {
            try
            {
                using (dogsEntities db = new dogsEntities())
                {
                    ViewBag.CurrentSort = sortOrder;
                    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                    ViewBag.CurrentFilter = searchString;
                    ViewBag.startdate = startdate;

                    var AppointmentsList = from a in db.Database.SqlQuery<ViewAppointments_Result>("exec ViewAppointments")
                                           select a;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        AppointmentsList = AppointmentsList.Where(a => a.Name.Contains(searchString));
                    }
                    if (!String.IsNullOrEmpty(startdate))
                    {                       
                        AppointmentsList = AppointmentsList.Where(x => x.DateOfAppointment.ToString("dd.MM.yyyy HH:mm").Contains(startdate.ToString()));
                    }

                    switch (sortOrder)
                    {
                        case "name_desc":
                            AppointmentsList = AppointmentsList.OrderByDescending(a => a.Name);
                            break;
                        case "Date":
                            AppointmentsList = AppointmentsList.OrderBy(a => a.DateOfAppointment);
                            break;
                        case "date_desc":
                            AppointmentsList = AppointmentsList.OrderByDescending(a => a.DateOfAppointment);
                            break;
                        default:
                            AppointmentsList = AppointmentsList.OrderBy(a => a.Name);
                            break;
                    }
                    return View(AppointmentsList.ToList());
                }
            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}