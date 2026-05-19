using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class testController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float jumpPower = 5;

    private CharacterController characterController;
    private Transform tm;
    private Vector3 moveVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        tm = transform;
    }

    void Update()
    {
        moveVelocity.x = 0f; moveVelocity.z = 0f;

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            moveVelocity.x += moveSpeed;
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            moveVelocity.x -= moveSpeed;
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            moveVelocity.z += moveSpeed;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            moveVelocity.z -= moveSpeed;
        }


        if (characterController.isGrounded)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("ジャンプ");
                moveVelocity.y = jumpPower;
            }
        }
        else
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        characterController.Move(moveVelocity * Time.deltaTime);
    }
}