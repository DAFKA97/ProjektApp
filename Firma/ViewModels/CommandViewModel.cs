using System.Windows.Input;

namespace Firma.ViewModels
{
    public class CommandViewModel : BaseViewModel
    {

        public string DisplayName { get; set; }

        public ICommand Command { get; set; }

        public CommandViewModel(string displayName, ICommand command) 
        {
        
            DisplayName = displayName; 
            Command = command;
        }
    }
}
