using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using Slider = UnityEngine.UI.Slider;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class IcebergDamage : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField] 
    private float health;
    [SerializeField] 
    private int damage;
    [SerializeField] 
    private GameObject healthUI;
    [SerializeField]
    private UnityEvent<float, float> onHealthChange;

    private int damageTaken;


    #region Dylan T.'s Addition
    [SerializeField]
    private Image boatIcon;
    [SerializeField]
    private Sprite boatIconDamaged;
    [SerializeField]
    private Sprite boatIconNormal;

    [SerializeField]
    private int maxRepairCount = 3;
    private int repairCount = 0;
    [SerializeField]
    private float repairCooldown = 20f;
    private float repairTimer = 0f;
    [SerializeField]
    private float timePerHealth = 0.2f;
    private bool repairing = false;

    private int healthRepaired = 0;
    private float toRepair = 0f;

    #endregion


    void Start()
    {
        repairTimer = repairCooldown;
        healthUI.GetComponent<Slider>().value = maxHealth;
        health = maxHealth;
        damageTaken = 0;
    }

    private void TakeDamage(int _damage)
    {
        health -= _damage;
        damageTaken += _damage;
        onHealthChange.Invoke(health, maxHealth);
        healthUI.GetComponent<Slider>().value = health;

        #region Dylan T.'s Addition
        // If the health is less than max health, change the boat icon to the damaged one.
        if (health < maxHealth)
        {
            boatIcon.sprite = boatIconDamaged;
        }
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        //This is for Damage Calcs
        if (collision.gameObject.CompareTag("DamageBoat"))
        {
            TakeDamage(damage);
        }
        
        //This is for detecting the finish line
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            ScoreCalc();
        }
    }




    // Dylan W's Code
    //public void Repair()
    //{
    //    health = maxHealth;
    //    healthUI.GetComponent<Slider>().value = health;
    //}

    #region Dylan T.'s Addition


    private void FixedUpdate()
    {
        RepairUpdate(toRepair);

    }

    // This is a Unity event that is called when the script is loaded or a value is changed in the inspector.
    // This allows me to see the effects of the health on the speed.
    private void OnValidate()
    {
        onHealthChange.Invoke(health, maxHealth);
        
    }
    public void Repair()
    {
        if (repairing)
            return;


        if (repairCount < maxRepairCount)
        {
            
            if (repairTimer >= repairCooldown)
            {
                repairing = true;
                repairTimer = 0f;
                repairCount++;
                toRepair = maxHealth - health;
                //health += 1;
                //healthUI.GetComponent<Slider>().value = health;
                Debug.Log("Repairing");
            }
        }
    }

    public void RepairUpdate(float toRepair)
    {
        if (repairing)
        {
            repairTimer += Time.deltaTime;
            if (repairTimer >= timePerHealth)
            {
                repairTimer = 0f;
                health += 1;
                healthUI.GetComponent<Slider>().value = health;
                Debug.Log("Repairing");
                Debug.Log($"Health: {health}, Max Health: {maxHealth}, To Repair: {toRepair}, Health Repaired: {healthRepaired}");
                healthRepaired++;
                onHealthChange.Invoke(health, maxHealth);

                if (healthRepaired >= toRepair)
                {
                    if (health >= maxHealth)
                    boatIcon.sprite = boatIconNormal;

                    repairing = false;
                    healthRepaired = 0;

                }
            }
        }
        else if (repairCount < maxRepairCount)
        {
            repairTimer += Time.deltaTime;

        }
    }

    #endregion



    private void ScoreCalc()
    {
        switch (damageTaken)
        {
            case 0:
                Debug.Log("S"); 
                break;
            
            case > 0 and <= 12:
                Debug.Log("A");
                break;
            
            case > 12 and <= 24:
                Debug.Log("B");
                break;
            
            case > 24 and <= 36:
                Debug.Log("C");
                break;
            
            case > 36 and <= 48:
                Debug.Log("D");
                break;
            
            case > 48:
                Debug.Log("F");
                break;
        }
    }


}
