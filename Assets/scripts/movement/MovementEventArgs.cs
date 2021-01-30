using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021.MovementEvents
{
    public class MovementEventArgs : System.EventArgs { }
    public class StartedMoving : MovementEventArgs { }
    public class StoppedMoving : MovementEventArgs { }
    public class Jumped : MovementEventArgs { }
    public class LandedEventArgs : MovementEventArgs { }
}