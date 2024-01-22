using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class IcebergDamage : MonoBehaviour
{
    [SerializeField] 
    private float health;
    [SerializeField] 
    private int damage;
    [SerializeField] 
    private int healthSpeed;
    
    [SerializeField] private GameObject healthUI;
    
    void Start()
    {
        healthUI.GetComponent<Slider>().value = 20;
        health = 20f;
    }

    public void TakeDamage(int damage)
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
            healthUI.GetComponent<Slider>().value = health;
        }
    }
}
