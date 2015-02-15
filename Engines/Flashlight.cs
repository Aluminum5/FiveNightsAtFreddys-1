using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FNAF.Engines
{
    /// <summary>
    /// This event is triggered when the image of the flashlight changes. This can be used to 
    /// change out the flashlight image as power decreases.
    /// </summary>
    /// <param name="sender">The flashlight itself.</param>
    /// <param name="e">Any event data needing to be provided to the owner.</param>
    public delegate void FlashlightImageChanged(object sender, EventArgs e);
    /// <summary>
    /// This event is triggered when the flashlight runs out of power. This can be used by the 
    /// developer to switch the flashlight image off.
    /// </summary>
    /// <param name="sender">The flashlight itself.</param>
    /// <param name="e">Any event data needing to be provided to the owner.</param>
    public delegate void FlashlightOutOfPower(object sender, EventArgs e);

    /// <summary>
    /// Errors returned from the Flashlight class methods.
    /// </summary>
    public enum FlashlightResult
    {
        /// <summary>
        /// The method was successfull.
        /// </summary>
        Success,
        /// <summary>
        /// The flashlight is already on so it cannot be toggled on.
        /// </summary>
        AlreadyOn,
        /// <summary>
        /// The flashligh is already off so it cannot be toggled off.
        /// </summary>
        AlreadyOff,
        /// <summary>
        /// The flashlight is out of power and cannot be toggled on.
        /// </summary>
        OutOfPower
    }
    public class Flashlight : ThreadBase
    {
        /// <summary>
        /// Constants.
        /// </summary>
        private const ushort POWER_DECREMENT_PERCENT = 5; // 5 power measurement decreased every TIMER_INTERVAL
        private const double TIMER_INTERVAL = 1000; // 10 seconds

        /// <summary>
        /// Member variables. Used through out the class.
        /// </summary>
        private bool _on = false;
        private ushort _powerRemaining = 100;
        private Timer _timer = new Timer(TIMER_INTERVAL);
        private object _lock = new object();
        private Image _image = global::FNAF.Properties.Resources.Power_100;

        /// <summary>
        /// The percentage of Power that is remaining before the flashlight can no longer be 
        /// turned on.
        /// </summary>
        public ushort PowerRemaining
        {
            get
            {
                return _powerRemaining;
            }
        }
        /// <summary>
        /// Whether the flashlight is on and draining power or not.
        /// </summary>
        public bool On
        {
            get
            {
                return _on;
            }
        }
        /// <summary>
        /// The image to display for the flashlight. This image changes as the battery decreases 
        /// in power.
        /// </summary>
        public Image Image
        {
            get
            {
                return _image;
            }
        }
        /// <summary>
        /// This event is triggered when the image of the flashlight changes. This can be used to 
        /// change out the flashlight image as power decreases.
        /// </summary>
        public event FlashlightImageChanged ImageChanged;
        /// <summary>
        /// This event is triggered when the image of the flashlight changes. This can be used to 
        /// change out the flashlight image as power decreases.
        /// </summary>
        public event FlashlightOutOfPower OutOfPower;

        /// <summary>
        /// The constructor for the flashlight. Sets up the event handlers.
        /// </summary>
        public Flashlight() : base("Flashlight")
        {
            //
            // The event that will be triggered when the timer starts and the timer only starts 
            // when the flashlight has changed.
            //
            _timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// Start begins the infinite loop for this thread monitoring the light on variables
        /// </summary>
        protected override void Start(object param)
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
                if (_powerRemaining == 0 && _on == true)
                {
                    lock (_lock)
                    {
                        _on = false;

                        if (this.OutOfPower != null)
                        {
                            //
                            // Notify the caller that the flashlight is out of power.
                            //
                            OutOfPower(this, new EventArgs());
                        }
                    }

                    if (_timer.Enabled)
                    {
                        lock (_lock)
                        {
                            _timer.Stop();
                        }
                    }
                }
            } // Main while loop
        } // Start()
        /// <summary>
        /// Turns the flashlight on starting the power drain.
        /// </summary>
        /// <returns>
        /// FlashlightResult : 
        ///     Success - Flashlight was turned on Successfully
        ///     AlreadyOn - Flashlight was already on and was asked to turn on again.
        ///     OutOfPower - Flashlight is out of power and was asked to turn on.
        /// </returns>
        public FlashlightResult TurnOn()
        {
            return ToggleFlashlight(true);
        }
        /// <summary>
        /// Turns the flashlight off stopping the power drain.
        /// </summary>
        /// <returns>
        /// FlashlightResult : 
        ///     Success - Flashlight was turned off Successfully
        ///     AlreadyOff - Flashlight was already off and was asked to turn off again.
        /// </returns>
        public FlashlightResult TurnOff()
        {
            return ToggleFlashlight(false);
        }        

        /// <summary>
        /// This event is triggered every time TIMER_INTERVAL milliseconds has elapsed.  
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">The arguments passed by the sending object.</param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
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

            //
            // After the _powerRemaining has decreased update the image
            //
            UpdateImage();
        }
        /// <summary>
        /// Turns the flashligh on or off depending on the <paramref name="onOff"/> parameter value.
        /// </summary>
        /// <param name="onOff">true: Turns the flashlight on, false: turns the flashlight off</param>
        /// <returns>
        /// FlashlightResult : 
        ///     AlreadyOn - Flashlight was already on and was asked to turn on again.
        ///     AlreadyOff - Flashlight was already off and was asked to turn off again.
        ///     OutOfPower - Flashlight is out of power and was asked to turn on.
        /// </returns>
        private FlashlightResult ToggleFlashlight(bool onOff)
        {
            //
            // Check to see if the flashlight is already on, already off, or out of power.
            //
            if (_on == onOff) return _on ? FlashlightResult.AlreadyOn : FlashlightResult.AlreadyOff;
            if (_on != onOff && _on == false && _powerRemaining == 0) return FlashlightResult.OutOfPower;

            //
            // Must lock around _on object as it is used within the start method which must be 
            // threadsafe.
            //
            lock (_lock)
            {
                //
                // Toggle the flashligh as the state the user is asking for appears valid.
                //
                _on = onOff;
            }


            return FlashlightResult.Success;
        }
        /// <summary>
        /// Updates this.Image to the proper image for the state the flashlights power is in.
        /// </summary>
        private void UpdateImage()
        {
            lock (_lock)
            {
                if (_powerRemaining == 100)
                {
                    _image = global::FNAF.Properties.Resources.Power_100;
                }
                else if (_powerRemaining <= 90 && _powerRemaining > 80)
                {
                    _image = global::FNAF.Properties.Resources.Power_90;
                }
                else if (_powerRemaining <= 80 && _powerRemaining > 70)
                {
                    _image = global::FNAF.Properties.Resources.Power_80;
                }
                else if (_powerRemaining <= 70 && _powerRemaining > 60)
                {
                    _image = global::FNAF.Properties.Resources.Power_70;
                }
                else if (_powerRemaining <= 60 && _powerRemaining > 50)
                {
                    _image = global::FNAF.Properties.Resources.Power_60;
                }
                else if (_powerRemaining <= 50 && _powerRemaining > 40)
                {
                    _image = global::FNAF.Properties.Resources.Power_50;
                }
                else if (_powerRemaining <= 40 && _powerRemaining > 30)
                {
                    _image = global::FNAF.Properties.Resources.Power_40;
                }
                else if (_powerRemaining <= 30 && _powerRemaining > 20)
                {
                    _image = global::FNAF.Properties.Resources.Power_30;
                }
                else if (_powerRemaining <= 20 && _powerRemaining > 10)
                {
                    _image = global::FNAF.Properties.Resources.Power_20;
                }
                else if (_powerRemaining <= 10 && _powerRemaining > 5)
                {
                    _image = global::FNAF.Properties.Resources.Power_10;
                }
                else if (_powerRemaining <= 5 && _powerRemaining > 0)
                {
                    _image = global::FNAF.Properties.Resources.Power_5;
                }
                else if (_powerRemaining == 0)
                {
                    _image = global::FNAF.Properties.Resources.Power_0;
                }

                if (this.ImageChanged != null)
                {
                    this.ImageChanged(this, new EventArgs());
                }
            }

            return;
        }
    }
}
