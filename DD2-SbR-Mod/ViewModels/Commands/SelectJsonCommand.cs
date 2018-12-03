using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sbr.ViewModels.Commands
{
    public class SelectJsonCommand : ICommand
    {
        public MainViewModel ViewModel { get; set; }
        public SelectJsonCommand(MainViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ViewModel.SelectJson();
        }
    }
}
