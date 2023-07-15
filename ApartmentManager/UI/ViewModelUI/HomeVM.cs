using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.People;

namespace AM.UI.ViewModel
{
    public class HomeVM
    {
        private readonly IPeople _Ipeople;
        public int a = 10;
        private PagedResult<CustomerVM> list;

        public HomeVM()
        {
        }

        public HomeVM(IPeople ipeople)
        {
            _Ipeople=ipeople;
            adsasd();
            a= list.PageSize;
        }

        public async void adsasd()
        {
            list = await _Ipeople.GetAllPage(GetAllPage());
        }

        private RequestPaging GetAllPage()
        {
            RequestPaging a = new RequestPaging();
            a.PageIndex =1;
            a.PageSize = 5;
            a.Keyword = null;
            return a;
        }
    }
}