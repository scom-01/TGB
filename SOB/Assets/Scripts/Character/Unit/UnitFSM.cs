using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFSM
{
    public UnitState CurrentState { get; private set; }

    //State ���� �� ȣ��Ǵ� �Լ�
    public void Initialize(UnitState startingState)
    {
        //���� State ����
        CurrentState = startingState;
        CurrentState.Enter();
    }

    //State ���� �� ȣ��Ǵ� �Լ�
    public void ChangeState(UnitState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
