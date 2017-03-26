using System.Collections.Generic;
using RestSharp;
using RestSharp.Deserializers;
using TrelloIntegration.Library;

namespace TrelloIntegration.Models
{
    public class Card
    {

        public string Id { get; set; }

        [DeserializeAs(Name = "idBoard")]
        public string BoardId { get; set; }

        [DeserializeAs(Name = "desc")]
        public string Description { get; set; }

        public string Name { get; set; }

        public static Card Get(string id, ITrelloApi api)
        {
            var card = api.GetCard(id, "id,name,desc,idBoard");
            return card;
        }

        public static List<Card> List(string boardId, ITrelloApi api)
        {
            var cards = api.GetCards(boardId, "id,name,desc,idBoard");
            return cards;
        }

    }
}