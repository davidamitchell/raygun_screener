using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using TrelloIntegration.Models;

namespace TrelloIntegration.Library
{
    public class TrelloApiRestSharp : ITrelloApi
    {

        private Token _token { get; set; }
        private JsonDeserializer _deserialiser{ get; set; }
        private RestClient _client { get; set; }
        private string _version { get; set; }

        /// <summary>
        /// Instantiates a new TrellowApiRestSharp.
        /// </summary>
        /// <param name="AuthToken">A string which is a users authentication token, this is used to access ones Trello boards, cards, etc.</param>
        /// <returns>a new TrellowApiRestSharp instance.</returns>
        public TrelloApiRestSharp(string AuthToken)
        {
            this._token = new Token { AuthToken = AuthToken };
            this._version = TrelloApiConfig.ApiVersion();
            this._client = new RestClient(TrelloApiConfig.BaseApiUri());
            //this.Deserialiser = new JsonDeserializer();
        }

        /// <summary>
        /// A helper method to build a Post request. Note the returned request may need to have AddUrlSegment called on it if a url segment is passed in as part of the resource param (/xyz/{id}/).
        /// </summary>
        /// <param name="resource">A string which is the location of the resource to post to.</param>
        /// <returns>A RestRequest instance.</returns>
        private RestRequest BuildPostRequest(string resource)
        {
            var req = new RestRequest(this._version + resource, Method.POST);
            req.AddParameter("token", this._token.AuthToken);
            req.AddParameter("key", TrelloApiConfig.DevKey());
            return req;
        }

        /// <summary>
        /// A helper method to build a Get request. Note the returned request may need to have AddUrlSegment called on it if a url segment is passed in as part of the resource param (/xyz/{id}/).
        /// </summary>
        /// <param name="resource">A string which is the location of the resource to post to.</param>
        /// <param name="fields">A string of comma separeted fields to return, it is ignored if null or an empty string.</param>
        /// <param name="filter">A string filter to use during the api call, it is ignored if null or an empty string.</param>
        /// <returns>A RestRequest instance.</returns>
        private RestRequest BuildGetRequest(string resource, string fields = null, string filter = null)
        {
            var req = new RestRequest(this._version + resource, Method.GET);
            req.AddParameter("token", this._token.AuthToken);
            req.AddParameter("key", TrelloApiConfig.DevKey());

            if (!String.IsNullOrWhiteSpace(fields))
            {
                req.AddParameter("fields", fields);
            }
            if (!String.IsNullOrWhiteSpace(filter))
            {
                req.AddParameter("filter", filter);
            }
            return req;
        }

        /// <summary>
        /// A wrapper around the Trello api to get a representation of a Member.
        /// </summary>
        /// <param name="fields">A string of comma separeted fields to get.</param>
        /// <returns>A Member instance, it will be empty if none is found.</returns>
        public Member GetMember(string fields)
        {
            var req = BuildGetRequest("/tokens/{token}/member/", fields);
            req.AddUrlSegment("token", this._token.AuthToken);

            //TODO check the status of the response
            var res = this._client.Execute<Member>(req);
            return res.Data != null ? res.Data : new Member();
        }

        /// <summary>
        /// A wrapper around the Trello api to get a representation of a Card.
        /// </summary>
        /// <param name="fields">A string of comma separeted fields to get.</param>
        /// <returns>A Card instance, it will be empty if none is found.</returns>
        public Card GetCard(string id, string fields)
        {
            var req = BuildGetRequest("/cards/{id}", fields);
            req.AddUrlSegment("id", id);

            //TODO check the status of the response
            var res = this._client.Execute<Card>(req);
            return res.Data != null ? res.Data : new Card();
        }

        /// <summary>
        /// A wrapper around the Trello api to get a list of Cards.
        /// </summary>
        /// <param name="boardId">The id of the board for which to get all the cards of.</param>
        /// <param name="fields">A string of comma separeted fields to get.</param>
        /// <returns>A List of Cards or an empty list if none are found.</returns>
        public List<Card> GetCards(string boardId, string fields)
        {
            var req = BuildGetRequest("/boards/{boardId}/cards", fields);
            req.AddUrlSegment("boardId", boardId);

            //TODO check the status of the response
            var res = this._client.Execute<List<Card>>(req);
            return res.Data != null ? res.Data : new List<Card>();
        }

        /// <summary>
        /// A wrapper around the Trello api to get a list of Comments.
        /// </summary>
        /// <param name="cardId">The id of the card for which to get all the comments of.</param>
        /// <returns>A List of Comment or an empty list if none are found.</returns>
        public List<Comment> GetComments(string cardId)
        {
            var req = BuildGetRequest("/cards/{id}/actions/", null, "commentCard");
            req.AddUrlSegment("id", cardId);

            //TODO check the status of the response
            var res = this._client.Execute<List<Comment>>(req);
            return res.Data != null ? res.Data : new List<Comment>();
        }

        /// <summary>
        /// A wrapper around the Trello api to get a list of Boards.
        /// </summary>
        /// <param name="memberId">The id of the Member for which to get all the boards of.</param>
        /// <param name="fields">A string of comma separeted fields to get.</param>
        /// <returns>A List of Cards or an empty list if none are found.</returns>
        public List<Board> GetBoards(string memberId, string fields)
        {
            var req = BuildGetRequest("/members/{memberId}/boards");
            req.AddUrlSegment("memberId", memberId);

            //TODO check the status of the response
            var res = this._client.Execute<List<Board>>(req);
            return res.Data != null ? res.Data : new List<Board>();
        }

        /// <summary>
        /// A wrapper around the Trello api to create a new Comment.
        /// </summary>
        /// <param name="comment">A Comment instance.</param>
        /// <returns>The newly created Comment.</returns>
        public Comment CreateComment(Comment comment)
        {
            var req = BuildPostRequest("/cards/{id}/actions/comments");
            req.AddParameter("text", comment.Text);
            req.AddUrlSegment("id", comment.CardId);

            //TODO check the status of the response
            var res = this._client.Execute<Comment>(req);
            return res.Data;
        }

    }

    public static class TrelloApiConfig
    {
        public static string BaseApiUri()
        {
            return "https://trello.com/";
        }
        public static string ApiVersion()
        {
            return "1";
        }
        public static string Scope()
        {
            return "read,write";
        }
        public static string ResponseType()
        {
            return "token";
        }
        public static string DevKey()
        {
            return Environment.GetEnvironmentVariable("TRELLO_DEV_KEY", EnvironmentVariableTarget.Machine);
        }
        public static string AppName()
        {
            return Environment.GetEnvironmentVariable("TRELLO_APP_NAME", EnvironmentVariableTarget.Machine);
        }
        public static string AuthoriseUri()
        {
            // https://trello.com/1/authorize?expiration=never&response_type=token&scope=read,write&name=TrelloIntegrationMvc&key=xyz
            return BaseApiUri() + ApiVersion() + "/authorize?expiration=never&response_type=" + ResponseType() + "&scope=" + Scope() + "&name=" + AppName() + "&key=" + DevKey();
        }

        public static bool ConfigValid()
        {
            var EnvValid = true;
            if (String.IsNullOrWhiteSpace(AppName()) || String.IsNullOrWhiteSpace(DevKey()))
            {
                EnvValid = false;
            }
            return EnvValid;
        }
    }
}
