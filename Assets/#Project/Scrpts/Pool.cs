using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
where T : IPoolClient
{
    private Transform anchor;
    private GameObject prefab;
    private Queue<T> queue = new();
    private int batch;

    public Pool(Transform anchor, GameObject prefab, int batch)
    {
        this.anchor = anchor;
        this.prefab = prefab;
        this.batch = batch;

        CreateBatch();
    }

    private void CreateBatch()
    {
        for (int _ = 0; _ < batch; _++)
        {
            GameObject ennemi = GameObject.Instantiate(prefab);
            if(ennemi.TryGetComponent(out T client))
            {
                Add(client);
            }
            else
            {
                throw new ArgumentException("Ce préfab n'est pas un IPoolClient.");
            }
        }
    }

    public void Add(T client)
    {
        queue.Enqueue(client);
        client.Fall();
    }

    public T Get()
    {

        if (queue.Count == 0) CreateBatch();
        T client = queue.Dequeue();
        client.Arise(anchor.position, anchor.rotation);
        return client;
    }


}
