using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }        //Input Action 생성한 후 Generate C# Class로 생성한 클래스
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()                // 캐릭터가 죽는 등 캐릭터가 disable 된 상황에서 input이 받아지지 않도록
    {
        InputActions.Disable();
    }
}

