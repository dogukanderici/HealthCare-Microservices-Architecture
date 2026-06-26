namespace Core.WorkflowEngine.WebAPI.Constants
{
    public static class APIConstants
    {
        public const string Success = "Request Successed.";
        public const string Error = "Request Failed.";
        public static string ValidationTemplate = "Invalid Validation Rules - Errors: {Errors}";

        public static class APISuccessMessage
        {
            public const string CallingSuccess = "API request is created successfully.";
            public const string AddingDataSuccessed = "New Data is added successfully.";
            public const string UpdatingDataSuccessed = "Data is updated successfully.";
            public static string DeletingDataSuccessed = "Data is deleted successfully.";
        }

        public static class APIErrorMessage
        {
            public const string CallingFail = "API request is failed.";
            public const string AddingDataFailed = "Adding new data is failed.";
            public const string UpdatingDataFailed = "Updating data is failed.";
            public static string DeletingDataFailed = "Deleting data is failed.";
        }
    }
}
