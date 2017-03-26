using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrelloIntegration.Models;
using TrelloIntegration.Library;

namespace TrelloIntegration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new Token());
        }

        [HttpPost]
        public ActionResult EnterToken(Token token)
        {
            SessionFacade.StoreAuthToken(token.AuthToken);
            return RedirectToAction("Index", "Boards");
        }
    }
}