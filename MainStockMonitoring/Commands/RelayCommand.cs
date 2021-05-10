using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainStockMonitoring.Commands
{
    public class RelayCommand : ICommand
    {
        #region Constructor
        public RelayCommand(Action action)
        {
            _actionRun = action;
        }
        #endregion
        #region Public properties
        private Action _actionRun;
        #endregion

        #region Public properties
        public event EventHandler CanExecuteChanged; 
        #endregion

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _actionRun();
        }
    }
}
