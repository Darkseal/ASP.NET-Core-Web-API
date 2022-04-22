using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyBGList.Attributes
{
    public class SortOrderFilter : IParameterFilter
    {
        public void Apply(
            OpenApiParameter parameter,
            ParameterFilterContext context)
        {
            var attributes = context.ParameterInfo
                .GetCustomAttributes(true)
                .OfType<SortOrderValidatorAttribute>();

            foreach (var attribute in attributes)
            {
                parameter.Schema.Extensions.Add(
                    "pattern",
                    new OpenApiString(string.Join("|", attribute.AllowedValues))
                    );
            }
        }
    }
}