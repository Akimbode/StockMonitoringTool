using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainStockMonitoring.StockServices
{
    public class SchaduleTimer
    {
        private System.Threading.Timer timer;
        private void SetUpTimer(TimeSpan alertTime)
        {
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
            {
                return;//time already passed
            }
            this.timer = new System.Threading.Timer(x =>
            {
                this.SomeMethodRunsAt1600();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private void SomeMethodRunsAt1600()
        {
            //TODO 
        }
    }
}
