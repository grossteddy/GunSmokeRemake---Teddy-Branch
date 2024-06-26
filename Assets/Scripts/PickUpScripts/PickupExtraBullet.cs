using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupExtraBullet : PickUpable
{
    [SerializeField] GameObject Revolver;
    [SerializeField] Animator anim;
    [SerializeField] Collider collider;
    [SerializeField] bool speed;
    [SerializeField] bool extraBullet;

    [SerializeField] GameObject Canvas;
    [SerializeField] string textString;
    [SerializeField] TMP_Text text;

    void Start()
    {
        text.text = textString;
    }

    protected override void OnPickUp()
    {
        if (extraBullet)
        {
            ExtraBullet();
        }
       else if (speed)
       {
           SpeedBullet();
       }
    }

    private void ExtraBullet()
    {
        Canvas.SetActive(true);
        anim.SetTrigger("Vanished");
        Revolver.GetComponent<Weapon>().ExtraBulletAmount(1);
        collider.enabled = false;
    }

    private void SpeedBullet()
    {
        Canvas.SetActive(true);
        anim.SetTrigger("Vanished");
        Revolver.GetComponent<Weapon>().SpeedUpBulletAmount(0.04f);
        collider.enabled = false;
    }

    void SetFalse()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    void SelfSetFalse()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
