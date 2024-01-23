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
    #endregion


    void Start()
    {
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
        else
        {
            boatIcon.sprite = boatIconNormal;
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
