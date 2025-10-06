using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Camera cam;
    

    private InputAction move;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CameraManager cameraManager;
    private (Vector3, Vector3) rightBorder;
    private (Vector3, Vector3) leftBorder;
    private float rightLimit;
    private float leftLimit;
    private float topLimit;
    private float bottomLimit;

    void Awake()
    {
        move = actions.FindActionMap("Player").FindAction("Move");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cam = FindFirstObjectByType<Camera>();

        rightLimit = cam.orthographicSize * cam.aspect;
        leftLimit = -rightLimit;
        topLimit = cam.orthographicSize;
        bottomLimit = -topLimit;

        // rightBorder = cameraManager.GetRightBorderPoints(0);
        // leftBorder = cameraManager.GetLeftBorderPoints(0);
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Chercher dans bounds du spriteRenderer
        Vector3 position = transform.position;
        if (position.x >= rightLimit)
        {
            position.x = rightLimit;
        }
        else if (position.y >= topLimit)
        {
            position.y = topLimit;
        }
        else if (position.x <= leftLimit)
        {
            position.x = leftLimit;
        }
        else if (position.y <= bottomLimit)
        {
            position.y = bottomLimit;
        }
        transform.position = position;

        moveInput = actions["Move"].ReadValue<Vector2>();
        //transform.Translate(move.ReadValue<float>() * speed * Time.deltaTime, 0f, 0f);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed * Time.deltaTime;
    }

}
