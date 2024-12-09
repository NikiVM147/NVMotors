using NVMotors.Web.ViewModels.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data.Interfaces
{
    public interface IQueryService
    {
        Task<List<QueryIndexViweModel>> IndexGetMyRequestsAsync(Guid id);
        Task<List<QueriesReceivedViewModel>> IndexGetReceivedRequestsAsync(Guid id);
        Task CreateQueryAsync(MakeQueryViewModel queryModel, Guid id);
    }
}
