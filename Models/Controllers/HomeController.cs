using RetreiveUploadimage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;

namespace RetreiveUploadimage.Controllers
{
    public class HomeController : Controller
    {
        uploaddataEntities1 db = new uploaddataEntities1();
        public ActionResult Index()
        {
            var data = db.uploads.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(upload p)
        {

            string filePath = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extension = Path.GetExtension(p.ImageFile.FileName);
            HttpPostedFileBase postedFile = p.ImageFile;
            int length = postedFile.ContentLength;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (length <= 1000000)
                {
                    filePath = filePath + extension;
                    p.image = "~/Images/" + filePath;
                    filePath = Path.Combine(Server.MapPath("~/Images/"), filePath);
                    p.ImageFile.SaveAs(filePath);
                    db.uploads.Add(p);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        ViewBag.data = "Data saved successfully !!!";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.data = "Data not saved successfully !!!";
                    }
                }
                else
                {
                    ViewBag.sizendata = "Size should be only 1 MB !!!";
                }
            }
            else
            {
                ViewBag.extensiondata = "Image not supported !!!";
            }


            return View();
        }

    }
}