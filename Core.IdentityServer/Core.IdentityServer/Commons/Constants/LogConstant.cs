using RabbitMQ.Client.Exceptions;
using System.Reflection;

namespace Core.IdentityServer.Commons.Constants;

public static class LogConstant
{

    public static readonly string ServiceName = "HealthCare.IdentityServer";
    public static string LogMessageTemplate = "Service: {serviceName}, Controller: {controllerName}, Action: {actionName}, Message: {message}";

    // Success Messages
    public static class SuccessMessages
    {
        public const string ProcessSuccessed = "Process Successed.";
        public const string UserCreated = "User Created Successfully.";
        public const string UserRegistered = "User Registered Successfully.";
        public const string RoleCreated = "Role Created Successfully.";
        public const string RoleUpdated = "Role Updated Successfully.";
        public const string RoleDeleted = "Role Deleted Successfully.";
        public const string UserRoleAssigned = "User Role Assigned Successfully.";
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
        public const string UpdateRoleFailed = "An Error Occured While Role Update.";
        public const string DeleteRoleFailed = "An Error Occured While Role Delete.";
    }
}