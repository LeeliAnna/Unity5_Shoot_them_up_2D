using UnityEngine;

public class MoveEnnemy : MonoBehaviour, IPoolClient
{
    private Camera cam;
    [SerializeField] private float moveSpeed = 5f;

    // R�cup�ration du bord gauche de la cam�ra 
    private Vector3 leftBorder;
    private Vector3 rightBorder;

    private float maxHeight = 4f;
    private float minHeight = -4f;
    private float random;

    private float rightLimit;
    private float leftLimit;
    private float topLimit;
    private float bottomLimit;

    [HideInInspector] public SpawnSide sp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = FindFirstObjectByType<Camera>();
        // récupération de la taille de la caméra
        rightLimit = cam.orthographicSize * cam.aspect;
        leftLimit = -rightLimit;
        topLimit = cam.orthographicSize;
        bottomLimit = -topLimit;
        //leftBorder = cam.ViewportToWorldPoint(new Vector3(-0.6f, random = Random.Range(minHeight, maxHeight), 0));
        //rightBorder = cam.ViewportToWorldPoint(new Vector3(0.5f, random, 0));
        //rightBorder = new Vector3(0, random = Random.Range(minHeight, maxHeight), 0);
        //leftBorder = new Vector3(-20, random , 0);
        transform.position = new Vector3(rightLimit, random = Random.Range(bottomLimit, topLimit), 0); ;
    }

    // Update is called once per frame
    void Update()
    {
        moveX();
        sp.transform.position = rightBorder;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Teleport();
        }
    }

    private void moveX()
    {
        Vector3 position = transform.position;

        position.x -= moveSpeed * Time.deltaTime;

        if (position.x < leftLimit)
        {
            Teleport();
        }

        transform.position = position;
    }

    public void Arise(Vector3 position, Quaternion rotation)
    {
        rightBorder = new Vector3(rightLimit, random = Random.Range(bottomLimit, topLimit), 0);
        transform.SetLocalPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
    }

    public void Fall()
    {
        gameObject.SetActive(false);
    }

    public void Teleport()
    {
        sp.Teleport(this);
    }
}
