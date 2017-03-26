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
        
        public TrelloApiRestSharp(string AuthToken)
        {
            this._token = new Token { AuthToken = AuthToken };
            this._version = TrelloApiConfig.ApiVersion();
            this._client = new RestClient(TrelloApiConfig.BaseApiUri());
            //this.Deserialiser = new JsonDeserializer();
        }

        private RestRequest BuildPostRequest(string resource)
        {
            var req = new RestRequest(this._version + resource, Method.POST);
            req.AddParameter("token", this._token.AuthToken);
            req.AddParameter("key", TrelloApiConfig.DevKey());
            return req;
        }

        private RestRequest BuildGetRequest(string resource, string fields = null, string filter = null)
        {
            var req = new RestRequest(this._version + resource, Method.GET);
            req.AddParameter("token", this._token.AuthToken);
            req.AddParameter("key", TrelloApiConfig.DevKey());

            if (fields != null && !String.IsNullOrWhiteSpace(fields))
            {
                req.AddParameter("fields", fields);
            }
            if (filter != null && !String.IsNullOrWhiteSpace(filter))
            {
                req.AddParameter("filter", filter);
            }
            return req;
        }

        public Member GetMember(string fields)
        {
            var req = BuildGetRequest("/tokens/{token}/member/", fields);
            req.AddUrlSegment("token", this._token.AuthToken);

            //TODO check the status of the response
            var res = this._client.Execute<Member>(req);
            return res.Data != null ? res.Data : new Member();
        }

        public Card GetCard(string id, string fields)
        {
            var req = BuildGetRequest("/cards/{id}", fields);
            req.AddUrlSegment("id", id);

            //TODO check the status of the response
            var res = this._client.Execute<Card>(req);
            return res.Data != null ? res.Data : new Card();
        }

        public List<Card> GetCards(string boardId, string fields)
        {
            var req = BuildGetRequest("/boards/{boardId}/cards", fields);
            req.AddUrlSegment("boardId", boardId);

            //TODO check the status of the response
            var res = this._client.Execute<List<Card>>(req);
            return res.Data != null ? res.Data : new List<Card>();
        }

        public List<Comment> GetComments(string id)
        {
            var req = BuildGetRequest("/cards/{id}/actions/", null, "commentCard");
            req.AddUrlSegment("id", id);

            //TODO check the status of the response
            var res = this._client.Execute<List<Comment>>(req);
            return res.Data != null ? res.Data : new List<Comment>();
        }

        public List<Board> GetBoards(string memberId, string fields)
        {
            var req = BuildGetRequest("/members/{memberId}/boards");
            req.AddUrlSegment("memberId", memberId);

            //TODO check the status of the response
            var res = this._client.Execute<List<Board>>(req);
            return res.Data != null ? res.Data : new List<Board>();
        }

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
            // https://trello.com/1/authorize?expiration=never&scope=read,write&response_type=token&name=TrelloIntegrationMvc&key=273686af81c20054647f4a9915be4447
            return BaseApiUri() + ApiVersion() + "/authorize?expiration=never&response_type=" + ResponseType() + "&name=" + AppName() + "&key=" + DevKey();
        }

        public static bool ConfigValid()
        {
            var EnvValid = true;
            if (AppName() == null || DevKey() == null)
            {
                EnvValid = false;
            }
            return EnvValid;
        }
    }
}
