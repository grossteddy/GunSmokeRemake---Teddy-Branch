using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRotate : MonoBehaviour
{
    [SerializeField] Vector3 _rotateDirection;
    [SerializeField] private bool selfOrWorlds = false;
    private void Update()
    {
        if (!selfOrWorlds)
        {
            transform.Rotate(_rotateDirection * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Rotate(_rotateDirection * Time.deltaTime, Space.World);

        }
    }

}
