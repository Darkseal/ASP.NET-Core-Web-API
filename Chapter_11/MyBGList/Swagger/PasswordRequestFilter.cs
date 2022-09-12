using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyBGList.Swagger
{
    internal class PasswordRequestFilter : IRequestBodyFilter
    {
        public void Apply(
            OpenApiRequestBody requestBody,
            RequestBodyFilterContext context)
        {
            var fieldName = "password";

            if (context.BodyParameterDescription.Name
                .Equals(fieldName,
                    StringComparison.OrdinalIgnoreCase)
                || context.BodyParameterDescription.Type
                .GetProperties().Any(p => p.Name
                    .Equals(fieldName,
                        StringComparison.OrdinalIgnoreCase)))
            {
                requestBody.Description =
                    "IMPORTANT: be sure to always use a strong password " +
                    "and store it in a secure location!";
            }
        }
    }
}
