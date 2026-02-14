using mosahem.Application.Interfaces.Repositories.Specifications;
using System.Linq.Expressions;

namespace Mosahem.Application.Interfaces.Repositories.Specifications
{
    public sealed class Specification<T> : BaseSpecification<T> where T : class
    {
        public Specification() : base()
        {
        }

        public Specification(Expression<Func<T, bool>> criteria) : base(criteria)
        {
        }

        public Specification<T> Include(Expression<Func<T, object>> includeExpression)
        {
            AddInclude(includeExpression);
            return this;
        }

        public Specification<T> Include(string includeString)
        {
            AddInclude(includeString);
            return this;
        }

        public Specification<T> OrderByAsc(Expression<Func<T, object>> orderByExpression)
        {
            ApplyOrderBy(orderByExpression);
            return this;
        }

        public Specification<T> OrderByDesc(Expression<Func<T, object>> orderByDescendingExpression)
        {
            ApplyOrderByDescending(orderByDescendingExpression);
            return this;
        }

        public Specification<T> NoTracking()
        {
            ApplyNoTracking();
            return this;
        }

        public Specification<T> AsSplitQuery()
        {
            ApplyAsSplitQuery();
            return this;
        }

        public Specification<T> Page(int skip, int take)
        {
            ApplyPaging(skip, take);
            return this;
        }
    }
}
