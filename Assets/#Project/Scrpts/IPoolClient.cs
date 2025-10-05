using UnityEngine;

public interface IPoolClient 
{
    void Arise(Vector3 position, Quaternion rotation);
    void Fall();
}
