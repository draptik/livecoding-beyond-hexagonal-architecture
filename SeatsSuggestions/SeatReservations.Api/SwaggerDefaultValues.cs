﻿using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SeatReservations.Api
{
    /// <summary>  
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.  
    /// </summary>  
    /// <remarks>This <see cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter"/> is only required due to bugs in the <see cref="Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator"/>.  
    /// Once they are fixed and published, this class can be removed.</remarks>  
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>  
        /// Applies the filter to the specified operation using the given context.  
        /// </summary>  
        /// <param name="operation">The operation to apply the filter to.</param>  
        /// <param name="context">The current operation filter context.</param>  
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }
            foreach (var parameter in operation.Parameters/*.OfType<NonBodyParameter>()*/)
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                var routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                //if (parameter.Default == null)
                //{
                //    parameter.Default = routeInfo.DefaultValue;
                //}

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}