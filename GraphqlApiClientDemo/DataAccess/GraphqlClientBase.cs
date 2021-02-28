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
            var endpoint = "https://testproject1.hasura.app/v1/graphql";

            var httpClientOption = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(endpoint)
            };

            var httpClient = new HttpClient();

            var defaultHeader = httpClient.DefaultRequestHeaders;
            defaultHeader.Add("X-Hasura-Role", "tenantuser");
            defaultHeader.Add("X-Hasura-Tenant-Id", "zyxv-abcd");

           return new GraphQLHttpClient(httpClientOption, new NewtonsoftJsonSerializer(), httpClient);
        } 
    }
}
