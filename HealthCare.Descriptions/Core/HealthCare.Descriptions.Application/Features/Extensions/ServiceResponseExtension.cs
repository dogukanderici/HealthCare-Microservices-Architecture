using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Extensions
{
    public static class ServiceResponseExtension
    {
        public static InternalHandlerResponse<T> ToHandlerResponse<T>(this InternalServiceResponse<T> serviceResponse)
        {
            if (serviceResponse.IsSuccess)
            {
                return InternalHandlerResponse<T>.Success(serviceResponse.Data);
            }

            return InternalHandlerResponse<T>.Failure();
        }
    }
}