using System;

namespace TimeTrackR.Core.Timer
{
    public class OnStateChangedEventArgs : EventArgs
    {
        public Timer.States State { get; set; }
    }
}