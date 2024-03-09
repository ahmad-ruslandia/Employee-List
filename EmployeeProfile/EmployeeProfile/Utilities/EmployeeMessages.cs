using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EmployeeProfile.Utilities
{
    public class EmployeeMessages : ValueChangedMessage<EmployeeMessage>
    {
        public EmployeeMessages(EmployeeMessage value) : base(value)
        {
            
        }
    }
}
