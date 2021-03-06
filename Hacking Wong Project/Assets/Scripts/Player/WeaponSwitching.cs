﻿using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon;
    public Transform crossHair;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = 0;
        SelectWeapon();
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurt"))
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else selectedWeapon--;
            SelectWeapon();
        } else if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            } else selectedWeapon++;
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                if (weapon.gameObject.tag == "Gun")
                {
                    crossHair.gameObject.SetActive(true);
                }
                else crossHair.gameObject.SetActive(false);
            } else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
