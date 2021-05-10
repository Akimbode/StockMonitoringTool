using StockPriceMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MainStockMonitoring.StockServices;

namespace StockPriceMonitor.Services
{
    public class StockService
    {
        #region Private Properties
        private static ObservableCollection<StockModel> _stockList;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new StockService class
        /// </summary>
        public StockService()
        {
            _stockList = new ObservableCollection<StockModel>()
            {
                //TEST DATA
                //new StockModel{ StockName = "APPL", StockPrice = 21},
               // new StockModel{ StockName = "AOL", StockPrice = 10}
            };
        }
        #endregion

        #region Public Methods
        public ObservableCollection<StockModel> GetStockModels()
        {
            return _stockList;
        }

        /// <summary>
        /// Add a new Stock´s favorite
        /// </summary>
        /// <param name="stockModel"></param>
        /// <returns></returns>
        public bool AddStock(StockModel stockModel)
        {
            bool result = false;
            try
            {
                var request = new StockHttpClient();
                var response = request.GetStockInfo(stockModel.StockName);
                if(response != null)
                {
                    _stockList.Add(response);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Update an existing Stock´s favorite
        /// </summary>
        /// <param name="stockModel"></param>
        /// <returns></returns>
        public bool UpdateStock(StockModel stockModel)
        {
            bool result = false;
            try
            {
                for(int i = 0; i < _stockList.Count; i++)
                {
                    if(_stockList[i].StockName == stockModel.StockName)
                    {
                        _stockList[i].StockPrice = stockModel.StockPrice;
                        result = true;
                        break;
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Delete an existing Stock´s favorite
        /// </summary>
        /// <param name="stockModel"></param>
        /// <returns></returns>
        public bool DeleteStock(StockModel stockModel)
        {
            bool result = false;
            try
            {
                for (int i = 0; i < _stockList.Count; i++)
                {
                    if (_stockList[i].StockName == stockModel.StockName)
                    {
                        _stockList.RemoveAt(i);
                        result = true;
                        break;
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }


        public StockModel SeachStockModel(StockModel stockModel)
        {
            return _stockList.FirstOrDefault(x => x.StockName == stockModel.StockName);
        }
        #endregion
    }
}
