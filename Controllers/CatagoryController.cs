using NIMAPTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NIMAPTest.Controllers
{
    public class CatagoryController : Controller
    {
        NidhiDatabaseEntities dbcontext = new NidhiDatabaseEntities();
        // GET: NIMAP
        [HttpGet]
        public ActionResult Catagory()
        {
            try
            {

                var itemlists = dbcontext.CatagoryMasters.ToList();
                return View(itemlists);

            }
            catch (Exception ex)
            {
                TempData["status"] = "E";
                TempData["msg"] = ex.Message;
                TempData.Keep();
            }
            return View();
        }

        [HttpGet]
        public ActionResult AddCatagory(int? id)
        {
            try
            {
                if (id != 0)
                {
                    var cat = dbcontext.CatagoryMasters.Where(o => o.id == id).FirstOrDefault();
                    return View(new CatagoryViewModel { id = cat.id, CatName = cat.CatName, CatDesc = cat.CatDesc });
                }
                else
                {
                    return View(new CatagoryViewModel());
                }
            }
            catch (Exception ex)
            {
                TempData["status"] = "E";
                TempData["msg"] = ex.Message;
                TempData.Keep();
            }


            return View();
        }

        [HttpPost]
        public ActionResult AddCatagory(CatagoryViewModel objmodel)
        {
            try
            {
                if (objmodel.id != 0)
                {
                    CatagoryMaster obj = dbcontext.CatagoryMasters.Where(o => o.id == objmodel.id).FirstOrDefault();
                    obj.CatName = objmodel.CatName;
                    obj.CatDesc = objmodel.CatDesc;
                    dbcontext.SaveChanges();
                    TempData["status"] = "U";
                    TempData["msg"] = "Catagory updated Successfully.";
                    TempData.Keep();
                }
                else
                {
                    CatagoryMaster obj = new CatagoryMaster();
                    obj.CatName = objmodel.CatName;
                    obj.CatDesc = objmodel.CatDesc;
                    dbcontext.CatagoryMasters.Add(obj);
                    dbcontext.SaveChanges();
                    TempData["status"] = "A";
                    TempData["msg"] = "Catagory added successfully";
                    TempData.Keep();
                }

                ModelState.Clear();

            }
            catch (Exception ex)
            {
                TempData["status"] = "E";
                TempData["msg"] = ex.Message;
                TempData.Keep();

            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteCatagory(int id)
        {
            try
            {
                var cat = dbcontext.CatagoryMasters.Find(id);
                dbcontext.CatagoryMasters.Remove(cat);
                dbcontext.SaveChanges();
                TempData["status"] = "D";
                TempData["msg"] = "Catagory Deleted Successfully.";
                TempData.Keep();
                return RedirectToAction("Catagory");
            }
            catch (Exception ex)
            {
                TempData["status"] = "E";
                TempData["msg"] = ex.Message;
                TempData.Keep();
            }

            return View();
        }
    }
}