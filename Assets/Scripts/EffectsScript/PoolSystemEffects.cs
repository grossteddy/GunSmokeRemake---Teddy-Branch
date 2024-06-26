using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystemEffects : MonoBehaviour
{
    [SerializeField] GameObject PoolPrefab;
    [SerializeField] List<GameObject> Pool = new List<GameObject>();
    [SerializeField] int numberOfClones;
    Vector3 vectorScale;

    private void Start()
    {
        vectorScale = PoolPrefab.transform.localScale;
        for (int i = 0; i < numberOfClones; i++)
        {
            var PoolUnit = Instantiate(PoolPrefab, transform);
            Pool.Add(PoolUnit);
            PoolUnit.SetActive(false);
        }
    }
    public void CallTrailObject(Vector3 position, float scale, Transform transform)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (!Pool[i].activeInHierarchy)
            {
                Pool[i].transform.position = position;
                Pool[i].transform.localScale = vectorScale * scale;
                Pool[i].GetComponent<BulletTrail>().ConnectTransform(transform);
                Pool[i].SetActive(true);
                Pool[i].GetComponent<ParticleSystem>().Play();
                break;
            }
            var PoolUnit = Instantiate(PoolPrefab, this.transform);
            Pool.Add(PoolUnit);
            PoolUnit.transform.position = position;
            PoolUnit.transform.localScale = vectorScale * scale;
        }
    }
    public void CallObject(Vector3 position, float scale)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (!Pool[i].activeInHierarchy)
            {
                Pool[i].transform.position = position;
                Pool[i].transform.localScale = vectorScale * scale;
                Pool[i].SetActive(true);
                break;
            }
            var PoolUnit = Instantiate(PoolPrefab, transform);
            Pool.Add(PoolUnit);
            PoolUnit.transform.position = position;
            PoolUnit.transform.localScale = vectorScale * scale;
        }
    }
    public void CallObjectWithDir(Vector3 position, Vector3 direction, Material material)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (!Pool[i].activeInHierarchy)
            {
                Pool[i].transform.position = position;
                DestructAfterShock destructAfterShock = Pool[i].GetComponent<DestructAfterShock>();
                destructAfterShock.dirPulse = direction;
                Pool[i].SetActive(true);
                break;
            }
            var PoolUnit = Instantiate(PoolPrefab, transform);
            Pool.Add(PoolUnit);
            PoolUnit.transform.position = position;
        }
    }
}
