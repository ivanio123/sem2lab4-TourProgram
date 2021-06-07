using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TourProgram.Models;
using TourProgram.Views;

namespace TourProgram.ViewModels
{
    public class TourViewModel : INotifyPropertyChanged
    {
        public TourViewModel(Tour Tour)
        {

            this.Tour = Tour;
        }
        public TourViewModel(Tour Tour, TourWindow window)
        {

            this.Tour = Tour;
            this.Window = window;
        }
        public TourViewModel(Tour Tour, TourWindow window, bool Edit)
        {

            this.Tour = Tour;
            this.Window = window;
            this.Edit = Edit;
        }
        public TourViewModel()
        {

            Tour = new Tour();
        }
        public bool Edit { get; set; }
        public RelayCommand CloseWindowCommand { get; private set; }
        public Tour Tour { get; set; }
        private DateTime _StartDate;
        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        private decimal _Price;
        public decimal Price
        {
            get
            {
                
                return _Price;
            }
            set
            {
                _Price = value;
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private RelayCommand addTour;
        public RelayCommand AddTour
        {
            get
            {
                return addTour ??
                    (addTour = new RelayCommand(obj =>
                    {
                        Tour tour = new Tour(StartDate);
                        if (Edit == false)
                        {
                            AddT((ToursProgramWindow)App.Current.MainWindow);
                        }
                        else
                        {
                            EditT((ToursProgramWindow)App.Current.MainWindow);

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
                        Window.Close();

                    }));
            }
        }
        private TourWindow _window;
        public TourWindow Window
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
        public void AddT(ToursProgramWindow Window)
        {
            ToursViewModel vm = ((ToursViewModel)Window.DataContext);
            Tour tour = new Tour(StartDate);
           
            vm.Tours.Add(tour);
            Refresh();
        }
        public void EditT(ToursProgramWindow Window)
        {
            ToursViewModel vm = ((ToursViewModel)Window.DataContext);
            Tour tour = new Tour(StartDate);
            ListBox TourList = (ListBox)Application.Current.Windows[0].FindName("TourList");
            vm.Tours[TourList.SelectedIndex] = tour;
            TourList.Items.Refresh();
            _window.Close();
        }
        public void Refresh()
        {
            ListBox TourList = (ListBox)Application.Current.Windows[0].FindName("TourList");
            TourList.Items.Refresh();
        }

    }
}
