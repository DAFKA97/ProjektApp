using Firma.Helpers;
using System;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class WorkspaceViewModel : BaseViewModel
    {
        public string DisplayName { get; set; }
        private BaseCommand _closeCommand;
        public event EventHandler RequestClose;
        private BaseCommand _pinCommand;
        public event EventHandler RequestPin;


        public ICommand CloseCommand
        {
            get
            {

                if (_closeCommand == null)
                    _closeCommand = new BaseCommand(OnRequestClose);
                return _closeCommand;
            }
        }

        private void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);

        }

        public ICommand PinCommand
        {
            get
            {

                if (_pinCommand == null)
                   _pinCommand = new BaseCommand(OnRequestClose);
                return _pinCommand;
            }
        }

        private void OnPinRequest()
        {
            EventHandler handler = this.RequestPin;
            if (handler != null)
                handler(this, EventArgs.Empty);

        }

    }
}
