using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


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
    //[SerializeField]
    //private Image boatIcon;
    //[SerializeField]
    //private Sprite boatIconDamaged;
    //[SerializeField]
    //private Sprite boatIconNormal;

    //[SerializeField]
    //private int maxRepairCount = 3;
    //private int repairCount = 0;
    [SerializeField]
    private int repairCooldown = 20;
    private float repairCooldownTimer = 20f;



    private float repairTimer = 0f;
    [SerializeField]
    private float timePerHealth = 0.2f;
    private bool repairing = false;

    private int healthRepaired = 0;
    private float toRepair = 0f;

    public float RepairCooldown { get => repairCooldownTimer; }
    //public int RepairCount { get => repairCount; }

    #endregion

    #region Andrei's Addition
    [SerializeField]
    private GameObject damageIcon;
    [SerializeField]
    private Material buttonMaterial;
    #endregion

    void Start()
    {
        repairCooldownTimer = repairCooldown;
        healthUI.GetComponent<Slider>().value = maxHealth;
        health = maxHealth;
        damageTaken = 0;
    }

    private void TakeDamage(int _damage)
    {
        #region Dylan T.'s Additions
        if (health <= 0)
            return;


        // Original code
        health -= _damage;



        health = Mathf.Clamp(health, 0, maxHealth);
        #endregion


        damageTaken += _damage;
        onHealthChange.Invoke(health, maxHealth);
        healthUI.GetComponent<Slider>().value = health;

        #region Dylan T.'s Addition
        // If the health is less than max health, change the boat icon to the damaged one.
        if (health < maxHealth)
        {
            //   boatIcon.sprite = boatIconDamaged;
            #region Andrei's Changes
            damageIcon.SetActive(true);
            buttonMaterial.EnableKeyword("_EMISSION");
            #endregion
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


        //if (repairCount < maxRepairCount)
        //{

        if (repairCooldownTimer <= 0)
        {
            repairing = true;
            repairCooldownTimer = repairCooldown;
            //repairCount++;
            toRepair = maxHealth - health;
            //health += 1;
            //healthUI.GetComponent<Slider>().value = health;

        }
        //}
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

                healthRepaired++;
                onHealthChange.Invoke(health, maxHealth);

                if (healthRepaired >= toRepair)
                {
                    if (health >= maxHealth)
                    {
                        damageIcon.SetActive(false);
                        buttonMaterial.DisableKeyword("_EMISSION");


                    }
                    // boatIcon.sprite = boatIconNormal;

                    #region Andrei's Changes
                    repairing = false;
                    healthRepaired = 0;
                    #endregion

                }
                else
                {
                    repairTimer += Time.deltaTime;
                }
            }
        }

        else if (repairCooldownTimer > 0)
        {
            repairCooldownTimer -= Time.deltaTime;
            repairCooldownTimer = Mathf.Clamp(repairCooldownTimer, 0, repairCooldown);
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
