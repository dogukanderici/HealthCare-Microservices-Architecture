using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Constants
{
    public static class InternalCommandConstants
    {
        public const string SuccessInstanceCreating = "New instance added successfully.";
        public const string SuccessInstanceUpdating = "New instance updated successfully.";
        public const string SuccessInstanceDeleting = "New instance deleted successfully.";

        public const string ErrorInstanceCreating = "An error occured while new instance is adding.";
        public const string ErrorInstanceUpdating = "An error occured while new instance is updating.";
        public const string ErrorInstanceDeleting = "An error occured while new instance is deleting.";


        public const string SuccessWorkItemCreating = "New workitem added successfully.";
        public const string SuccessWorkItemUpdating = "New workitem updated successfully.";
        public const string SuccessWorkItemDeleting = "New workitem deleted successfully.";

        public const string ErrorWorkItemCreating = "An error occured while new workitem is adding.";
        public const string ErrorWorkItemUpdating = "An error occured while new workitem is updating.";
        public const string ErrorWorkItemDeleting = "An error occured while new workitem is deleting.";


        public const string SuccessProcessDefinitionCreating = "New process definition added successfully.";
        public const string SuccessProcessDefinitionUpdating = "New process definition updated successfully.";
        public const string SuccessProcessDefinitionDeleting = "New process definition deleted successfully.";

        public const string ErrorProcessDefinitionCreating = "An error occured while new process definition is adding.";
        public const string ErrorProcessDefinitionUpdating = "An error occured while new process definition is updating.";
        public const string ErrorProcessDefinitionDeleting = "An error occured while new process definition is deleting.";

        public const string WorkItemNotFound = "Specified workitem id is not found.";


        public const string AlreadyExistData = "Added instance is already exist.";
        public const string NotFoundData = "Updated instance is not found.";
    }
}