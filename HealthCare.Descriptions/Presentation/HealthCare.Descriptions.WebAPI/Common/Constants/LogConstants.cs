namespace HealthCare.Descriptions.WebAPI.Common.Constants
{
    public static class LogConstants
    {
        public static readonly string ServiceName = "HealthCare.Descriptions";
        public static string LogMessageTemplate = "Controller: {Controller}, Action: {Action}";
        public static string ValidationTemplate = "Invalid Validation Rules - Errors: {Errors}";

        public static class SuccessMessage
        {
            public const string CallingRequest = "API request is created successfully.";
            public const string AddingData = "New Data is added successfully.";
            public const string UpdatingData = "Data is updated successfully.";
            public static string DeletingData = "Data is deleted successfully.";
        }

        public static class ErrorMessage
        {
            public const string CallingRequest = "API request is failed.";
            public const string AddingData = "Adding new data is failed.";
            public const string UpdatingData = "Updating data is failed.";
            public static string DeletingData = "Deleting data is failed.";
        }
    }
}