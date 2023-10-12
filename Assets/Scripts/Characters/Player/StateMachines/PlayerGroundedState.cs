using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);      //AnimationData�� �ִϸ��̼� �Ķ���� ������ ����
    }

    public override void Exit() 
    { 
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if(stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }
        stateMachine.ChangeState(stateMachine.IdleState);       //Ground�� �ƴ� �ٸ� State�� ���� �� Key�� ���� �ٸ� ������ �ؾ� ��. �� �κ��� Grond�� �� Ű�� ���� ��� �� �ųĿ� ���� ���̱� ������ ���⿡ ���� �� ����

        base.OnMovementCanceled(context);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected virtual void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.ComboAttackState);
    }
}
