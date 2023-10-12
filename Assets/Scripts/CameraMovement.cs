using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy;

    PlayerInputActions input;

    public float sensitivity = 0.1f;
    public float damping = 5.0f;

    public float minYAngle = -60.0f;
    public float maxYAngle = 60.0f;

    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentRotation = Vector2.zero;
    private Vector2 rotation = Vector2.zero;

    private void Awake()
    {
        input = new PlayerInputActions();
        currentRotation.x = transform.eulerAngles.y;
        currentRotation.y = transform.eulerAngles.x;
    }
    private void Update()
    {
        if (!StageManager.instance.IsGameOver)
        {
            Vector3 pos = new Vector3(player.position.x, transform.position.y, player.position.z + 3);
            this.transform.position = pos;

            Vector2 mouseDelta = input.Player.Look.ReadValue<Vector2>(); // 여기서 값을 읽어옴
            UpdateRotation(mouseDelta);

            rotation.x = Mathf.Lerp(rotation.x, currentRotation.x, 1 / damping);
            rotation.y = Mathf.Lerp(rotation.y, currentRotation.y, 1 / damping);

            transform.eulerAngles = new Vector3(rotation.y, rotation.x, 0);
        }
    }


    private void UpdateRotation(Vector2 delta)
    {
        currentMouseDelta = delta * sensitivity;

        currentRotation.x += currentMouseDelta.x;
        currentRotation.y -= currentMouseDelta.y;

        currentRotation.y = Mathf.Clamp(currentRotation.y, minYAngle, maxYAngle);
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
