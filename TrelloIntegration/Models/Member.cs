using System.Collections.Generic;
using TrelloIntegration.Library;

namespace TrelloIntegration.Models
{
    public class Member
    {
        public string Id { get; set; }
        public string username { get; set; }
        public string email { get; set; }

        public static Member Get(ITrelloApi api)
        {
            var member = api.GetMember("id,username,email");
            return member;
        }
    }
}