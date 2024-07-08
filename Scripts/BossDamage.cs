using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
   [SerializeField]public HealthBar healthBar;
   //[SerializeField]public HealthBar shieldBar;
   public float maxHealth = 100.0f;
   //[SerializeField] private float maxShield;
   //[SerializeField] private GameObject shieldg;

    public float currentHealth;
    //public float currentShield;

   //private bool shield = true;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
      
    }

    void Update()
    {
        
    }
    public void TakeDamage(float damage){
     
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth<=0)
        {
            // gameObject.GetComponent<Scanner>().Die();
        }
    }
}