using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MP.CrossCutting.Utils.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TResult> LeftJoinn<TOuter, TInner, TKey, TResult>(
            this IQueryable<TOuter> outer,
            IQueryable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {
            return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outerObj, innerObjs) =>
                new { outerObj, innerObjs = innerObjs.DefaultIfEmpty() })
                .SelectMany(
                    x => x.innerObjs.Select(innerObj => resultSelector.Compile().Invoke(x.outerObj, innerObj))
                );
        }
    }

}
