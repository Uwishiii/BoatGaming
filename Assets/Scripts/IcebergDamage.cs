using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Slider = UnityEngine.UI.Slider;
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
    private int healthSpeed;
    [SerializeField] 
    private GameObject healthUI;
    [SerializeField]
    private UnityEvent<float, float> onHealthChange;


    #region Dylan T.'s Addition
    [SerializeField]
    private Image boatIcon;
    [SerializeField]
    private Sprite boatIconDamaged;
    [SerializeField]
    private Sprite boatIconNormal;
    #endregion


    void Start()
    {
        healthUI.GetComponent<Slider>().value = maxHealth;
        health = maxHealth;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        //if (health <= 0f)
        //{
        //    Destroy(gameObject);
        //    Debug.Log("ded");
        //}


        #region Dylan T.'s Addition
        // If the health is less than max health, change the boat icon to the damaged one.
        if (health < maxHealth)
        {
            boatIcon.sprite = boatIconDamaged;
        }
        else
        {
            boatIcon.sprite = boatIconNormal;
        }
        #endregion


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageBoat"))
        {
            TakeDamage(damage);
            // Dylan T.'s suggestion: Refactor this to a method or move it to the TakeDamage method.
            onHealthChange.Invoke(health, maxHealth);
            healthUI.GetComponent<Slider>().value = health;
        }
    }

    public void Repair()
    {
        health = maxHealth;
        healthUI.GetComponent<Slider>().value = health;
    }
    #region Dylan T.'s Addition

    // This is a Unity event that is called when the script is loaded or a value is changed in the inspector.
    // This allows me to see the effects of the health on the speed.
    private void OnValidate()
    {
        onHealthChange.Invoke(health, maxHealth);
        
    }
    #endregion

}
