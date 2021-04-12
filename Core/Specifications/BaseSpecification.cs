using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria 
        {
            get;
        }

        public List<Expression<Func<T, object>>> Includes 
        {
            get;
        } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;}

        public Expression<Func<T, object>> OrderByDescending {get; private set;}
        public BaseSpecification(int take, int skip, bool isPagingEnabled) 
        {
            this.Take = take;
                this.Skip = skip;
                this.IsPagingEnabled = isPagingEnabled;
               
        }
                public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool IsPagingEnabled {get; private set;}

        protected void AddInclude(Expression<Func<T,Object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }

        protected void AddOrderBy(Expression<Func<T,Object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T,Object>> OrderByDescExpression)
        {
            OrderByDescending = OrderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled =true;
        }
    }
}