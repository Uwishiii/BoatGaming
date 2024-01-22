using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
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
    
    void Start()
    {
        healthUI.GetComponent<Slider>().value = maxHealth;
        health = maxHealth;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Destroy(gameObject);
            Debug.Log("ded");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageBoat"))
        {
            TakeDamage(damage);
            onHealthChange.Invoke(health, maxHealth);
            healthUI.GetComponent<Slider>().value = health;
        }
    }

    public void Repair()
    {
        health = maxHealth;
        healthUI.GetComponent<Slider>().value = health;
    }
}
