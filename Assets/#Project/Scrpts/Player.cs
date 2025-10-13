using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
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
    private Bounds spriteBounds;
    private float spriteBoundLeft;
    private float spriteBoundRight;
    private float spriteBoundTop;
    private float spriteBoundBottom;

    void Awake()
    {
        move = actions.FindActionMap("Player").FindAction("Move");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cam = FindFirstObjectByType<Camera>();

        // récupération de la taille de la caméra
        rightLimit = cam.orthographicSize * cam.aspect;
        leftLimit = -rightLimit;
        topLimit = cam.orthographicSize;
        bottomLimit = -topLimit;

        // Recupération des bords du player
        spriteBounds = spriteRenderer.bounds;
        // spriteBoundLeft = spriteBounds.center.x - spriteBounds.extents.x;
        // spriteBoundRight = spriteBounds.center.x + spriteBounds.extents.x;
        // spriteBoundTop = spriteBounds.center.y + spriteBounds.extents.y;
        // spriteBoundBottom = spriteBounds.center.y - spriteBounds.extents.y;
        

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
            Debug.Log(spriteBounds.size);
        // Chercher dans bounds du spriteRenderer
        Vector3 position = transform.position;
        if (position.x >= rightLimit)
        {
            Debug.Log(spriteBounds.size);
            Debug.Log(rightLimit - spriteBounds.size.x);
            //position.x = rightLimit;
            position.x = rightLimit - spriteBounds.size.x;
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
