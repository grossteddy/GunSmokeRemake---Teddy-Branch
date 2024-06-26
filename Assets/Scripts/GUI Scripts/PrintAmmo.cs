using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PrintAmmo : MonoBehaviour
{
    [SerializeField] TMP_Text Ammo;
    [SerializeField] PlayerFire _playerFire;
    [SerializeField] GameObject[] _weaponsGUI;
    int currentWeapon = 99;

    private void Start()
    {
        ChangeWeaponGUI();
    }
    void Update()
    {
        PrintedAmmo();
        ChangeWeaponGUI();
    }

    private void PrintedAmmo()
    {
        Ammo.text = ($"{_playerFire.GetAmmo()}");
    }

    private void ChangeWeaponGUI()
    {
        if (currentWeapon != _playerFire.chooseWeapon)
        {
            currentWeapon = _playerFire.chooseWeapon;
            for (int i = 0; i < _weaponsGUI.Length; i++)
            {
                _weaponsGUI[i].SetActive(false);
            }
            _weaponsGUI[currentWeapon].SetActive(true);
        }
    }
}
