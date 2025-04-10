using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemoryGame2.ViewModel
{
    public class TimeManager : BaseClass
    {

        private readonly DispatcherTimer _dispatcherTimer;

        private DateTime _deadline;
        

        public event EventHandler TimeExpired;


        private int _timeLeft;
        public int TimeLeft
        {
            get { return _timeLeft; }
            private set
            {
                if (_timeLeft != value)
                {
                    _timeLeft = value;
                    OnPropertyChanged(nameof(TimeLeft));
                }
            }
        }


        private int _delay;
        public int Delay
        {
            get => _delay;
            set => _delay = value;
        }

        public TimeManager(int delayInSeconds)
        {
            _delay = delayInSeconds;
            TimeLeft = _delay;
            _dispatcherTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        public void Start()
        {
            _deadline = DateTime.Now.AddSeconds(_delay);
            TimeLeft = _delay;
            _dispatcherTimer.Start();
        }

        public void Stop()
        {
            _dispatcherTimer.Stop();
           
            int secondsRemaining = (int)(_deadline - DateTime.Now).TotalSeconds;
            _delay = secondsRemaining > 0 ? secondsRemaining : 0;
            TimeLeft = _delay;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Calculate the remaining seconds
            int secondsRemaining = (int)(_deadline - DateTime.Now).TotalSeconds;

            if (secondsRemaining <= 0)
            {
                _dispatcherTimer.Stop();
                TimeLeft = 0;
                TimeExpired?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                TimeLeft = secondsRemaining;
            }
        }

    }
}
