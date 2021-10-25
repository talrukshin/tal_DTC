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
                        ModelState.AddModelError("", "UserName or Password is wrong.");
                    }

                }
                return View();
            }
            catch (Exception)
            {
                ModelState.Clear();
                ViewBag.Message = "UserName or Password is wrong.";
                return View();
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
                ViewBag.Message = "Error: userName "+ c.UserName + " is already exists in the system.";
                return View();
            }
        }


    }
}