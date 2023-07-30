using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_media_Guide.Models;
using Newtonsoft.Json;

namespace Multi_media_Guide.Controllers
{
    public class UserController : Controller
    {
        job_interviewEntities db = new job_interviewEntities();
        // GET: User
        public ActionResult Index()
        {
            var list = db.applicant_tbl.ToList();
            return View(list);
        }

        [HttpGet]
        public JsonResult GetId(int id)
        {
            var result = db.applicant_tbl.Where(x => x.Id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult edit(applicant_tbl user)
        {
            var entity = db.applicant_tbl.Where(x => x.Id == user.Id).FirstOrDefault();

            if (entity != null)
            {
                entity.f_name = user.f_name;
                entity.m_name = user.m_name;
                entity.l_name = user.l_name;
                entity.a_address = user.a_address;
                entity.a_contact_num = user.a_contact_num;
                entity.a_email_add = user.a_email_add;
                entity.a_user = user.a_user;
                entity.a_pass = user.a_pass;

                db.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult delete_user(int id)
        {
            var entity = db.applicant_tbl.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {
                db.applicant_tbl.Remove(entity);
                db.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public ActionResult Print()
        {
            var list = db.applicant_tbl.ToList();
            return View(list);
        }
    }


}