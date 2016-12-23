using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            String word = "Mich  el  le Acc  uso   Ste  ve  ns";
            word = Regex.Replace(word, @"\s", "");

            string x = "";


            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }


            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("dohlgo10k", "589963655715214", "59lpJcYmJaB8KxEXll5WrfAfLgA");

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            var fileURL = Path.Combine(Server.MapPath("~/Images"),Path.GetFileName(file.FileName));

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new CloudinaryDotNet.Actions.FileDescription(fileURL)
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            string url = cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));


            //List<test> testList = new List<test>();

            //using (StreamReader r = new StreamReader(Server.MapPath("/Scripts/Test.json")))
            //{
            //    string json = r.ReadToEnd();
            //    dynamic files = JsonConvert.DeserializeObject(json);



            //    foreach (var item in files)
            //    {
            //        testList.Add(new test()
            //        {
            //            Name = item.Name,
            //            Surname = item.Surname
            //        });

            //    }

            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    testList.Add(new test()
            //    {
            //        Name = "Candice",
            //        Surname = "Savahl"
            //    });
            //}

            //var jsonString = JsonConvert.SerializeObject(testList);

            //System.IO.File.WriteAllText(Server.MapPath("/Scripts/Test.json"), jsonString);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}