using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;

namespace StockPriceMonitor.Models
{
    public class StockModel : INotifyPropertyChanged
    {
        #region PRivate properties
        private string _stockName;
        private double _priceChange;
        private double _stockPrice;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new StockModel class
        /// </summary>
       
        public StockModel()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the stock`s name
        /// </summary>
        [JsonProperty(PropertyName = "Option")]
        public string StockName 
        {
            get => _stockName;
            set
            {
                _stockName = value;
                OnPropertyChanged("StockName");
            }
        }

        [JsonProperty(PropertyName = "change")]
        public double PriceChange
        {
            get => _priceChange;
            set
            {
                _priceChange = value;
                OnPropertyChanged("PriceChange");
            }
        }


        /// <summary>
        /// GEts or sets the Stock´s price
        /// </summary>
        [JsonProperty(PropertyName = "lastPrice")]
        public double StockPrice
        {
            get => _stockPrice;
            set
            {
                _stockPrice = value;
                OnPropertyChanged("StockPrice");
            }
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
