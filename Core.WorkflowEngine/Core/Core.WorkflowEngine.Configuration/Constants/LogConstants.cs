using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Configuration.Constants
{
    public static class LogConstants
    {
        public static readonly string ServiceName = "HealthCare.WorkflowEngine";
        public static string LogMessageTemplate = "HandlerName: {handlerName}, Message: {message}";

        // Success Messages
        public static class SuccessMessages
        {
            public const string ProcessSuccessed = "Process Successed.";
            public const string DataCreatedSuccessfully = "Data Created Successfully.";
            public const string DataUpdatedSuccessfully = "Data Updated Successfully.";
            public const string DataDeletedSuccessfully = "Data Deleted Successfully.";
        }

        // Error Messages
        public static class ErrorMessages
        {
            public const string ProcessFailed = "Process Failed.";
            public const string DataCreationFailed = "Data Creation Failed.";
            public const string DataUpdateFailed = "Data Update Failed.";
            public const string DataDeletionFailed = "Data Deletion Failed.";
            public const string DataNotFound = "Data Not Found.";
        }
    }
}