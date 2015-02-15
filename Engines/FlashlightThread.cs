using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FNAF.Engines
{
    class FlashlightThread
    {
        private const ushort POWER_DECREMENT_PERCENT = 5;
        private const double TIMER_INTERVAL = 10000; // milliseconds

        private bool _on = false;
        private ushort _powerRemaining = 100;
        private Timer _timer = new Timer(TIMER_INTERVAL);
        private object _lock = new object();

        public ushort PowerRemaining
        {
            get
            {
                return _powerRemaining;
            }
        }
        public bool OutOfPower
        {
            get
            {
                return (_powerRemaining == 0);
            }
        }
        public bool On
        {
            get
            {
                return _on;
            }
            set
            {
                //
                // If the user is out of power change the value to read only.
                //
                if (_powerRemaining != 0)
                {
                    lock (_lock)
                    {
                        _on = value;
                    }
                }
            }
        }

        public FlashlightThread()
        {
            _timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// This event is triggered every time TIMER_INTERVAL milliseconds has elapsed.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_powerRemaining > 0)
            {
                lock (_lock)
                {
                    //
                    // If the timer has triggered this event the TIMER_INTERVAL seconds has 
                    // elapsed and the power must be decremented.
                    //
                    _powerRemaining -= POWER_DECREMENT_PERCENT;
                }
            }
        }

        /// <summary>
        /// Start begins the infinite loop for this thread monitoring the light on variables
        /// </summary>
        public void Start()
        {
            //
            // Main flashlight loop. This does not exit until signaled by thread caller
            //
            while (true)
            {
                //
                // If the flashlight is on but the timer has not started, start the timer to track 
                // how long the user has the flashlight on for.
                //
                if (_on == true && _timer.Enabled == false)
                {
                    lock (_lock)
                    {
                        _timer.Start();
                    }
                }

                //
                // If the flashligh is off but the timer is still running, stop the timer as it is
                // no longer needed since the flashlight is off.
                //
                if (_on == false && _timer.Enabled == true)
                {
                    lock (_lock)
                    {
                        _timer.Stop();
                    }
                }

                //
                // first check if there is any power remaining in the falshlight. If not
                // then turn the flashlight off and skip further processing. Also stop
                // the timer as it is not needed unless the flashlight is on.
                //
                if (_powerRemaining == 0)
                {
                    lock (_lock)
                    {
                        _on = false;
                    }
                    continue;
                }
            }
        }
    }
}
