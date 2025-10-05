using UnityEngine;

public class MoveEnnemy : MonoBehaviour, IPoolClient
{
    //[SerializeField] private Camera cam;
    [SerializeField] private float moveSpeed = 5f;

    // Récupération du bord gauche de la caméra 
    private Vector3 leftBorder;
    private Vector3 rightBorder;

    private float maxHeight = 4f;
    private float minHeight = -4f;
    private float random;

    [HideInInspector] public SpawnSide sp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //leftBorder = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        //rightBorder = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        rightBorder = new Vector3(0, random = Random.Range(minHeight, maxHeight), 0);
        leftBorder = new Vector3(-20, random , 0);
        transform.position = rightBorder;
    }

    // Update is called once per frame
    void Update()
    {
        moveX();
        sp.transform.position = rightBorder;
    }

    private void moveX()
    {
        Vector3 position = transform.position;

        position.x -= moveSpeed * Time.deltaTime;

        if (position.x < leftBorder.x)
        {
            Teleport();
        }

        transform.position = position;

    }

    public void Arise(Vector3 position, Quaternion rotation)
    {
        rightBorder = new Vector3(0, random = Random.Range(minHeight, maxHeight), 0);
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
