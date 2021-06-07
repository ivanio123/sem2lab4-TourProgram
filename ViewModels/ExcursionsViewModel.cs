using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using TourProgram.Models;
using TourProgram.Views;


namespace TourProgram.ViewModels
{
    public class ExcursionsViewModel : INotifyPropertyChanged
    {
        public ExcursionsViewModel(List<Excursion> Excursions)
        {
            this.Excursions = Excursions;
            ExcursionsWindow ExcursionsWindow = null;
            foreach (var excursion in App.Current.Windows)
            {
                if (excursion is ExcursionsWindow)
                {
                    ExcursionsWindow = (ExcursionsWindow)excursion;
                }
            }
            ExcursionsList = ExcursionsWindow.ExcursionList;
        }
        public ExcursionsViewModel(Tour Tour)
        {
            this.Tour = Tour;
            Excursions = Tour.Excursions;
            ExcursionsWindow ExcursionsWindow = null;
            foreach (var excursion in App.Current.Windows)
            {
                if (excursion is ExcursionsWindow)
                {
                    ExcursionsWindow = (ExcursionsWindow)excursion;
                }
            }
            ExcursionsList = ExcursionsWindow.ExcursionList;
        }
        public ExcursionsViewModel()
        {

            Excursions = new List<Excursion>();
            ExcursionsWindow ExcursionsWindow = null;
            foreach (var excursion in App.Current.Windows)
            {
                if (excursion is ExcursionsWindow)
                {
                    ExcursionsWindow = (ExcursionsWindow)excursion;
                }
            }
            ExcursionsList = ExcursionsWindow.ExcursionList;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            CheckFullInfo();
        }
        public Tour Tour { get; set; }
        public ListBox ExcursionsList { get; set; }
        private List<Excursion> _Excursions;
        public List<Excursion> Excursions
        {
            get { return _Excursions; }
            set
            {
                _Excursions = value;
                OnPropertyChanged(nameof(Excursions));
                CheckFullInfo();
            }
        }

        private RelayCommand addExcursionCommand;
        public RelayCommand AddExcursionCommand
        {
            get
            {
                return addExcursionCommand ??
                    (addExcursionCommand = new RelayCommand(obj =>
                    {
                        ExcursionWindow excursionWindow = new ExcursionWindow();
                        
                        excursionWindow.DataContext = new ExcursionViewModel(Tour, SelectedExcursion, excursionWindow);
                        excursionWindow.Show();
                       
                        Calculate();
                        Refresh();
                        CheckFullInfo();
                    }));
            }
        }
        private RelayCommand editExcursionCommand;
        public RelayCommand EditExcursionCommand
        {
            get
            {
                return editExcursionCommand ??
                    (editExcursionCommand = new RelayCommand(obj =>
                    {
                        if (Tour != null)
                        {
                            ExcursionWindow excursionWindow = new ExcursionWindow();
                            excursionWindow.DataContext = new ExcursionViewModel(Tour, SelectedExcursion, excursionWindow, true);
                            ExcursionViewModel vm = ((ExcursionViewModel)excursionWindow.DataContext);
                            vm.CarryingOut = SelectedExcursion.CarryingOut.carryingOut;
                            vm.Name = SelectedExcursion.Organizer.Name;
                            vm.Surname = SelectedExcursion.Organizer.Surname;
                            vm.ExcursionPrice = SelectedExcursion.ExcursionPrice;
                            vm.ExcursionPlace = SelectedExcursion.ExcurcionPlace;
                            excursionWindow.Show();
                            
                            Calculate();
                            Refresh();
                            CheckFullInfo();
                        }
                    }));
            }
        }

        private RelayCommand deleteExcursionCommand;
        public RelayCommand DeleteExcursionCommand
        {
            get
            {
                return deleteExcursionCommand ??
                    (deleteExcursionCommand = new RelayCommand(obj =>
                    {

                        if (SelectedExcursion != null)
                        {
                            Excursions.Remove(SelectedExcursion);
                            Calculate();
                            Refresh();
                            CheckFullInfo();
                        }
                    }));
            }
        }

        private Excursion _selectedExcursion;
        public Excursion SelectedExcursion
        {
            get
            {
                return _selectedExcursion;
            }
            set
            {
                _selectedExcursion = value;
                OnPropertyChanged(nameof(SelectedExcursion));
                CheckFullInfo();
            }
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
        public void Refresh()
        {
            ExcursionsList.Items.Refresh();
        }
        public void Calculate()
        {
            Tour.CalculatePrice();
        }
    }
}
