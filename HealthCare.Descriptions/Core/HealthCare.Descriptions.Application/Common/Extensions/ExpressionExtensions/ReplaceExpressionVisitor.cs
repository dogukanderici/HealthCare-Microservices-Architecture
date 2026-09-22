using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Extensions.ExpressionExtensions
{
    public class ReplaceExpressionVisitor : ExpressionVisitor
    {
        // Expression<Func<T,bool>> x1=>x1.Id == 1;
        // Expression<Func<T,bool>> x2=>x2.IsActive == true;
        // x1 ve x2 C# için birbirilerinden farklılar.
        // _oldValue x1 veya x2'yi ifade ederken _newValue x_ortak olan yeni x'i ifade eder.
        // Expression içinde gezerken eğer x1 veya x2'ye denk gelirse x_ortak ile değiştirir.
        // Id == 1 veya IsActive == true kısımlarına dokunmaz.
        // İşlem sonucunda x1=>x1.Id == 1 ifadesi x_ortak => x_ortak.Id == 1 olarak güncellenir.
        // x_ortak ise extension sınıfında oluşturulmuş x1 ve x2'de ayrı yeni bir nesnedir. x1 ve x2'yi birleştirmek için kullanılır.

        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
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