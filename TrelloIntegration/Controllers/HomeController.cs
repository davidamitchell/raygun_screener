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
            //var token = new Token { AuthToken = "c6810c83faf3b5d25cad59a31db0a1571bb1afaac11773826ecb5af0bc335009" };
            return View(new Token());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnterToken(Token token)
        {
            // TODO validate token is not null and not an empty string
            SessionFacade.StoreAuthToken(token.AuthToken);
            return RedirectToAction("Index", "Boards");
        }
    }
}