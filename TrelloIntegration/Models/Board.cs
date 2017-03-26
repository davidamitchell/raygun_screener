using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrelloIntegration.Library;

namespace TrelloIntegration.Models
{
    public class Board
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static List<Board> List(ITrelloApi api)
        {
            var member = Member.Get(api);
            var boards = api.GetBoards(member.Id, "id,name");
            return boards;
        }
    }
}