using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Extensions.ExpressionExtensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            try
            {
                // Yeni ortak bir x_ortak
                // Birbirinden ayrı iki filtreyi birleştirecek olan yeni x.
                var parameter = Expression.Parameter(typeof(T));

                // x1 x_ortak olarak güncellenir.
                // left.Parameters[0] x=>x.Plate==34 ifadesindeki => dolundaki değişken yani x.
                var leftVisitor = new ReplaceExpressionVisitor(left.Parameters[0], parameter);
                var leftBody = leftVisitor.Visit(left.Body);

                // x2 x_ortak olarak güncellenir.
                var rightVisitor = new ReplaceExpressionVisitor(right.Parameters[0], parameter);
                var rightBody = rightVisitor.Visit(right.Body);

                // iki ayrı filtre x_ortak ile tek bir filtrede birleştirilir.
                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftBody, rightBody), parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}