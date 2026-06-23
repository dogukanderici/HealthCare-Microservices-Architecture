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


        public const string SuccessProcessTaskCreating = "New process task added successfully.";
        public const string SuccessProcessTaskUpdating = "New process task updated successfully.";
        public const string SuccessProcessTaskDeleting = "New process task deleted successfully.";

        public const string ErrorProcessTaskCreating = "An error occured while new process task is adding.";
        public const string ErrorProcessTaskUpdating = "An error occured while new process task is updating.";
        public const string ErrorProcessTaskDeleting = "An error occured while new process task is deleting.";


        public const string SuccessProcessTaskActionCreating = "New process task action added successfully.";
        public const string SuccessProcessTaskActionUpdating = "New process task action updated successfully.";
        public const string SuccessProcessTaskActionDeleting = "New process task action deleted successfully.";

        public const string ErrorProcessTaskActionCreating = "An error occured while new process task action is adding.";
        public const string ErrorProcessTaskActionUpdating = "An error occured while new process task action is updating.";
        public const string ErrorProcessTaskActionDeleting = "An error occured while new process task action is deleting.";

        public const string WorkItemNotFound = "Specified workitem id is not found.";
        public const string AlreadyExistData = "Added instance is already exist.";
        public const string NotFoundData = "Updated instance is not found.";
        public const string InvalidBusinessRule = "Business rule is invalid.";
    }
}