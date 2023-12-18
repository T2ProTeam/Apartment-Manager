using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Account;

namespace Services.Interface
{
    public interface IAccount
    {
        Task<int> CreateAccount(AccountCreateViewModel model);
        Task<int> UpdateAccount(AccountUpdateViewModel model);
        Task<int> DeleteAccount(int AccountID);

    }
}