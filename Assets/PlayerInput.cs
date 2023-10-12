using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }        //Input Action ������ �� Generate C# Class�� ������ Ŭ����
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

    private void OnDisable()                // ĳ���Ͱ� �״� �� ĳ���Ͱ� disable �� ��Ȳ���� input�� �޾����� �ʵ���
    {
        InputActions.Disable();
    }
}

