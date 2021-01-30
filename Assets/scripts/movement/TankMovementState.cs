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

            if(!Context.IsGrounded)
                Context.Move(new Vector3(0, 0, Context.DesiredMovement.y), Context.WantsToRun);

            if(Context.DesiredTurning == 0.0f)
                Context.Turn(Context.DesiredMovement.x);
            else
                Context.Turn(Context.DesiredTurning);

            if(Context.WantsToJump)
                Context.Jump();
        }
    }
}