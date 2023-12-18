using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Factory;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AM.UI.Command.Home
{
    public class UpdateCurrentHomeViewModelCommand : CommandBase
    {
        private readonly IHome _Ihome;
        private readonly HomeStore _HomeStore;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;

        public UpdateCurrentHomeViewModelCommand(IHome ihome, HomeStore homeStore, INavigator navigator, IAparmentViewModelFactory factory)
        {
            _Ihome=ihome;
            _HomeStore=homeStore;
            _navigator=navigator;
            _factory=factory;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ViewHomeType)
            {
                ViewHomeType viewHomeType = (ViewHomeType)parameter;
                switch (viewHomeType)
                {
                    case ViewHomeType.Bill:
                        _navigator.CurrentHomeViewModel = new HomeBillListingViewModel(_Ihome, _HomeStore, _navigator, _factory);
                        break;

                    case ViewHomeType.EmptyRoom:
                        _navigator.CurrentHomeViewModel = new HomeEmptyRoomListtingVMUI(_Ihome, _HomeStore, _navigator, _factory);
                        break;

                    case ViewHomeType.Customer:
                        _navigator.CurrentHomeViewModel = new HomeViewDetailCustomerVMUI(_Ihome, _HomeStore, _navigator, _factory);
                        break;

                    case ViewHomeType.Payment:
                        _navigator.CurrentHomeViewModel = new HomePaymentListtingVMUI(_Ihome, _HomeStore, _navigator, _factory);
                        break;

                    default:
                        throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
                }
            }
        }
    }
}