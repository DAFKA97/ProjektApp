using Firma.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;

namespace Firma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Workspaces

        private ObservableCollection<WorkspaceViewModel> _workspaces;

        public ObservableCollection<WorkspaceViewModel> Workspaces 
        { 
            get 
            {
            
                if( _workspaces == null )
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _workspaces;
            } 
        }

        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += onWorkspaceRequestClose;

            if (e.OldItems !=null && e.OldItems.Count != 0)
                foreach(WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= onWorkspaceRequestClose; 
        }

        private void onWorkspaceRequestClose(object sender,EventArgs e)
        {

            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            this.Workspaces.Remove(workspace);

        }
        #endregion

        #region ListCommand
        private ReadOnlyCollection<CommandViewModel> _commands;

        public ReadOnlyCollection<CommandViewModel> Commands
        {

            get
            {
                if (_commands == null)
                {
                    var commands = InitListCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(commands); 
                }
                return _commands;
            }
        }
        private List<CommandViewModel> InitListCommands()
        {
            BaseCommand baseCommand = new BaseCommand(() => ShowView(new InvoiceAddItemViewModel()));
            return new List<CommandViewModel>
            {

                new CommandViewModel("Dodaj Kontahenta", new BaseCommand(() => ShowView(new EmployeeViewModel()))),
                new CommandViewModel("Kontrahenci", new BaseCommand(() => ShowView(new ContractorViewModel()))),
                new CommandViewModel("Dodaj fakturę", new BaseCommand(() => ShowView(new InvoiceViewModel()))),
                new CommandViewModel("Lista faktur", new BaseCommand(() => ShowView(new InvoiceListViewModel()))),
                new CommandViewModel("Dodaj towar na fakturze", new BaseCommand(() => ShowView(new InvoiceAddItemViewModel())))
            };
        }
        #endregion
        #region Helpers

        private void ShowView<T>(T workspaceViewModel)
        {

            var workspace = Workspaces.FirstOrDefault(vm => vm.GetType()  == workspaceViewModel.GetType());
            
            if (workspace == null)
            {

                workspace = workspaceViewModel as WorkspaceViewModel;
                Workspaces.Add(workspace);
            }

            SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {

            Debug.Assert(this.Workspaces.Contains(workspace));
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion
    }
}
