using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int Explosiondmg;
    [SerializeField] private float health;
    [SerializeField] private float Explosionradius;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private bool isExploded;
    //public float dmg;
  
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders= Physics.OverlapSphere(center,radius);
        foreach(var hitCollider in hitColliders)
        {
            if(hitCollider.CompareTag("Enemy"))
            {
                hitCollider.gameObject.GetComponent<try2>().TakeDamage(Explosiondmg);
            }
            if (hitCollider.CompareTag("Explosives"))
            {
                hitCollider.gameObject.GetComponent<BombDamage>().TakeDamage(Explosiondmg);
            }
             if (hitCollider.CompareTag("Player"))
            {
                hitCollider.gameObject.GetComponent<Player3>().TakeDamage(Explosiondmg);
            }
        }
    }
    void Start()
    {
        isExploded=false;
        // if (health<=0 && !isExploded)  
        // {
        //     isExploded=true;
        //     Invoke(nameof(Explode), 0.5f);
            
        // }

    }
    public void TakeDamage(float damage)
    {
        health-=damage;
        if (health<=0 && !isExploded) 
        {
            isExploded=true;
            Invoke(nameof(Explode), 0.5f);
            
        }
    }
    public void Explode()
    {
        Debug.Log("------------------Helth<0 now exploding----------------");
        GameObject newObject = Instantiate(explosionPrefab,transform.position,Quaternion.identity);
        ExplosionDamage(transform.position,Explosionradius);
        Destroy(gameObject);
        

        
        
    }
    
    // void OnCollisionEnter(Collision collision)

    // {
    //     if (collision.gameObject.CompareTag("Bullet"))
    //     {
    //         TakeDamage(dmg);
    //     }
    // }
}
