using System;
using System.Collections.Generic;
using TrelloIntegration.Models;

namespace TrelloIntegration.Library
{
    public interface ITrelloApi
    {
        Member GetMember(string fields);
        Card GetCard(string id, string fields);
        List<Card> GetCards(string boardId, string fields);
        List<Comment> GetComments(string id);
        List<Board> GetBoards(string memberId, string fields);
        Comment CreateComment(Comment comment);
    }
}
