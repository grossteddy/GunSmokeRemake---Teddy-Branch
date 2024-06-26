using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructAfterShock : MonoBehaviour
{
    [SerializeField] List<GameObject> pieces = new List<GameObject>();
    [SerializeField] float minForce;
    [SerializeField] float maxForce;
    [SerializeField] Vector3 addedForce;
    [SerializeField] bool randomScaling;
    [HideInInspector] public Vector3 dirPulse;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pieces.Add(transform.GetChild(i).gameObject);
        }
    }
    private void OnEnable()
    {
        if (randomScaling)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                pieces[i].transform.localScale = RandomVector3(0.5f, 2);
                pieces[i].GetComponent<Rigidbody>().AddForce(dirPulse * Random.Range(minForce, maxForce) + addedForce, ForceMode.Impulse);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Rigidbody rb = pieces[i].GetComponent<Rigidbody>();
                if(rb!= null)
                {
                    pieces[i].GetComponent<Rigidbody>().AddForce(dirPulse * Random.Range(minForce, maxForce) + addedForce, ForceMode.Impulse);
                }
            }
        }
    }

    Vector3 RandomVector3(float min, float max)
    {
        Vector3 RandomVector = new Vector3 (Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        return RandomVector;
    }
}
