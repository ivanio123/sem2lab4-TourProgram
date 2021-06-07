using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TourProgram.Models;
using TourProgram.Views;

namespace TourProgram.ViewModels
{
    public class ExcursionViewModel : INotifyPropertyChanged
    {
        public ExcursionViewModel(Excursion Excursion)
        {
            foreach (var excursion in App.Current.Windows)
            {
                if (excursion is ExcursionsWindow)
                {
                    ExcursionsWindow = (ExcursionsWindow)excursion;
                }
            }
            Excursions = ExcursionsWindow.ExcursionList;
            this.Excursion = Excursion;
        }
        public ExcursionViewModel(Tour Tour, Excursion Excursion, ExcursionWindow window)
        {
            this.Tour = Tour;
            this.Excursion = Excursion;
            this.Window = window;
            foreach (var excursion in App.Current.Windows)
            {
                if (excursion is ExcursionsWindow)
                {
                    ExcursionsWindow = (ExcursionsWindow)excursion;
                }
            }
            Excursions = ExcursionsWindow.ExcursionList;
        }
        public ExcursionViewModel(Tour Tour, Excursion Excursion, ExcursionWindow window, bool Edit)
        {
            this.Tour = Tour;
            this.Excursion = Excursion;
            this.Window = window;
            this.Edit = Edit;
            foreach (var excursion in App.Current.Windows)
            {
                if (excursion is ExcursionsWindow)
                {
                    ExcursionsWindow = (ExcursionsWindow)excursion;
                }
            }
            Excursions = ExcursionsWindow.ExcursionList;

        }
        public ExcursionViewModel()
        {

            Excursion = new Excursion();
            foreach (var excursion in App.Current.Windows)
            {
                if (excursion is ExcursionsWindow)
                {
                    ExcursionsWindow = (ExcursionsWindow)excursion;
                }
            }
            Excursions = ExcursionsWindow.ExcursionList;
        }

        private ExcursionWindow _window;
        public ExcursionWindow Window
        {
            get
            {
                return _window;
            }
            set
            {
                _window = value;
                OnPropertyChanged("Window");
            }
        }
        private CarryingOutForm.CarryingOut _carryingOut;
        public CarryingOutForm.CarryingOut CarryingOut
        {
            get
            {
                return _carryingOut;
            }
            set
            {
                _carryingOut = value;
                OnPropertyChanged("CarryingOut");
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        private decimal _excursionPrice;
        public decimal ExcursionPrice
        {
            get
            {
                return _excursionPrice;
            }
            set
            {
                _excursionPrice = value;
                OnPropertyChanged("ExcursionPrice");
            }
        }
        private string _excursionPlace;
        public string ExcursionPlace
        {
            get
            {
                return _excursionPlace;
            }
            set
            {
                _excursionPlace = value;
                OnPropertyChanged("ExcursionPrice");
            }
        }
        public ListBox Excursions { get; set; }
        public ExcursionsWindow ExcursionsWindow { get; set; }
        public bool Edit { get; set; }
        public RelayCommand CloseWindowCommand { get; private set; }
        public Excursion Excursion { get; set; }
        public Tour Tour { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private RelayCommand addExcursionCmd;
        public RelayCommand AddExcursionCmd
        {
            get
            {
                return addExcursionCmd ??
                    (addExcursionCmd = new RelayCommand(obj =>
                    {
                        CarryingOutForm carryingOut = new CarryingOutForm(CarryingOut);
                        Organizer organizer = new Organizer(Name, Surname);

                        Excursion excursion = new Excursion(organizer, carryingOut, ExcursionPrice, ExcursionPlace);
                       
                        if (Edit == false)
                        {
                            ExcursionsViewModel vm = (ExcursionsViewModel)ExcursionsWindow.DataContext;
                            bool name = excursion.CheckName();
                            bool surname = excursion.CheckSurname();
                            bool price = excursion.CheckPrice();
                            if (name) { Window.Name.Background = Brushes.LightGreen; }
                            else { Window.Name.Background = Brushes.Red; }
                            if (surname) { Window.Surname.Background = Brushes.LightGreen; }
                            else { Window.Surname.Background = Brushes.Red; }
                            if (price) { Window.Price.Background = Brushes.LightGreen; }
                            else { Window.Price.Background = Brushes.Red; }
                            if (name && surname && price)
                            {
                                vm.Excursions.Add(excursion);
                                Refresh();
                                Calculate();
                                Refresh();
                            }
                            else
                            {
                                MessageBox.Show("Plese make sure if all fields are correct");
                            }
                        }
                        else
                        {
                            ExcursionsViewModel vm = (ExcursionsViewModel)ExcursionsWindow.DataContext;
                            bool name = excursion.CheckName();
                            bool surname = excursion.CheckSurname();
                            bool price = excursion.CheckPrice();
                            if (name) { Window.Name.Background = Brushes.LightGreen; }
                            else { Window.Name.Background = Brushes.Red; }
                            if (surname) { Window.Surname.Background = Brushes.LightGreen; }
                            else { Window.Surname.Background = Brushes.Red; }
                            if (price) { Window.Price.Background = Brushes.LightGreen; }
                            else { Window.Price.Background = Brushes.Red; }
                            if (name && surname && price)
                            {
                                vm.Excursions[this.Excursions.SelectedIndex] = excursion;
                                Refresh();
                                Calculate();
                                Refresh();

                                Window.Close();
                            }
                            else
                            {
                                MessageBox.Show("Plese make sure if all fields are correct");
                            }
                        }
                    }));
            }
        }


        private RelayCommand close;
        public RelayCommand Close
        {
            get
            {
                return close ??
                    (close = new RelayCommand(obj =>
                    {
                        Calculate();
                        Refresh();
                        Window.Close();

                    }));
            }
        }

        public void Refresh()
        {
            Excursions.Items.Refresh();
            ListBox TourList = (ListBox)Application.Current.Windows[0].FindName("TourList");
            TourList.Items.Refresh();
            CheckFullInfo();
        }
        public void CheckFullInfo()
        {
            TextBlock Block = (TextBlock)Application.Current.Windows[0].FindName("Info");
            if (Tour != null)
            {
                Block.Text = Tour.GenerateFullInformation();
            }
            else
            {
                Block.Text = "";
            }
        }
        public void Calculate()
        {
            Tour.CalculatePrice();
        }
    }
}
