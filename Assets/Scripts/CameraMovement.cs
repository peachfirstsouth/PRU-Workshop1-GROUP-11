using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class CameraMovement : MonoBehaviour
{
    Vector2 movementAxis;
    Vector2 lookAxis;
    public CharacterController characterController;

    bool mouseLock;

    public float sensivity = 5f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ToggleMouse()
    {
        Cursor.lockState = mouseLock ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = mouseLock ? true : false;
        mouseLock = !mouseLock;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementAxis = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookAxis = context.ReadValue<Vector2>();
    }

    public void Update()
    {
        float mouseX = lookAxis.x * sensivity * Time.deltaTime;
        float mouseY = lookAxis.y * sensivity * Time.deltaTime;

        float xAxis = -mouseY;
        xAxis = Mathf.Clamp(xAxis, -90f, 90f);

        this.transform.rotation *= Quaternion.Euler(xAxis, 0f, 0f);
        this.gameObject.transform.parent.gameObject.transform.rotation *= Quaternion.Euler(0f, mouseX, 0f);

        if (Keyboard.current.escapeKey.isPressed)
        {
            ToggleMouse();
        }
    }

    public void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * movementAxis.y + transform.right * movementAxis.x;
        characterController.Move(moveDirection * 50f * Time.deltaTime);
        
    }
}
