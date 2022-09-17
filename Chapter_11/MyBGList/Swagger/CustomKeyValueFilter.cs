using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MyBGList.Attributes
{
	public class CustomKeyValueFilter : ISchemaFilter
	{
		public void Apply(
			OpenApiSchema schema,
			SchemaFilterContext context)
		{
			var caProvider = context.MemberInfo 
				?? context.ParameterInfo 
				as ICustomAttributeProvider;

			var attributes = caProvider?
				.GetCustomAttributes(true)
				.OfType<CustomKeyValueAttribute>();

			if (attributes != null)
			{
				foreach (var attribute in attributes)
				{
					schema.Extensions.Add(
						attribute.Key,
						new OpenApiString(attribute.Value)
						);
				}
			}
		}
	}
}
