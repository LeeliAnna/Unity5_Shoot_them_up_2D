using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private const int LIVES = 3;
    private const string INPUT_SHOOT_ACTION = "Shoot";

    [SerializeField] private InputActionAsset actions;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Camera cam;
    [SerializeField] private float invulnerability = 1f;
    [SerializeField] private HeartPanel heartPanel;
    

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
    private bool isInvincible = false;
    private int currentLives = LIVES;

    private Bullet bullet;

    Vector2 half;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;



    void Awake()
    {
        move = actions.FindActionMap("Player").FindAction("Move");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cam = FindFirstObjectByType<Camera>();
        bullet = GetComponent<Bullet>();

        // récupération de la taille de la caméra
        rightLimit = cam.orthographicSize * cam.aspect;
        leftLimit = - rightLimit;
        topLimit = cam.orthographicSize;
        bottomLimit = - topLimit;

        // Recupération des bords du player
        spriteBounds = spriteRenderer.bounds;

        // Recuperation des limites du sprite et définition des position maximum en fonction de l'écran
        half = new Vector2(spriteBounds.extents.x, spriteBounds.extents.y);
        minX = leftLimit + half.x;
        maxX = rightLimit - half.x;
        minY = bottomLimit + half.y;
        maxY = topLimit - half.y;

        heartPanel.SetLives(currentLives);

        // rightBorder = cameraManager.GetRightBorderPoints(0);
        // leftBorder = cameraManager.GetLeftBorderPoints(0);
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
        actions.FindActionMap("Player").FindAction(INPUT_SHOOT_ACTION).performed += Shoot;
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
        actions.FindActionMap("Player").FindAction(INPUT_SHOOT_ACTION).performed -= Shoot;
    }

    void Update()
    {
        Move();
    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage();
        }
    }

    private void Move()
    {
        // Chercher dans bounds du spriteRenderer
        Vector3 position = transform.position;

        // Verification que le player se trouve bien dans les limites que j'ai définies et replace le player au besoin
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        
        transform.position = position;

        moveInput = actions["Move"].ReadValue<Vector2>();
    }

    private void TakeDamage()
    {
        Debug.Log($"Nombre de vies :  {currentLives}");
        currentLives--;
        heartPanel.SetLives(currentLives);
        StartCoroutine(Invincibility());
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;
        for (int i = 0; i < 6; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(invulnerability / 6f);
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        
    }

}
