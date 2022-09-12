using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyBGList.Swagger
{
    internal class PasswordParameterFilter : IParameterFilter
    {
        public void Apply(
            OpenApiParameter parameter, 
            ParameterFilterContext context)
        {
            if (context.ParameterInfo.ParameterType
                .GetProperties().Any(p => p.Name
                    .Equals("Password",
                        StringComparison.OrdinalIgnoreCase)))
            {
                parameter.Description =
                    "IMPORTANT: be sure to always use a strong password" +
                    "and store it in a secure location!";
            }
        }
    }
}
