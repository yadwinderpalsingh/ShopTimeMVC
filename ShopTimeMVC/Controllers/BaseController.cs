using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTimeMVC.Models;

namespace ShopTimeMVC.Controllers
{
    public class BaseController : Controller
    {
        #region Constructor

        public ShopTimeEntities shopTimeDB;

        public BaseController()
        {
            shopTimeDB = new ShopTimeEntities();
        }

        #endregion
    }
}