using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PrintedHearts : MonoBehaviour
{
    PlayerStats _playerStats;
    int _playerHP;
    int _numberOfHearts;
    [SerializeField] int _renderedItems;
    [SerializeField] Vector3 firstPos;
    [SerializeField] float offsetPosition = 80;
    [SerializeField] GameObject ItemGraphics;
    List<GameObject> hearts = new List<GameObject>();
    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        firstPos.x -= offsetPosition;
        for (int i = 0; i < _renderedItems; i++)
        {
            var thisHeart = Instantiate(ItemGraphics, transform);
            firstPos.x += offsetPosition;
            thisHeart.transform.localPosition = firstPos;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            hearts.Add(transform.GetChild(i).gameObject);
            hearts[i].SetActive(false);
        }
        UpdatedHearts();
    }

    void UpdatedHearts()
    {
        for (int i = _playerStats.HP; i < hearts.Count; i++)
        {
            hearts[i].SetActive(false);
        }
        for (int i = 0; i < _playerStats.HP; i++)
        {
            hearts[i].SetActive(true);
            _numberOfHearts = _playerStats.HP;
        }
    }
    


    void Update()
    {
        if (_numberOfHearts != _playerStats.HP)
        {
            UpdatedHearts();
        }
    }
}
