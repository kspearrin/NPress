using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NPress.Core;
using NPress.Core.Repositories;

namespace NPress.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IPostRepository g)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
