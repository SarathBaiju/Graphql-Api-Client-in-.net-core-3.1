using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Net.Http;

namespace GraphqlApiClientDemo.DataAccess
{
    public abstract class GraphqlClientBase
    {
        public readonly GraphQLHttpClient _graphQLHttpClient ;
        public GraphqlClientBase()
        {
            if(_graphQLHttpClient == null)
            {
                _graphQLHttpClient = GetGraphQlApiClient();
            }
        }

        public GraphQLHttpClient GetGraphQlApiClient()
        {
            var endpoint = "https://userdemographql.hasura.app/v1/graphql";

            var httpClientOption = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(endpoint)
            };

           return new GraphQLHttpClient(httpClientOption, new NewtonsoftJsonSerializer(), new HttpClient());
        } 
    }
}
