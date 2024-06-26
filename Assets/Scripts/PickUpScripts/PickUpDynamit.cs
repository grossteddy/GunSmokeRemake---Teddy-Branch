using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDynamit : PickUpable
{
    [SerializeField] private PlayerDynamiteThrow _playerDynamiteThrow;
    void Start()
    {
        try
        {
            player = GameObject.Find("Player");
            audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

            _playerDynamiteThrow = player.GetComponent<PlayerDynamiteThrow>();
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    protected override void OnPickUp()
    {
        _playerDynamiteThrow.AddDynamit();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
