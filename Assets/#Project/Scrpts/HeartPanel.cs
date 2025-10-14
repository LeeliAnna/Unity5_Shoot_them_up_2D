using System;
using UnityEngine;

public class HeartPanel : MonoBehaviour
{
    private int lives;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform container;

    private void Start()
    {
        BuildHearts(lives);
    }

    private void BuildHearts(int count)
    {
        foreach (Transform item in container)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject heart = Instantiate(heartPrefab, container);
            heart.transform.localPosition = new Vector3(i * -60, 0, 0);
        }
    }

    public void SetLives(int newLives)
    {
        if (newLives >= 0)
        {
            lives = newLives;
            BuildHearts(lives);
        }
    }
}
