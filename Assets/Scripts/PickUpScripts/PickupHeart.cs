using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeart : PickUpable
{
    protected override void OnPickUp()
    {
        player.GetComponent<PlayerStats>().HP += 1;
        gameObject.SetActive(false);
    }

}
