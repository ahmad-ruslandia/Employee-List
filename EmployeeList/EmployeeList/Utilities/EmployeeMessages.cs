using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EmployeeList.Utilities
{
    public class EmployeeMessages : ValueChangedMessage<EmployeeMessage>
    {
        public EmployeeMessages(EmployeeMessage value) : base(value)
        {
            
        }
    }
}
