using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ShutdownUI
{
    public class TimeHandler
    {

        //All the beloved dependencies for this class.
        #region Dependencies
        private readonly MainWindow _mainWindow;
        private readonly ShutdownHandler _shutdownHandler;
        #endregion

        /// <summary>
        /// The Consructor for the TimeHandler class...
        /// </summary>
        public TimeHandler()
        {
            //Getting an instance of the mainwindow
            _mainWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);
            _shutdownHandler = new ShutdownHandler();
        }

        /// <summary>
        /// Yebikeyee motherfucker, let's make a new timer
        /// </summary>
        /// <param name="TimeLeft"></param>
        /// <returns></returns>
        public DispatcherTimer newTimer(TimeSpan TimeLeft)
        {
            //So what i am doing here is very simple, just setting the textbox showing how much time there is left until your computer gets mad and shuts down...
            _mainWindow.textBox_timer.Text = TimeLeft.ToString();

            //Creating the actual fucking timer-object
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

            //Creating an event for everytime this shits ticks
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

            //Defining that this awesome timer should tick every 1 second.
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            //Starting the tiemr
            dispatcherTimer.Start();

            //Returning an instance of the timer.
            return dispatcherTimer;
        }

        /// <summary>
        /// The event handler for when the timer ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Dear god who made this shit code... Oh that's right thats me. Anyways here we parse the timeleft from the textbox showing how much time that is left ^^
            TimeSpan timeLeft = TimeSpan.Parse(_mainWindow.textBox_timer.Text);

            //Now, removing 1 second from the timespan
            timeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));

            //Checking if the timer reached Zero
            if(timeLeft.Ticks <= 0)
            {
                //It did, hurrayy, btw, under here is the button styling once again
                _mainWindow.button_shutdown.IsEnabled = true;
                _mainWindow.button_abort.IsEnabled = false;

                //Getting the timer object
                DispatcherTimer timer = (DispatcherTimer)sender;

                //Stopping the timer :P
                timer.Stop();

                //Giving a message to the user
                MessageBox.Show("Computer Will Shutdown Now");

                //Calling the method that shuts the pc down...
                _shutdownHandler.Shutdown();
            }
            //Setting the textbox to how much time that is left.
            _mainWindow.textBox_timer.Text = timeLeft.ToString();
        }

        
    }
}
