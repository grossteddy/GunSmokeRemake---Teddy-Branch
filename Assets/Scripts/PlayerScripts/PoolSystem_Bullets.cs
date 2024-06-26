using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem_Bullets : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float _numberOfBullets;
    [SerializeField] List<GameObject> BulletsPool;

    void Start()
    {
        //Instantiate all pool object as childrens
        for (int i = 0; i < _numberOfBullets; i++)
        {
            BulletsPool.Add(Instantiate(Bullet));
            BulletsPool[i].transform.SetParent(transform);
            BulletsPool[i].SetActive(false);
        }
    }

    //The function to use when calling the bullet from another script
    public GameObject PullBullet()
    {
        for (int i = 0; i < BulletsPool.Count - 1; i++)
        {
            if (!BulletsPool[i].activeInHierarchy)
            {
                BulletsPool[i].SetActive(true);
                return BulletsPool[i];
            }
            
        }
        BulletsPool.Add(Instantiate(Bullet));
        BulletsPool[BulletsPool.Count -1].transform.SetParent(transform);
        BulletsPool[BulletsPool.Count -1].SetActive(true);
        return BulletsPool[BulletsPool.Count - 1];
    }

}
