using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInputActions;
    [SerializeField] private Transform[] LaneTranforms;
    private Vector3 _destination;
    private int _currentLaneIndex;
    [SerializeField] private float moveSpeed = 2f;


    private void OnEnable()
    {
        playerInputActions = new PlayerInput();
        playerInputActions?.Enable();
        
    }

    private void Start()
    {
        playerInputActions.Gameplay.Move.performed += MovePerformed;

        for (int i = 0; i < LaneTranforms.Length; i++)
        {
            if (LaneTranforms[i].position == transform.position)
            {
                _currentLaneIndex = i;
                _destination = LaneTranforms[i].position;
            }
        }
    }

    void MovePerformed(InputAction.CallbackContext obj)
    {
        float InputValue = obj.ReadValue<float>(); 
        if(InputValue < 0f) { MoveLeft(); }
        else if(InputValue > 0f) { MoveRight(); }
    }

    private void MoveLeft()
    {
        if(_currentLaneIndex == 0)
        {
            return;
        }

        _currentLaneIndex--;
        _destination = LaneTranforms[_currentLaneIndex].position;
    }

    private void MoveRight()
    {
        if (_currentLaneIndex == LaneTranforms.Length - 1)
        {
            return;
        }

        _currentLaneIndex++;
        _destination = LaneTranforms[_currentLaneIndex].position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,_destination,moveSpeed * Time.deltaTime);
       
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
