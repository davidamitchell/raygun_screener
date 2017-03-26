using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrelloIntegration.Models
;

namespace TrelloIntegration.ViewModels
{
    public class CommentsView
    {
        public List<Comment> Comments { get; set; }
        public Comment NewComment { get; set; }
        public Card Card { get; set; }
    }
}