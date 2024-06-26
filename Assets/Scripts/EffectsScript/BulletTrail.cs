using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    Transform trailPool;
    void Update()
    {
        if(trailPool != null)
        {
            transform.position = trailPool.position;
        }
    }

    public void ConnectTransform(Transform transform)
    {
        trailPool = transform;
    }
}
