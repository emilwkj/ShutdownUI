using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ShutdownUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TimeHandler _timeHandler;
        private DispatcherTimer _timer;
        public MainWindow()
        {
            _timeHandler = new TimeHandler();
            InitializeComponent();
        }

        /// <summary>
        /// Event for the Shutdown Button on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_shutdown_Click(object sender, RoutedEventArgs e)
        {
            //Regex for checking if timespan input in textfield is valid
            Regex timeSpan = new Regex(@"\d{2}[:]\d{2}[:]\d{2}");

            //Checking if the timespan is valid
            if (timeSpan.IsMatch(textBox_time.Text))
            {
                TimeSpan span = TimeSpan.Parse(textBox_time.Text);

                //Checking if the time is not zero seconds
                if(span.Ticks > 0)
                {
                    //Creating the new timer, with the timespan
                    _timer = _timeHandler.newTimer(span);

                    //Enable the Abort shutdown button and disable the Shutdown button
                    button_shutdown.IsEnabled = false;
                    button_abort.IsEnabled = true;
                    MessageBox.Show($"Your PC will shutdown in {span.TotalSeconds} seconds");
                }
                else
                {
                    //If no time was entered, messagebox pops up
                    MessageBox.Show("Please enter time to shutdown!");
                }
            }
            else
            {
                //Telling the user to input correct format!
                MessageBox.Show("Please enter vaild time-format (Hour:Minutes:Seconds), (HH:MM:SS)");
            }
        }

        private void button_abort_Click(object sender, RoutedEventArgs e)
        {
            //Checking if the timer is not null. This shouldn't be nescesarry because that this button is only enabled when the timer is running, but you never know....
            if(_timer != null)
            {
                //Stop the timer
                _timer.Stop();

                //Button Styling again
                button_shutdown.IsEnabled = true;
                button_abort.IsEnabled = false;

                //Tell user that shutdown has been aborted!
                MessageBox.Show("Shutdown has been aborted");
            }
        }
    }
}
