using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float rotationVertical = 5.0f;
    [SerializeField] float rotationHorizontal = 5.0f;

    [SerializeField] TextMeshProUGUI HealthCounter;
    [SerializeField] TextMeshProUGUI PillCounter;
    [SerializeField] TextMeshProUGUI KeyCounter;
    [SerializeField] TextMeshProUGUI DiedText;

    private float mouseDeltaX = 0f;
    private float mouseDeltaY = 0f;
    private float cameraRotX = 0f;
    private int rotDir = 0;

    private int health = 3;
    private int keys = 0;
    private int pills = 0;

    private bool keyTime;
    private bool keyDown;

    PlayerControllerMappings playerMappings;

    InputAction move;
    InputAction fire;
    InputAction jump;
    InputAction look;
    InputAction useKey;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerMappings = new PlayerControllerMappings();
        move = playerMappings.Player.Move;
        fire = playerMappings.Player.Fire;
        jump = playerMappings.Player.Jump;
        look = playerMappings.Player.Look;
        useKey = playerMappings.Player.UseKey;

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
        useKey.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
        look.Disable();
        useKey.Disable();
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

        keyDown = Input.GetKey(KeyCode.E);

        HealthCounter.text = "Health: " + health.ToString();
        PillCounter.text = "Pills: " + pills.ToString();
        KeyCounter.text = "Keys: " + keys.ToString();
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

    void UseKey (InputAction.CallbackContext context)
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Key")
        {
            keys++;
            KeyCounter.text = "Keys: " + keys.ToString();
        }

        if (collision.gameObject.name == "Trap")
        {
            TakeDamage();
        }

        if (collision.gameObject.name == "Pill")
        {
            pills++;
            PillCounter.text = "Pills: " + pills.ToString();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Door" && keyDown && keys > 0)
        {
            keys--;
            keyTime = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Door")
        {
            keyTime = false;
        }
    }

    public bool GiveKey()
    {
        return keyTime;
    }

    private void TakeDamage()
    {
        health--;
        HealthCounter.text = "Health: " + health.ToString();

        if (health <= 0)
        {
            //this.gameObject.SetActive(false);
            DiedText.gameObject.SetActive(true);

            move.Disable();
            fire.Disable();
            jump.Disable();
            look.Disable();
            useKey.Disable();
        }
    }

    public void reStart()
    {
        move.Enable();
        fire.Enable();
        jump.Enable();
        look.Enable();
        useKey.Enable();

        transform.position = new Vector3(0, 1.06f, -67.9f);
        transform.eulerAngles = new Vector3(0, 0, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(0, 0, 0);

        health = 3;
        HealthCounter.text = "Health: " + health.ToString();

        keys = 0;
        keyTime = false; ;
        KeyCounter.text = "Keys: " + keys.ToString();

        pills = 0;
        PillCounter.text = "Pills: " + pills.ToString();
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
