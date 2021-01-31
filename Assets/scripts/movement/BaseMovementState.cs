using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public abstract class BaseMovementState
    {
        public Mover Context { get; set; }
        public void SetContext(Mover context){Context = context; }
        public virtual void Entry() { }
        public virtual void FixedExecute() { }
        public virtual void Execute(){ }
        public virtual void Exit() { }
    }
}