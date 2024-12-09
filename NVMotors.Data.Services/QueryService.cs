using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data
{
    public class QueryService : IQueryService
    {
        private readonly NVMotorsDbContext context;

        public QueryService(NVMotorsDbContext _context)
        {
            context = _context; 
        }

        public async Task CreateQueryAsync(MakeQueryViewModel queryModel, Guid id)
        {
            if (queryModel == null) 
            {
                throw new ArgumentNullException("Invalid data!");
            }
            if (id == Guid.Empty) 
            {
                throw new ArgumentException("Invalid user!");
            }

            var adExists = await context.Ads.AnyAsync(a => a.Id == queryModel.AdId && a.IsApproved);
            if (!adExists)
            {
                throw new InvalidOperationException($"Ad does not exist or is not approved.");
            }
            var query = new Query()
            {
                PhoneNumber = queryModel.PhoneNumber,
                Description = queryModel.Description,
                AdId = queryModel.AdId,
                RequesterId = id,
                DateRequested = DateTime.Now,
            };
            await context.Queries.AddAsync(query);
            await context.SaveChangesAsync();
        }

        public async Task<List<QueryIndexViweModel>> IndexGetMyRequestsAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid user!");
            }
            var query = context.Queries.Where(q => q.RequesterId == id)
                 .Select(q => new QueryIndexViweModel
                 {
                     AdId = q.AdId,
                     Description = q.Description,
                     Make = q.Ad.Motor.Make,
                     Model = q.Ad.Motor.Model,
                     Date = q.DateRequested.ToString("dd/MM/yyyy"),
                 }).ToList();
            return query;
        }

        public async Task<List<QueriesReceivedViewModel>> IndexGetReceivedRequestsAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid user!");
            }
            var query = context.Queries.Where(q => q.Ad.Motor.SellerId == id)
                 .Select(q => new QueriesReceivedViewModel
                 {
                     AdId = q.AdId,
                     Description = q.Description,
                     Make = q.Ad.Motor.Make,
                     Model = q.Ad.Motor.Model,
                     DateRequested = q.DateRequested.ToString("dd/MM/yyyy"),
                     PhoneNumber = q.PhoneNumber,
                     Email = q.Requester.Email!,
                     FullName = $"{q.Requester.FirstName} {q.Requester.LastName}"
                 }).ToList();
            return query;
        }
    }
}
