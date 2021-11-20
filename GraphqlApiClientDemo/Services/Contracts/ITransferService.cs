using GraphqlApiClientDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphqlApiClientDemo.Services.Contracts
{
    public interface ITransferService
    {
        Task<IEnumerable<TransferDto>> GetTransfers();
    }
}
