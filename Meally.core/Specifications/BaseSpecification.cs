using Meally.core.Entities.Identity;

namespace Meally.core.Specifications;

public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDes { get; set; }

    public BaseSpecification() { }

    public BaseSpecification(Expression<Func<T, bool>> criteriaExpression) => Criteria = criteriaExpression;

    public void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;

    public void AddOrderByDes(Expression<Func<T, object>> orderByDesExpression) => OrderByDes = orderByDesExpression;

}

