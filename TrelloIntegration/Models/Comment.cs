using RestSharp.Deserializers;
using System.Collections.Generic;
using TrelloIntegration.Library;

namespace TrelloIntegration.Models
{
    public class Comment
    {
        public string Id { get; set; }

        [DeserializeAs(Name = "data.text")]
        public string Text { get; set; }

        [DeserializeAs(Name = "data.card.id")]
        public string CardId { get; set; }

        public static List<Comment> List(string id, ITrelloApi api)
        {
            var comments = api.GetComments(id);
            return comments;
        }

        public static Comment Create(Comment comment, ITrelloApi api)
        {
            var newComment = api.CreateComment(comment);
            return newComment;
        }

    }
}