using Lucca.Converter.Models;
using System;
using System.Globalization;


namespace Lucca.Converter
{
    public class CurrencyConverter
    {


        /// <summary>
        /// Get The shortest exchange Rate between two currencies
        /// </summary>
        public static string GetTheShortestrateExchangeRate(string userChoice , List<string> listCurrency)
        {
            
            if (string.IsNullOrEmpty(userChoice))
                return "you must write your choice !";
            if(listCurrency == null)
                return "please fill in your currency exchange list !";
           
            var test = userChoice.Split(";");
            if(test.Length != 3)
            return "Your input line is invalid. It should contain source currency, amount and destination currency separated by ';' character. Like: D1;M;D2";

            var result = "";
            var deviseDepart = test[0].ToUpper();
            var deviseCible=test[2].ToUpper();
            var amount =  Convert.ToDouble(test[1], CultureInfo.InvariantCulture);
            if (!verifyNumber(amount))
                return "Amount type is invalid";

        var userListCurrency = new List<ConverterModel>();
            var userListConvertCurrency = new List<ConverterModel>();
            var BaseCurrency = new List<ConverterModel>();
            var tragetCurrecny = new List<ConverterModel>();
            try
            {
               foreach(var currency in listCurrency)
                {
                    var currencyList = currency.Split(";");
                    if (currencyList.Length != 3)
                        return "Your input line is invalid. It should it must be in the format: DD;DA;T --> FromCurrency;ToCurrency;ExchangeRate";
                    var dd=currencyList[0].ToUpper();
                    var dc=currencyList[1].ToUpper();
                    double dm= Convert.ToDouble(currencyList[2], CultureInfo.InvariantCulture);
                    if (!verifyNumber(dm))
                        return "Amount type is invalid";

                    userListCurrency.Add(new ConverterModel { BaseCurrency = dd, TargetCurrency = dc, ExchangeRate = Math.Round( dm, 4) });

                    
                }
               foreach (var currency in userListCurrency)
                {
                    
                    if ((currency.BaseCurrency == deviseDepart && currency.TargetCurrency == deviseCible ) ||
                        (currency.BaseCurrency == deviseCible && currency.TargetCurrency == deviseDepart))
                    {
                        userListConvertCurrency.Add(currency);
                        break;
                    }
                    else
                    {
                        if (currency.BaseCurrency == deviseDepart )
                        {
                            BaseCurrency.Add(currency);
                        }
                        if (currency.TargetCurrency == deviseCible)
                        {
                            tragetCurrecny.Add(currency);
                        }
                    }
             
                }
                var tttt = userListConvertCurrency;
                var t = BaseCurrency;
                var tt = tragetCurrecny;
                var newtragetCurrecny = new List<ConverterModel>();
                var newBaseCurrency = new List<ConverterModel>();
                if (userListConvertCurrency.Count() == 0)
                {
                    foreach (var cur in BaseCurrency)
                    {
                        for (int i = 0; i < userListCurrency.Count(); i++)
                        {
                            if (cur.TargetCurrency == userListCurrency[i].TargetCurrency)
                            {
                                newBaseCurrency.Add(userListCurrency[i]);
                            }

                        }
                    }
                    var ts = newBaseCurrency.Distinct();
                   
                    foreach (var tar in tragetCurrecny)
                    {
                        for (int i = 0; i < userListCurrency.Count(); i++)
                        {
                            if (tar.BaseCurrency == userListCurrency[i].BaseCurrency)
                            {
                                newtragetCurrecny.Add(userListCurrency[i]);
                            }
                        }
                    }
                    var tts = newtragetCurrecny.Distinct(); 

                    var newList = ts.Concat(tts).Distinct().ToList();
                    var finalList = new List<ConverterModel>(); 
                    foreach (var last in newList)
                    {
                        var nbroccurencetraget = newList.Where(y => y.TargetCurrency == last.TargetCurrency).Count();
                        var nbroccurencebase = newList.Where(y => y.BaseCurrency == last.BaseCurrency).Count();
                        if(nbroccurencetraget > 1 && nbroccurencebase > 1 )
                        {
                            finalList.Add(last);
                        }
                        else
                        {
                           if((nbroccurencetraget == 1 && last.TargetCurrency == deviseCible) || (nbroccurencebase ==1 && last.BaseCurrency == deviseDepart))
                                finalList.Add(last);
                        }
                    }
                    //sort list
                    userListConvertCurrency = finalList;
                    var firstConvert = finalList.Where(currency => currency.BaseCurrency == deviseDepart).FirstOrDefault();
                    var firstValue = Math.Round(amount * firstConvert.ExchangeRate, 4) ;
                    var lastConvert = finalList.Where(currency => currency.TargetCurrency == deviseCible).FirstOrDefault();
                    var meduimList = finalList.Where(currency => currency.BaseCurrency != deviseDepart && currency.TargetCurrency != deviseCible).ToList();
                    for (int i = 0; i < meduimList.Count(); i++)
                    {
                        if (finalList[i].BaseCurrency != lastConvert.TargetCurrency  && finalList[i].TargetCurrency == firstConvert.TargetCurrency)
                        {
                            firstValue = Math.Round(firstValue * (1 / finalList[i].ExchangeRate), 4);
                        }
                        else
                        {
                            firstValue = firstValue * finalList[i].ExchangeRate;
                        }
                    }
                 
                    var ValeurFinal = (int) Math.Floor(firstValue * lastConvert.ExchangeRate);
                    result = ValeurFinal.ToString();

                }
                else
                {
                    var line = userListConvertCurrency.FirstOrDefault();
                    if (line.BaseCurrency == deviseDepart)
                    {
                        var reslt = (int)Math.Floor(amount * Convert.ToDouble(line.ExchangeRate));
                        result = reslt.ToString();
                    }
                    else
                    { 
                        var reslt = (int)Math.Floor(Convert.ToDouble(line.ExchangeRate) / amount);
                        result = reslt.ToString();

                    }
                    
                    
                }
  
            }
            catch { return ""; }
            return result;
        }

        private static bool verifyNumber(double nbr)
        {
            return  nbr/nbr == 1;
        }
    }
   
}
