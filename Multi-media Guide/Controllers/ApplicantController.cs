using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_media_Guide.Models;

namespace Multi_media_Guide.Controllers
{
    public class ApplicantController : Controller
    {
        job_interviewEntities db = new job_interviewEntities();
        // GET: Applicant
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var entity = db.applicant_tbl.Where(x => x.a_user.Equals(username) && x.a_pass.Equals(password)).FirstOrDefault();

            Session["userId"] = entity.f_name;

            if (entity != null)
            {
                
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult About()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Contact(tbl_message tm)
        {
            try
            {
                db.tbl_message.Add(tm);
                db.SaveChanges();

                ViewBag.message = "true";

                return View();
            }
            catch(Exception ex)
            {
                ViewBag.error = ex;
                return View();
            }
            
        }

        public ActionResult Gallery(string town)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login");
            }

            if(town != null)
            {
                var list = db.video_tbl.Where(x => x.v_category == town).ToList();
                return View(list);
            }
            else
            {
                return View(db.video_tbl.ToList());
            }
           
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(applicant_tbl apt)
        {
            try
            {
                Session["userId"] = apt.f_name;
                db.applicant_tbl.Add(apt);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.error = ex;
                return View();
            }
            
        }
    }
}