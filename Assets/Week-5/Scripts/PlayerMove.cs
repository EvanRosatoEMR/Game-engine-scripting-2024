using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float rotationVertical = 5.0f;
    [SerializeField] float rotationHorizontal = 5.0f;

    private float mouseDeltaX = 0f;
    private float mouseDeltaY = 0f;
    private float cameraRotX = 0f;
    private int rotDir = 0;

    PlayerControllerMappings playerMappings;

    InputAction move;
    InputAction fire;
    InputAction jump;
    InputAction look;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerMappings = new PlayerControllerMappings();
        move = playerMappings.Player.Move;
        fire = playerMappings.Player.Fire;
        jump = playerMappings.Player.Jump;
        look = playerMappings.Player.Look;

        fire.performed += Fire;
        jump.performed += Jump;
        look.performed += Look; 
    }

    private void OnEnable()
    {
        move.Enable();
        fire.Enable();
        jump.Enable();
        look.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
        look.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        //lastMouseX = Input.mousePosition.x;
    }

    private void Update()
    {
       HandleHorizontalRotation();
       HandleVerticalRotation();
    }

    void HandleHorizontalRotation()
    {
        mouseDeltaX = look.ReadValue<Vector2>().x;

        if (mouseDeltaX != 0)
        {
            rotDir = mouseDeltaX > 0 ? 1 : -1;

            transform.eulerAngles += new Vector3(0, rotationHorizontal * Time.deltaTime * rotDir, 0);
        }
    }

    void HandleVerticalRotation()
    {
        mouseDeltaY = look.ReadValue<Vector2>().y;

        if (mouseDeltaY != 0)
        {
            rotDir = mouseDeltaY > 0 ? 1 : -1;
            cameraRotX += rotationVertical * Time.deltaTime * rotDir;
            cameraRotX = Mathf.Clamp(cameraRotX, -45f, 45f);

            var targetRotation = Quaternion.Euler(Vector3.right * cameraRotX);

            Camera.main.transform.localRotation = targetRotation;
        }
    }

    private void FixedUpdate()
    {
        Vector2 input = move.ReadValue<Vector2>();
        Vector3 direction = (input.x * transform.right) + (transform.forward * input.y);

        transform.position = transform.position + (direction * speed * Time.deltaTime);
    }

    void Fire(InputAction.CallbackContext context)
    {
        //rb.AddForce(transform.up * jumpForce);
    }

    void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void Look(InputAction.CallbackContext context)
    {

    }

    /*void HandleMovement()
    {
        Vector3 input = (Input.GetAxis("Horizontal") * transform.right) + (transform.forward * Input.GetAxis("Vertical"));
        characterController.Move(input * speed * Time.deltaTime);
    }

    void HandleRotation()
    {
        mouseDeltaX = Input.mousePosition.x - lastMouseX;

        if (mouseDeltaX != 0)
        {
            rotDir = mouseDeltaX > 0 ? 1 : -1;
            lastMouseX = Input.mousePosition.x;

            transform.eulerAngles += new Vector3(0, rotation * Time.deltaTime * rotDir, 0);
        }
    }*/
}
