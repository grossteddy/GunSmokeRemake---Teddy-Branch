using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class PrintedDynamit : MonoBehaviour
{
    [SerializeField] PlayerDynamiteThrow _playerDynamit;
    [SerializeField] private TMP_Text text;
    int _playerDynamitInt;
    int _numberOfDynamits;
    [SerializeField] int _renderedItems;
    [SerializeField] Vector3 firstPos;
    [SerializeField] float offsetPosition = 80;
    [SerializeField] GameObject ItemGraphics;
    List<GameObject> dyanamits = new List<GameObject>();
    void Start()
    {
        /*firstPos.x -= offsetPosition;
        for (int i = 0; i < _renderedItems; i++)
        {
            var thisDynamit = Instantiate(ItemGraphics, transform);
            firstPos.x += offsetPosition;
            thisDynamit.transform.localPosition = firstPos;
        }
        
        for (int i = 0; i < transform.childCount; i++)
        {
            dyanamits.Add(transform.GetChild(i).gameObject);
            dyanamits[i].SetActive(false);
        }
        UpdatedDynamit();*/
    }

    void UpdatedDynamit()
    {
        /*for (int i = _playerDynamit.numOfDynamite; i < dyanamits.Count; i++)
        {
            dyanamits[i].SetActive(false);
        }
        for (int i = 0; i < _playerDynamit.numOfDynamite; i++)
        {
            dyanamits[i].SetActive(true);
            _numberOfDynamits = _playerDynamit.numOfDynamite;
        }*/
    }
    


    void Update()
    {
        text.text = _playerDynamit.numOfDynamite.ToString();
        /*if (_numberOfDynamits != _playerDynamit.numOfDynamite)
        {
            UpdatedDynamit();
        }*/
    }
}
