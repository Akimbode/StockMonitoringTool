using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StockPriceMonitor.Services;
using StockPriceMonitor.Models;
using MainStockMonitoring.Commands;
using System.Collections.ObjectModel;

namespace MainStockMonitoring.ViewModels
{
    public class StockViewModel : INotifyPropertyChanged
    {
        #region Constructor
        public StockViewModel()
        {
            StockService = new StockService();
            LoadStockList();
            CurrentStockModel = new StockModel();

            _startCommand = new RelayCommand(StartMonitoring);
            _stopCommand = new RelayCommand(StopMonitoring);
            _deleteCommand = new RelayCommand(DeleteStock);
            _saveCommand = new RelayCommand(SaveStock);
            _searchCommand = new RelayCommand(SearchStock);
        }
        #endregion

        #region Private properties
        private ObservableCollection<StockModel> _stockModelList;
        private StockModel _currentStockModel;
        private string _message;
        #endregion

        #region Public properties

        public StockService StockService;
        public string Message
        {
            get => _message;

            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public ObservableCollection<StockModel> StockModelList
        {
            get => _stockModelList;

            set
            {
                _stockModelList = value;
                OnPropertyChanged("StockModelList");
            }
        }


        public StockModel CurrentStockModel
        {
            get => _currentStockModel;
            set
            {
                _currentStockModel = value;
                OnPropertyChanged("CurrentStockModel");
            }
        }
        #endregion

        #region Save Command
        private RelayCommand _saveCommand;
        /// <summary>
        /// Relays click events
        /// </summary>
        public RelayCommand SaveCommand
        {
            get => _saveCommand;
        }
        /// <summary>
        /// Adds Stocks to the favorite list
        /// </summary>
        public void SaveStock()
        {
            try
            {
                if(_currentStockModel != null && 
                    !string.IsNullOrWhiteSpace(_currentStockModel.StockName))
                {
                    if (StockService.SeachStockModel(_currentStockModel) != null)
                    {
                        Message = "Stock is already your favorite!";
                        return;
                    }
                    var isSaved = StockService.AddStock(_currentStockModel);

                    LoadStockList();

                    if (isSaved)
                    {
                        CurrentStockModel = new StockModel();
                        Message = "Stock is added";
                    }
                    else
                    {
                        Message = "save stock is failed or the added value is not valide!";
                    }
                }
                else
                {
                    Message = "Please enter your stock favorite!";
                }
                
            }catch(Exception ex)
            {
                Message = $"save stock is failed! Info: {ex.Message}";
            }

        }
        #endregion

        #region Search Command
        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get => _searchCommand;
        }
        public void SearchStock()
        {
            try
            {
                var findStock = StockService.SeachStockModel(_currentStockModel);

                if (findStock != null)
                {
                    CurrentStockModel = findStock;            
                }
                else
                {
                    Message = "Stock not found!";
                }
            }
            catch (Exception ex)
            {
                Message = $"search stock is failed! Info: {ex.Message}";
            }

        }
        #endregion

        #region Delete Command
        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand;
            }

            set
            {
                _deleteCommand = value;
                OnPropertyChanged("DeleteCommand");
            }
        }
        public void DeleteStock()
        {
            try
            {
                var deleteStock = StockService.SeachStockModel(_currentStockModel);

                if (deleteStock != null)
                {
                    var isDeleted = StockService.DeleteStock(deleteStock);
                    if (isDeleted)
                    {
                        Message = "Stock is deleted!";
                    }
                }
                else
                {
                    Message = "Stock is not found!";
                }
            }
            catch (Exception ex)
            {
                Message = $"Delete tock is failed! Info: {ex.Message}";
            }

        }
        #endregion

        #region StartMonitoring Command
        private RelayCommand _startCommand;

        public void StartMonitoring()
        {
            try
            {
                //TODO Start
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region Stop Command
        private RelayCommand _stopCommand;
        
        public void StopMonitoring()
        {
            try
            {
                //TODO STop
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region Private methods
        private void LoadStockList()
        {
            StockModelList = new ObservableCollection<StockModel>(StockService.GetStockModels());
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed EventHandler
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyPrice)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyPrice));
            }
        }

        #endregion
    }
}
