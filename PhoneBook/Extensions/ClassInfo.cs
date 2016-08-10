using System;
using System.Linq.Expressions;

namespace PhoneBook.Extensions
{
    public static class ClassInfo
    {
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            if (expression == null) return null;

            //Имя свойства или поля
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member.Name;

            //Имя метода
            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression != null)
                return methodCallExpression.Method.Name;
            
            throw new ArgumentException("Неверное выражение");
        }
    }
}