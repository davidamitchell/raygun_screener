using System.Web.Mvc;
using TrelloIntegration.Models;
using TrelloIntegration.ViewModels;
using TrelloIntegration.Library;
using System.Collections.Generic;

namespace TrelloIntegration.Controllers
{
    public class CardsController : Controller
    {
        private ITrelloApi _api;
        public CardsController(ITrelloApi api)
        {
            this._api = api;
        }

        public ActionResult Index(string boardId)
        {
            // TODO wrap in try catch
            var cards = Card.List(boardId, this._api);
            return View(cards);
        }
        public ActionResult Detail(string id)
        {
            // TODO wrap in try catch
            var comments = Comment.List(id, this._api);
            var card = Card.Get(id, this._api);
            if(card.Id == null)
            {
                return View("Error");
            }
            var cv = new CommentsView { Comments = comments, NewComment = new Models.Comment(), Card = card };
            return View(cv);
        }

        [HttpPost]
        public ActionResult CreateComment(CommentsView m)
        {
            var cardId = m.Card.Id;
            var comment = new Comment { Text = m.NewComment.Text, CardId = cardId };

            // TODO wrap in try catch
            var newComment = Comment.Create(comment, this._api);
            if (newComment == null)
            {
                return View("Error");
            }
            return RedirectToAction("Detail", new { id = cardId });
        }

    }
}