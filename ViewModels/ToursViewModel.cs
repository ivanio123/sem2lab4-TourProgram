using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using TourProgram.Models;
using TourProgram.Views;


namespace TourProgram.ViewModels
{
    class ToursViewModel : INotifyPropertyChanged
    {
        public ToursViewModel(List<Tour> Tours)
        {
            this.Tours = Tours;
        }
        public ToursViewModel()
        {
            Tour testTour1 = new Tour(DateTime.Now);
            Tour testTour2 = new Tour(DateTime.UtcNow);
            Tours = new List<Tour>();
            Tours.Add(testTour1);
            Tours.Add(testTour2);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        private List<Tour> _Tours;
        public List<Tour> Tours
        {
            get { return _Tours; }
            set
            {
                _Tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }
        private List<Excursion> _Excursions;
        public List<Excursion> Excursions
        {
            get { return _Excursions; }
            set
            {
                _Excursions = value;
                OnPropertyChanged(nameof(Excursions));
            }
        }
        private RelayCommand addTourCommand;
        public RelayCommand AddTourCommand
        {
            get
            {
                return addTourCommand ??
                    (addTourCommand = new RelayCommand(obj =>
                    {
                        TourWindow tourWindow = new TourWindow();
                        tourWindow.DataContext = new TourViewModel(SelectedTour, tourWindow);
                        tourWindow.Show();
                        Calculate();
                    }));
            }
        }
        private RelayCommand editTourCommand;
        public RelayCommand EditTourCommand
        {
            get
            {
                return editTourCommand ??
                    (editTourCommand = new RelayCommand(obj =>
                    {
                        if (SelectedTour != null)
                        {
                            TourWindow tourWindow = new TourWindow();
                            tourWindow.DataContext = new TourViewModel(SelectedTour, tourWindow, true);
                            TourViewModel vm = ((TourViewModel)tourWindow.DataContext);
                            vm.StartDate = SelectedTour.Start;
                            vm.Window = tourWindow;
                            tourWindow.Show();
                            Calculate();
                        }
                    }));
            }
        }

        private RelayCommand deleteTourCommand;
        public RelayCommand DeleteTourCommand
        {
            get
            {
                return deleteTourCommand ??
                    (deleteTourCommand = new RelayCommand(obj =>
                    {

                        if (SelectedTour != null)
                        {
                            Tours.Remove(SelectedTour);
                            Refresh();
                            Calculate();
                        }
                    }));
            }
        }
        private RelayCommand openTourCommand;
        public RelayCommand OpenTourCommand
        {
            get
            {
                return openTourCommand ??
                    (openTourCommand = new RelayCommand(obj =>
                    {
                        if (SelectedTour != null)
                        {
                            ExcursionsWindow excursionsWindow = new ExcursionsWindow();
                            excursionsWindow.DataContext = new ExcursionsViewModel(SelectedTour);
                            excursionsWindow.ExcursionList.Items.Refresh();
                            excursionsWindow.Show();
                            Calculate();
                        }
                    }));
            }
        }
        private RelayCommand serialize;
        public RelayCommand Serialize
        {
            get
            {
                return serialize ??
                    (serialize = new RelayCommand(obj =>
                    {
                        string fileName = Directory.GetCurrentDirectory() + "\\data.xml";
                        Serialization serialization = new Serialization(Tours, fileName);
                        serialization.Serialize();

                    }));
            }
        }
        private RelayCommand deserialize;
        public RelayCommand Deserialize
        {
            get
            {
                return deserialize ??
                    (deserialize = new RelayCommand(obj =>
                    {
                        string fileName = Directory.GetCurrentDirectory() + "\\data.xml";
                        Serialization serialization = new Serialization(Tours, fileName);
                        Tours = serialization.Deserialize();

                    }));
            }
        }
        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                TextBlock Block = (TextBlock)Application.Current.Windows[0].FindName("Info");
                if (SelectedTour != null)
                {
                    Block.Text = _selectedTour.GenerateFullInformation();
                }
                else
                {
                    Block.Text = "";
                }
                Calculate();
            }
        }

        public void Refresh()
        {
            ListBox TourList = (ListBox)Application.Current.Windows[0].FindName("TourList");
            TourList.Items.Refresh();
        }
        public void Calculate()
        {
            foreach (var tour in Tours)
            {
                tour.CalculatePrice();
            }
        }
    }
}
