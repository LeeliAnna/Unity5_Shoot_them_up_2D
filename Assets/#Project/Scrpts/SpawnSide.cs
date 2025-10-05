using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnSide : MonoBehaviour
{
    [SerializeField] private float cooldown = 0.2f;
    [SerializeField] private GameObject prefab;

    private Pool<MoveEnnemy> pool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool = new(transform, prefab, 2);
        StartCoroutine(Spawn());   
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            MoveEnnemy ennemy = pool.Get();
            ennemy.sp = this;
            
        }
    }

    public void Teleport(MoveEnnemy ennemy)
    {
        pool.Add(ennemy);
    }
}
