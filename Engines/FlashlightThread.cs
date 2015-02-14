using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNAF.Engines
{
    class FlashlightThread
    {
        private const ushort POWER_DECREMENT_PERCENT = 5;
        private const ushort POWER_DECREMENT_FREQUENCY_TICKS = 10;

        private bool _on = false;
        private ushort _powerRemaining = 100;
        private Nullable<DateTime> _lightOnTime = null;
        private Nullable<DateTime> _lastLightOnCheck = null;
        private TimeSpan _powerDecrementFrequency = new TimeSpan(POWER_DECREMENT_FREQUENCY_TICKS);
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
        }

        public FlashlightThread()
        {
        }

        /// <summary>
        /// Start begins the infinite loop for this thread monitoring the light on variables
        /// </summary>
        public void Start()
        {
            long decrementFrequencyCount = 0;
            bool wasLightOnLastTimeChecked = _on;

            //
            // Main flashlight loop. This does not exit until signaled by thread caller
            //
            while (true)
            {
                //
                // first check if there is any power remaining in the falshlight. If not
                // then turn the flashlight off and skip further processing.
                //
                if (_powerRemaining == 0)
                {
                    lock (_lock)
                    {
                        _on = false;
                    }
                    continue;
                }

                //
                // If the flashlight is on process the power remaining
                //
                if (_on)
                {
                    //
                    // Lock as we're going to be modifying variables visible to the user.
                    //
                    lock (_lock)
                    {
                        //
                        // Update the time the light was turned on to now. This should always be null 
                        // the first time the light is turned on.
                        //
                        if (_lightOnTime == null || _lastLightOnCheck == null)
                        {
                            _lightOnTime = DateTime.Now;
                            _lastLightOnCheck = _lightOnTime;
                        }

                        //
                        // Check to see if the last time the time was checked was greater than the 
                        // decrement frequency. This is basically checking to see if the last time 
                        // we checked whether we needed to decrease power was longer away in time 
                        // than that time which we should decrease telling us to decrement the power.
                        //
                        if (DateTime.Now > _lastLightOnCheck.Value.Add(_powerDecrementFrequency))
                        {
                            //
                            // This variable is used to determine how many decreases we should do. 
                            // Since the loop doesn't sleep there shouldn't be more than a second 
                            // that goes by each time but if the system is under heavy load then maybe. 
                            // This ensures the power decreases at the correct interval syncing the 
                            // decrement with the time.
                            //
                            decrementFrequencyCount = DateTime.Now.Subtract(
                                _lastLightOnCheck.Value
                            ).Ticks / POWER_DECREMENT_FREQUENCY_TICKS;

                            //
                            // This decreases the power remaining percentage by the decrement frequency
                            // count. Resets the last time we checked if the light was on time. And 
                            // resets te decrement frequency count as well.
                            //
                            _powerRemaining -= (ushort)(POWER_DECREMENT_PERCENT * decrementFrequencyCount);
                            _lastLightOnCheck = DateTime.Now;
                            decrementFrequencyCount = 0;
                        }
                    }
                }
                else
                {
                    lock (_lock)
                    {
                        //
                        // These are used when the light is on and need to be reset 
                        // when the light turns back on from an off state.
                        //
                        _lightOnTime = null;
                        _lastLightOnCheck = null;
                    }
                }
            }
        }
    }
}
