using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Commons.Extensions.ExpressionExtensions
{
    public class ReplaceExtensionVisitor : ExpressionVisitor
    {
        // Expression<Func<T,bool>> x1 => x1.Id == 1;
        // Expression<Func<T,bool>> x2 => x2.IsActive == true;
        // x1 ve x2 C# için farklı nesnelerdir.
        // _oldValue x1 veya x2'yi temsil eder.
        // _newValue yeni ortak olan x'i temsil eder.
        // Expression içinde gezerken x1 veya x2'ye denk gelirse x_ortak ile değiştirilir.
        // x.Id == 1 veya x.IsActive=true kısımlarına dokunmaz.
        // İşlem sonucunda x1 = x=>x.Id == 1 ifadesi x_ortak => x_ortak.Id == 1 olarak güncellenir.

        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExtensionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        [return: NotNullIfNotNull("node")]
        public override Expression? Visit(Expression? node)
        {
            if (node == _oldValue)
                return _newValue;

            return base.Visit(node);
        }
    }
}