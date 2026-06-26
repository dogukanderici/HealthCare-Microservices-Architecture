using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using Core.WorkflowEngine.WebAPI.Helpers.ValidationHelpers;

namespace Core.WorkflowEngine.WebAPI.Configurations
{
    public static class HelperConfigurations
    {
        public static IServiceCollection AddHelperServiceConfiguration(this IServiceCollection service)
        {
            service.AddTransient(typeof(IValidationHelper<>), typeof(ValidationHelper<>));
            service.AddTransient(typeof(IControllerReponseHelper<>), typeof(ControllerResponseHelper<>));

            return service;
        }
    }
}