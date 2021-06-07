using System.Windows;

namespace TourProgram.Views
{
    /// <summary>
    /// Interaction logic for ExcursionWindow.xaml
    /// </summary>
    public partial class ExcursionWindow : Window
    {
        public ExcursionWindow()
        {
            InitializeComponent();
            comboBox.Items.Add("ByBus");
            comboBox.Items.Add("Walking");
            comboBox.Items.Add("HorsebackRide");
            comboBox.Items.Add("TripTransport");

        }
    }
}
