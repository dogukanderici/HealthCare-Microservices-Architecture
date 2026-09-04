using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Constants
{
    public static class LogConstant
    {
        public static readonly string ServiceName = "HealthCcare.Descriptions";
        public static readonly string MessageTemplate = "HandlerName: {handlerName}, Message: {message}";


        // Success Messages
        public static class SuccessMessages
        {
            public const string ProcessSuccessed = "Process Successed.";
            public const string TransactionSuccessed = "Transaction Successed.";
            public const string DataCreated = "Data Created Successfully.";
            public const string DataUpdated = "Data Updated Successfully.";
            public const string DataDeleted = "Data Deleted Successfully.";
        }

        // Error Messages
        public static class ErrorMessages
        {
            public const string ProcessFailed = "Process Failed.";
            public const string TransactionFailed = "Transaction Failed.";
            public const string DataCreate = "Data Creation Failed.";
            public const string DataUpdate = "Data Update Failed.";
            public const string DataDeletion = "Data Deletion Failed.";
            public const string DataNotFound = "Data Not Found.";
        }
    }
}