using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera cam;


    private Vector3 leftBorder;
    private Vector3 rightBorder;
    private float random;
    private float maxHeight = 4f;
    private float minHeight = -4f;


    void Start()
    {
        cam = FindFirstObjectByType<Camera>();
        leftBorder = cam.ViewportToWorldPoint(new Vector3(-0.6f, random = Random.Range(minHeight, maxHeight), 0));
        rightBorder = cam.ViewportToWorldPoint(new Vector3(0.5f, random, 0));
    }

    public (Vector3, Vector3) GetRightBorderPoints(float z)
    {
        Vector3 top = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, z));
        Vector3 bottom = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, z));
        return (bottom, top);
    }

    public (Vector3, Vector3) GetLeftBorderPoints(float z)
    {
        Vector3 top = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height,z));
        Vector3 bottom = cam.ScreenToWorldPoint(new Vector3(0f, 0f, z));
        return (bottom, top);
    }




}
