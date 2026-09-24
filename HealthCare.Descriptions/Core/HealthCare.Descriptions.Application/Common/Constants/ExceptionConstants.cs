using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Constants
{
    public static class ExceptionConstants
    {
        public static readonly string ValidationExMessage = "VALIDATION FAILED. REQUEST:";
        public static readonly string TransactionExMessage = "Handler:";
        public static readonly string TokenDecryptExMessage = "";
        public static readonly string InvokeExMessage = "AN ERROR OCCURED WHILE EXECUTING PIPELINE! MESSAGE:";
        public static readonly string ResponseContentType = "application/json";


        public static class MiddlewareMessages
        {
            public const string ValidationMessage = "VALIDATION RULE ERROR!";
            public const string BussinessMessage = "BUSINESS RULE ERROR!";
            public const string TokenDecryptionMessage = "TOKEN DECRYPTION ERROR!";
            public const string TransactionMessage = "TRANSACTION ERROR!";
            public const string UnauthorizedMessage = "AUTHORIZATION ERROR!";
            public const string DefaultMessage = "AN ERROR OCCURED WHILE REQUEST IS PROCESSING!";
        }
        public static class MiddlewareLogMessages
        {
            public const string ValidationMessage = "";
            public const string BussinessMessage = "BUSINESS RULE ERROR OCCURED WHILE EXECUTING REQUEST! MESSAGE:";
            public const string TokenDecryptionMessage = "INVALID PAGINATION TOKEN. CHECK TOKEN IN API REQUEST BODY! MESSAGE:";
            public const string TransactionMessage = "TRANACTION ERROR. MESSAGE:";
            public const string UnauthorizedMessage = "UNAUTHORIZED! MESSAGE:";
            public const string DefaultMessage = "AN ERROR OCCURED WHILE REQUEST IS PROCESSING. MESSAGE:";
        }
    }
}
