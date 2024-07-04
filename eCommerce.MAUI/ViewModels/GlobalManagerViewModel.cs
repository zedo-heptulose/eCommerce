using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Models;
using eCommerce.Library.Services;

namespace eCommerce.MAUI.ViewModels
{
    public class GlobalManagerViewModel
    {

        public GlobalManagerViewModel() { }

        
        public decimal TaxRatePercent
        {
            get
            {
                return GlobalVariableService.Current.GlobalVar?.TaxRatePercent ?? 0.0M;
            }
            set
            {
                GlobalVariableService.Current.SetTaxRatePercent(value);
            }
        }

    }
}
