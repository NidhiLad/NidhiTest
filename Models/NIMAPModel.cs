using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NIMAPTest.Models
{
    public class NIMAPModel
    {
    }

    public class CatagoryViewModel
    {
        public int id { get; set; }
        public string CatName { get; set; }
        public string CatDesc { get; set; }

    }

    public class ProductViewModel
    {
        public int id { get; set; }
        public int catid { get; set; }
        public int catName { get; set; }
        public string ProdName { get; set; }
        public decimal ProdPrice { get; set; }
        public string ProdDesc { get; set; }
    }

    public class ProducuctListModel
    {
        public List<ProductMaster> ProductList { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }


    }
}