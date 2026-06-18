using RabbitMQ.Client.Exceptions;
using System.Reflection;

namespace Core.IdentityServer.Constants;

public static class LogConstant
{

    public static readonly string ServiceName = "HealthCare.IdentityServer";
    public static string LogMessageTemplate = "Service: {serviceName}, Controller: {controllerName}, Action: {actionName}, Message: {message}";

    // Success Messages
    public static class SuccessMessages
    {
        public const string ProcessSuccessed = "Process Successed.";
        public const string UserCreatedSuccessfully = "User Created Successfully.";
        public const string UserRegisteredSuccessfully = "User Registered Successfully.";
        public const string RoleCreatedSuccessfully = "Role Created Successfully.";
        public const string UserRoleAssignedSuccessfully = "User Role Assigned Successfully.";
    }

    // Error Messages
    public static class ErrorMessages
    {
        public const string ProcessFailed = "Process Failed.";
        public const string InvalidCredentials = "Invalid Credentials.";
        public const string UserNotFound = "User Not Found.";
        public const string UserAlreadyExists = "User Already Exists.";
        public const string RoleNotFound = "Role Not Found.";
        public const string RoleAlreadyExists = "Role Already Exists.";
        public const string UserRoleAlreadyExists = "User Role Already Exists.";
        public const string AddNewRoleFailed = "An Error Occured While New Role Save.";
    }
}