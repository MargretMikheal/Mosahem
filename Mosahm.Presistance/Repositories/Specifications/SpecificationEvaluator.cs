using Microsoft.EntityFrameworkCore;
using Mosahm.Application.Interfaces.Repositories.Specifications;
using Mosahm.Domain.Entities;

namespace Mosahm.Persistence.Repositories.Specifications
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // 1. Apply Filtering (Criteria)
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            // 2. Apply Performance Flags
            if (specification.IsAsNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (specification.IsAsSplitQuery)
            {
                query = query.AsSplitQuery();
            }

            // 3. Apply Includes (Eager Loading)
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            // 4. Apply Ordering
            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // 5. Apply Paging (Skip & Take) - Must be applied last
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query;
        }
    }
}