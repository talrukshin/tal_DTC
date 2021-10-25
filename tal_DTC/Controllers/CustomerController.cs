using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tal_DTC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Customer c)
        {
            try
            {
                using (dogsEntities db = new dogsEntities())
                {
                    var cust = db.Customers.Single(cu => cu.UserName == c.UserName && cu.Password == c.Password);
                    if (cust != null)
                    {
                        Session["UserName"] = cust.UserName.ToString();
                        Session["Password"] = cust.Password.ToString();
                        return RedirectToAction("Index", "Appointment");// table appartment
                    }
                    else
                    {
                        ModelState.AddModelError("", "UserName or Password ir wrong.");
                    }

                }
                return View();
            }
            catch (Exception)
            {

                //ModelState.AddModelError("", "UserName or Password ir wrong.");
                //return View("UserName or Password ir wrong.");//User not in DB
                return RedirectToAction("LogIn");

            }

        }


        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Customer c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (dogsEntities db = new dogsEntities())
                    {
                        db.Customers.Add(c);
                        db.SaveChanges();
                    }
                    ModelState.Clear();
                    ViewBag.Message = c.Name + " successfullly registered.";
                }
                return View();
            }
            catch (Exception)
            {
                ModelState.Clear();
                ViewBag.Message = "User Name: "+ c.UserName + " is already exists in the system.";
                return View();
            }
        }


        //public ActionResult ShowAppointmentsList()
        //{
        //    using (dogsEntities db = new dogsEntities())
        //    {
        //        var AppointmentsList = db.Database.SqlQuery<ViewAppointments_Result>("exec ViewAppointments").ToList<ViewAppointments_Result>();

        //        return View(AppointmentsList.ToList());
        //    }

        //}


    }
}