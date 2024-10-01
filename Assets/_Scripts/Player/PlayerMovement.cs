using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController _controller;
    [SerializeField] private float _speed;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void ClickPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameManager.Instance.GameStarted == GameState.FINISH) return;

            if(GameManager.Instance.GameStarted == GameState.STARTING) GameManager.Instance.StartGame();

            _controller.PlayerActive(true);
            Vector2 mousePos = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            transform.position = new Vector2(mousePos.x, transform.position.y);
        }
        else if (context.canceled)
        {
            _controller.PlayerActive(false);
        }
    }
}
