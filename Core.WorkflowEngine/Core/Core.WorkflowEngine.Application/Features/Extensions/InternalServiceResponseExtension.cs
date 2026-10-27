using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Extensions
{
    public static class InternalServiceResponseExtension
    {
        public static InternalHandlerResponse<T> ToHandlerResponse<T>(this InternalServiceResponse<T> serviceResponse, string? token = null, bool isLastPage = false)
        {
            if (serviceResponse.IsSuccess)
            {
                return InternalHandlerResponse<T>.Success(serviceResponse.Data, "Success", token, isLastPage);
            }

            return InternalHandlerResponse<T>.Failure(serviceResponse.ServiceMessage);
        }
    }
}