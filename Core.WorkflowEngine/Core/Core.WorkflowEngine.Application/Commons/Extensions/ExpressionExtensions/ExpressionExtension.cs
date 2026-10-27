using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Commons.Extensions.ExpressionExtensions
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            // left => Birleştirme işleminden önce hazırlanan filtre.
            // right => Hazır filtreye eklenmek istenen yeni kriter.

            try
            {
                // x'leri birleştirecek yeni x_ortak.
                var parameter = Expression.Parameter(typeof(T));

                // x1 => x_ortak olarak güncellenir.
                var leftVisitor = new ReplaceExtensionVisitor(left.Parameters[0], parameter);
                var leftBody = leftVisitor.Visit(left.Body);

                // x2 => x_ortak olarak güncellenir.
                var rightVisitor = new ReplaceExtensionVisitor(right.Parameters[0], parameter);
                var rightBody = rightVisitor.Visit(right.Body);

                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftBody, rightBody), parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}