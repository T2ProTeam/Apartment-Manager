using AM.UI.Command.LoadDataBase;
using AM.UI.Command.Statistics;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Data.Entity;
using Data.Enum;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel.Statistic;

namespace AM.UI.ViewModelUI.Statistics
{
    public class StatisticsHomeVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly StatisticsStore _StatisticsStore;

        private ObservableCollection<StatisticsVm> _Statistics;
        public ObservableCollection<StatisticsVm> Statistics => _Statistics;

        private ObservableCollection<StatisticsProfitVm> _StatisticsProfit;
        public ObservableCollection<StatisticsProfitVm> StatisticsProfit => _StatisticsProfit;

        private ObservableCollection<Month> _ComboboxForMonth;
        public ObservableCollection<Month> ComboboxForMonth => _ComboboxForMonth;

        private ObservableCollection<int> _ComboboxForYear;
        public ObservableCollection<int> ComboboxForYear => _ComboboxForYear;

        public ICommand ProfitButton { get; }

        public ICommand LoadDataBase { get; }

        public ICommand ElectricInputCommand { get; }
        public ICommand WaterInputCommand { get; }

        public ICommand ServiceInputCommand { get; }

        public ICommand EditGovernmentCommand { get; }

        public ICommand AddSuccess { get; }

        public ICommand AddConFirm { get; }

        private MoneyOfGovernment _GovermentMoney;

        public MoneyOfGovernment GovernmentMoney
        {
            get { return _GovermentMoney; }
            set
            {
                _GovermentMoney = value;
                OnPropertyChanged(nameof(GovernmentMoney));
            }
        }

        public bool HasData => _Statistics.Any();

        private Month _MonthForStatistics = Month.October;

        public Month MonthForStatistics
        {
            get { return _MonthForStatistics; }

            set
            {
                _MonthForStatistics = value;
                OnPropertyChanged(nameof(MonthForStatistics));
                _Statistics.Clear();
                LoadDataBase.Execute(null);
            }
        }

        private int _SelectedYear = 2023;

        public int SelectedYear
        {
            get { return _SelectedYear; }
            set
            {
                _SelectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                _Statistics.Clear();
                LoadDataBase.Execute(null);
            }
        }

        private StatisticTotalMoney _StatisticsTotal;

        public StatisticTotalMoney StatisticsTotal
        {
            get { return _StatisticsTotal; }
            set
            {
                _StatisticsTotal = value;
                OnPropertyChanged(nameof(StatisticsTotal));
            }
        }

        private StatisticsProfitVm _StatisticsProfitVM;

        public StatisticsProfitVm StatisticsProfitVM
        {
            get { return _StatisticsProfitVM; }
            set
            {
                _StatisticsProfitVM = value;
                OnPropertyChanged(nameof(StatisticsProfitVM));
            }
        }

        private int _TotalProfitMoney;

        public int TotalProfitMoney
        {
            get { return _TotalProfitMoney; }
            set
            {
                _TotalProfitMoney = value;
                OnPropertyChanged(nameof(TotalProfitMoney));
            }
        }

        private DateTime _DateCreateProfit = DateTime.Now;

        public DateTime DateCreateProfit
        {
            get { return _DateCreateProfit; }
            set
            {
                _DateCreateProfit = value;
                OnPropertyChanged(nameof(DateCreateProfit));
            }
        }

        public string _MessageError;

        public string MessageError
        {
            get { return _MessageError; }
            set
            {
                _MessageError = value;
                OnPropertyChanged(nameof(MessageError));
            }
        }

        public StatisticsHomeVMUI(INavigator navigator, IAparmentViewModelFactory viewModelFactory, StatisticsStore StatisticsStore)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _StatisticsStore = StatisticsStore;
            _Statistics = new ObservableCollection<StatisticsVm>();
            _GovermentMoney = new MoneyOfGovernment();
            _StatisticsTotal = new StatisticTotalMoney();
            _StatisticsProfitVM = new StatisticsProfitVm();
            LoadDataBase = new LoadStatisticsView(this, _StatisticsStore);
            LoadDataBase.Execute(null);

            EditGovernmentCommand = new EditGovernmentCommand(_navigator, _viewModelFactory, _StatisticsStore, this);
            ElectricInputCommand = new RelayCommand(ShowElectricInput);
            WaterInputCommand = new RelayCommand(ShowWaterInput);
            ServiceInputCommand = new RelayCommand(ShowServiceInput);
            _ComboboxForMonth = new ObservableCollection<Month>();
            _ComboboxForYear = new ObservableCollection<int>();
            _StatisticsStore.EditGovernmentMoney += Edit_Store;
            AddSuccess = new CreateGovernmentCommand(_navigator, _viewModelFactory, _StatisticsStore, this);
            AddConFirm = new RelayCommand(CreateStatistics);
            foreach (Month month in Enum.GetValues(typeof(Month)))
            {
                _ComboboxForMonth.Add(month);
            }

            for (int i = 2020; i <= 2050; i++)
            {
                _ComboboxForYear.Add(i);
            }
            _Statistics.CollectionChanged += OnReservationsChanged;
        }

        public void CreateStatistics(object parameter)
        {
            AddSuccess.Execute(null);
        }

        public void Edit_Store(MoneyOfGovernment data)
        {
            GovernmentMoney.ElectricMoneyOfGovernment = data.ElectricMoneyOfGovernment;
            GovernmentMoney.WaterMoneyOfGovernment = data.WaterMoneyOfGovernment;
            GovernmentMoney.ServiceOfGovernment = data.ServiceOfGovernment;
            _Statistics.Clear();
            LoadDataBase.Execute(null);
        }

        private void ShowElectricInput(object parameter)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a number:", "Input", "", -1, -1);
            if (int.TryParse(input, out int number))
            {
                GovernmentMoney.ElectricMoneyOfGovernment = number;
                EditGovernmentCommand.Execute(null);
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void ShowServiceInput(object parameter)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a number:", "Input", "", -1, -1);
            if (int.TryParse(input, out int number))
            {
                GovernmentMoney.ServiceOfGovernment = number;
                EditGovernmentCommand.Execute(null);
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void ShowWaterInput(object parameter)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a number:", "Input", "", -1, -1);
            if (int.TryParse(input, out int number))
            {
                GovernmentMoney.WaterMoneyOfGovernment = number;
                EditGovernmentCommand.Execute(null);
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        public void UpdateDataStatistics(List<StatisticsVm> data)
        {
            data.ForEach(x => _Statistics.Add(x));
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}