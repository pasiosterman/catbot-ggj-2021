using UnityEngine;

namespace GGJ2021
{
    public class StrafeMovementState : BaseMovementState
    {
        public override void FixedExecute()
        {
            if (Context == null) return;

            if (Context.IsGrounded)
                Context.Move(new Vector3(Context.DesiredMovement.x, 0, Context.DesiredMovement.y), Context.WantsToRun);

            Context.Turn(Context.DesiredTurning);

            if (Context.WantsToJump)
                Context.Jump();
        }
    }
}