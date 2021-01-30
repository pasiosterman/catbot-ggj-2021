using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class TankMovementState : BaseMovementState
    {
        public override void FixedExecute()
        {
            if(Context == null) return;

            Context.Move(new Vector3(0, 0, Context.DesiredMovement.y), Context.WantsToRun);
            Context.Turn(Context.DesiredMovement.x);
            if(Context.WantsToJump)
                Context.Jump();
        }
    }
}