﻿using GraphQL.Client.Http;
using GraphqlApiClientDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using System;
using System.Net.Http;
using GraphqlApiClientDemo.DataAccess;

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
            return response.Data.Transfer;
        }
    }
    public class TransferQueryResponse
    {
        public IEnumerable<TransferDto> Transfer { get; set; }
    }
}
