using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Models;

namespace eCommerce.Library.Services
{
    public class GlobalVariableService
    {
        private static GlobalVariableService instance;
        private static object instanceLock = new object();

        private GlobalVariableService() 
        {
            globalVar = new GlobalVariables();   
        }

        private GlobalVariables globalVar;

        public GlobalVariables GlobalVar
        {
            get
            {
                return globalVar;
            }
        }

        public void SetTaxRatePercent(decimal? taxRatePercent)
        {
            globalVar.TaxRatePercent = taxRatePercent ?? 0;
        }

        public static GlobalVariableService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new GlobalVariableService();
                    }
                }
                return instance;
            }
        }
    }
}
