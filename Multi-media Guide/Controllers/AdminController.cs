using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_media_Guide.Models;
using System.IO;
using Newtonsoft.Json;

namespace Multi_media_Guide.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        job_interviewEntities db = new job_interviewEntities();
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.list = db.applicant_tbl.ToList();
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var entity = db.admin_tbl.Where(x => x.a_username == username && x.a_password == password).FirstOrDefault();

            if(entity != null)
            {
                Session["admin"] = entity.f_name;
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

        public ActionResult Scene()
        {
            /*if (Session["admin"] == null)
            {
                return RedirectToAction("Login");
            }*/
            var list = db.video_tbl.ToList();

            return View(list);
        }

        [HttpPost]
        public JsonResult Create(video_tbl sd)
        {
            if (sd.ImageUpload != null && sd.ImageUpload.ContentLength < 104857600)
            {
                string fileName = Path.GetFileNameWithoutExtension(sd.ImageUpload.FileName);
                string extension = Path.GetExtension(sd.ImageUpload.FileName);
                sd.image_path = sd.v_title + extension;
                sd.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Scene"), sd.image_path));

                db.video_tbl.Add(sd);
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

        [HttpGet]
        public JsonResult GetId(int id)
        {
            var result = db.video_tbl.Where(x => x.Id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(video_tbl dt)
        {
            var entity = db.video_tbl.Where(x => x.Id == dt.Id).FirstOrDefault();

            if (entity != null)
            {
                
                    if (dt.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(dt.ImageUpload.FileName);
                        string extension = Path.GetExtension(dt.ImageUpload.FileName);
                        dt.image_path = dt.v_title + extension;
                        dt.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Scene"), dt.image_path));

                        entity.v_title = dt.v_title;
                        entity.v_description = dt.v_description;
                        entity.v_category = dt.v_category;
                        entity.v_video_link = dt.v_video_link;
                        entity.image_path = dt.image_path;
                        db.SaveChanges();

                        return Json(new
                        {
                            status = true
                        });
                    }
                entity.v_title = dt.v_title;
                entity.v_description = dt.v_description;
                entity.v_category = dt.v_category;
                entity.v_video_link = dt.v_video_link;
                db.SaveChanges();

                return Json(new { status = true });
              
            }
            else
            {
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public JsonResult delete(int id)
        {
            var entity = db.video_tbl.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {
                db.video_tbl.Remove(entity);
                db.SaveChanges();

                return Json(new
                {
                    del = true
                });
            }
            return Json(new { del = false });
        }

        public ActionResult ChangePass()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (Session["admin"] != null)
            {
                string name = Session["admin"] as string;

                var entity = db.admin_tbl.Where(x => x.f_name == name).FirstOrDefault();
                ViewBag.id = entity.Id;
                ViewBag.name = entity.f_name+" "+entity.m_name+" "+entity.l_name;
                ViewBag.username = entity.a_username;
                ViewBag.password = entity.a_password;
            }

            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(admin_tbl c_admin)
        {
            var entity = db.admin_tbl.Where(x => x.Id == c_admin.Id).FirstOrDefault();

            entity.a_username = c_admin.a_username;
            entity.a_password = c_admin.a_password;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Message()
        {
            var list = db.tbl_message.ToList();

            return View(list);
        }

        public JsonResult view_message(int Id)
        {
            var result = db.tbl_message.Where(x => x.Id == Id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult message_del(int id)
        {
            var entity = db.tbl_message.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {
                db.tbl_message.Remove(entity);
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
    }
}