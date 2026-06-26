namespace Core.WorkflowEngine.WebAPI.Constants
{
    public static class LogConstants
    {
        public static readonly string ServiceName = "HealthCare.WorkflowEngine";
        public static string LogMessageTemplate = "Controller: {Controller}, Action: {Action}, Message: {Message}";
        public static string ValidationTemplate = "Invalid Validation Rules - Errors: {Errors}";

        public static class SuccessMessage
        {
            public const string CallingSuccess = "API request is created successfully.";
            public const string AddingDataSuccessed = "New Data is added successfully.";
            public const string UpdatingDataSuccessed = "Data is updated successfully.";
            public static string DeletingDataSuccessed = "Data is deleted successfully.";
        }

        public static class ErrorMessage
        {
            public const string CallingFail = "API request is failed.";
            public const string AddingDataFailed = "Adding new data is failed.";
            public const string UpdatingDataFailed = "Updating data is failed.";
            public static string DeletingDataFailed = "Deleting data is failed.";
        }
    }
}