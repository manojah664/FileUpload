using Gallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gallery.Controllers
{
    public class userController : Controller
    {
        // GET: user

        public ActionResult Index()
        {
            return View();
        }

       [HttpPost]
        public ActionResult Index(HttpPostedFileBase ImageFile,Member member)
        {
            MEntities mEntities = new MEntities();

            string FileName = Path.GetFileName(ImageFile.FileName);

            //To Get File Extension  
            string FileExtension = "~/Gallery/"+FileName;

            ImageFile.SaveAs(Server.MapPath(FileExtension));

            Image image = new Image();
            var a = member.Name;
            var b = member.PhoneNumber;
            var c = FileExtension;
            var d = FileName;

            image.MemberName = a;
            image.PhoneNumber = b;
            image.ImagePath = c;
            image.ImageName = d;
            mEntities.Images.Add(image);
            mEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetData( )

        {
            MEntities entities = new MEntities();
            var v = entities.Images.ToList();
            return View(v);
        }


        public ActionResult Edit(int Id)
        {
            //here, get the student from the database in the real application
            MEntities entities = new MEntities();

            //getting a student from collection for demo purpose
            var std = entities.Images.Where(s => s.MemberId == Id).FirstOrDefault();

            return View(std);
        }
        [HttpPost]
        public  ActionResult Edit(Image image)
        {
            MEntities entities = new MEntities();

            if (image.ModifiedName!=null)
            {
                image.IsModified = true;

                entities.Entry(image).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}