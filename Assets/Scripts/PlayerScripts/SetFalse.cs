using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFalse : MonoBehaviour
{
    [SerializeField] float timeToSetFalse = 8;
    float timer;
    [SerializeField] bool returnToPlayerPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToSetFalse)
        {
            timer = 0;
            if (returnToPlayerPos)
            {
                transform.position = Vector3.zero;
            }
            gameObject.SetActive(false);
        }
    }
}
