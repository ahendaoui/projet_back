using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucca.Converter.Models
{
    public class ConverterModel
    {
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }

        private double _exchangeRate;
        public double  ExchangeRate   
        {
            get { return  Math.Round(_exchangeRate, 4); }  
            set { _exchangeRate = value; }  
        }
    }
}
