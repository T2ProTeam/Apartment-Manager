﻿using AM.UI.State;
using AM.UI.ViewModelUI;
using Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.LoadDataBase
{
    public class LoadRentalContractView : AsyncCommandBase
    {
        private readonly RentalContractHomeVMUI _rentalcontracthome;
        private readonly ApartmentStore _rental;
        public LoadRentalContractView(RentalContractHomeVMUI rentalcontracthome, ApartmentStore rental)
        {
            _rentalcontracthome = rentalcontracthome;
            _rental = rental;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _rentalcontracthome.IsLoading = true;
            try
            {           
                  await _rental.LoadeRentalContract();
                 _rentalcontracthome.UpdateData(_rental.rentalvm);
            }catch (Exception)
            {
                _rentalcontracthome.ErrorMessage = "Can't Load RentalContract Database";
            }
            _rentalcontracthome.IsLoading = false;
        }
        
    }
}
