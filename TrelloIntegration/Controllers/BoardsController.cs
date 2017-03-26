using System.Web.Mvc;
using TrelloIntegration.Models;
using TrelloIntegration.Library;

namespace TrelloIntegration.Controllers
{
    public class BoardsController : Controller
    {
        private ITrelloApi _api;
        public BoardsController(ITrelloApi api)
        {
            this._api = api;
        }

        public ActionResult Index()
        {
            // TODO wrap in try catch
            var boards = Board.List(this._api);
            return View(boards);
        }

        public ActionResult ListCards(string id)
        {
            return RedirectToAction("Index", "Cards", new { boardId = id });
        }
    }
}