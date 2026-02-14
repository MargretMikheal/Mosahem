using MediatR;
using System.Reflection;

namespace Mosahem.Application.Behaviors
{
    public class StringTrimBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            TrimStrings(request);
            return await next();
        }

        private void TrimStrings(object obj)
        {
            if (obj == null) return;

            var stringProperties = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

            foreach (var prop in stringProperties)
            {
                var value = (string?)prop.GetValue(obj);
                if (!string.IsNullOrWhiteSpace(value))
                    prop.SetValue(obj, value.Trim());
            }
        }
    }

}
