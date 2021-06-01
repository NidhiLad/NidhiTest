using NIMAPTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NIMAPTest.Controllers
{
    public class ProductController : Controller
    {
        NidhiDatabaseEntities dbcontext = new NidhiDatabaseEntities();
        [HttpGet]
        public ActionResult Products()
        {
            try
            {
                //var itemlists = dbcontext.ProductMasters.ToList();
                var itemlists = this.GetProducts(1);
                ViewBag.pagecount = ViewBag.Pagecnt;
                ViewBag.currentPage = ViewBag.CurrPg;
                
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

        [HttpPost]
        public ActionResult Products(int? page)
        {
            try
            {
                //var itemlists = dbcontext.ProductMasters.ToList();
                var itemlists = this.GetProducts(Convert.ToInt32(page));
                ViewBag.pagecount = ViewBag.Pagecnt;
                ViewBag.currentPage = ViewBag.CurrPg;

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
        private ProducuctListModel GetProducts(int currentPage)
        {
            int maxRows = 10;
            ProducuctListModel ProdModel = new ProducuctListModel();
            ProdModel.ProductList = (from prod in dbcontext.ProductMasters
                                       select prod)
                            .OrderBy(p => p.id)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();

            double pageCount = (double)((decimal)dbcontext.ProductMasters.Count() / Convert.ToDecimal(maxRows));
            ProdModel.PageCount = (int)Math.Ceiling(pageCount);

            ProdModel.CurrentPageIndex = currentPage;

            return ProdModel;
        }
        [HttpGet]
        public ActionResult AddProduct(int? id)
        {
            List<SelectListItem> catlist = new List<SelectListItem>();
            catlist.Add(new SelectListItem() { Text = "Select Catagory", Value = "" });
            var itemlists = dbcontext.CatagoryMasters.ToList();
            foreach (var item in itemlists)
            {
                catlist.Add(new SelectListItem() { Text = item.CatName, Value = Convert.ToString(item.id) });
            }
            ViewBag.catagory = catlist;


            try
            {
                if (id != 0)
                {
                    var prod = dbcontext.ProductMasters.Where(o => o.id == id).FirstOrDefault();
                    return View(new ProductViewModel
                    {
                        id = prod.id,
                        catid = prod.catid,
                        ProdName = prod.ProdName,
                        ProdDesc = prod.ProdDesc,
                        ProdPrice = prod.Price
                    });
                }
                else
                {
                    return View(new ProductViewModel());
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
        public ActionResult AddProduct(ProductViewModel objmodel)
        {
            List<SelectListItem> catlist = new List<SelectListItem>();
            catlist.Add(new SelectListItem() { Text = "Select Catagory", Value = "" });
            var itemlists = dbcontext.CatagoryMasters.ToList();
            foreach (var item in itemlists)
            {
                catlist.Add(new SelectListItem() { Text = item.CatName, Value = Convert.ToString(item.id) });
            }
            ViewBag.catagory = catlist;
            try
            {
                if (objmodel.id != 0)
                {
                    ProductMaster obj = dbcontext.ProductMasters.Where(o => o.id == objmodel.id).FirstOrDefault();
                    obj.id = objmodel.id;
                    obj.catid = objmodel.catid;
                    obj.catName = itemlists.Where(c => c.id == objmodel.catid).Select(s => s.CatName).FirstOrDefault();
                    obj.ProdName = objmodel.ProdName;
                    obj.Price = objmodel.ProdPrice;
                    obj.ProdDesc = objmodel.ProdDesc;
                    dbcontext.SaveChanges();
                    TempData["status"] = "U";
                    TempData["msg"] = "Product updated Successfully.";
                    TempData.Keep();
                }
                else
                {
                    ProductMaster obj = new ProductMaster();
                    obj.catid = objmodel.catid;
                    obj.ProdName = objmodel.ProdName;
                    obj.catName = itemlists.Where(c => c.id == objmodel.catid).Select(s => s.CatName).FirstOrDefault();
                    obj.Price = objmodel.ProdPrice;
                    obj.ProdDesc = objmodel.ProdDesc;
                    dbcontext.ProductMasters.Add(obj);
                    dbcontext.SaveChanges();
                    TempData["status"] = "A";
                    TempData["msg"] = "Product added successfully";
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
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var prod = dbcontext.ProductMasters.Where(o => o.id == id).FirstOrDefault();
                dbcontext.ProductMasters.Remove(prod);
                dbcontext.SaveChanges();
                TempData["status"] = "D";
                TempData["msg"] = "Product Deleted Successfully.";
                TempData.Keep();
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