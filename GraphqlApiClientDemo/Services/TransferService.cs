using GraphQL.Client.Http;
using GraphqlApiClientDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using System;
using System.Net.Http;
using GraphqlApiClientDemo.DataAccess;
using System.Linq;
using GraphqlApiClientDemo.Services.Contracts;

namespace GraphqlApiClientDemo.Services
{
    public class TransferService : GraphqlClientBase, ITransferService
    {
        public async Task<IEnumerable<TransferDto>> GetTransfers()
        {
            var query = @"query MyQuery {
                                          Transfer {
                                            Id
                                            destinationTenantId
                                            originTenantId
                                            originalManuscriptId
                                            title
                                          }
                                        }";

            var request = new GraphQLRequest(query);
            

            var response = await _graphQLHttpClient.SendQueryAsync<TransferQueryResponse>(request);
            if (response.Errors != null && response.Errors.Any())
            {
                throw new Exception(string.Join(", ",response.Errors.Select(s => s.Message).ToList()));
            }

            return response.Data.Transfer;
        }
    }
    public class TransferQueryResponse
    {
        public IEnumerable<TransferDto> Transfer { get; set; }
    }
}
