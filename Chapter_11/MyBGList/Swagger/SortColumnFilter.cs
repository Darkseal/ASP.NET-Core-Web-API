using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MyBGList.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyBGList.Swagger
{
    public class SortColumnFilter : IParameterFilter
    {
        public void Apply(
            OpenApiParameter parameter,
            ParameterFilterContext context)
        {
            var attributes = context.ParameterInfo?
                .GetCustomAttributes(true)
                .Union(
                    context.ParameterInfo.ParameterType.GetProperties()
                    .Where(p => p.Name == parameter.Name)
                    .SelectMany(p => p.GetCustomAttributes(true))
                    )
                .OfType<SortColumnValidatorAttribute>();

            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    var pattern = attribute.EntityType
                        .GetProperties()
                        .Select(p => p.Name);
                    parameter.Schema.Extensions.Add(
                        "pattern",
                        new OpenApiString(string.Join("|", pattern.Select(v => $"^{v}$")))
                        );
                }
            }
        }
    }
}
