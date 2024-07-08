using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private int dmg = 10;
 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log("------------------------------------------------------------------------------------------");
        if(other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<try2>().TakeDamage(dmg);            
            Destroy(gameObject);
        }
        // if (other.CompareTag("Player"))
        // {
        //     other.gameObject.GetComponent<Player3>().TakeDamage(dmg);
        //     Destroy(gameObject);
        // }
        if(other.CompareTag("Environment")){
            Destroy(gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossDamage>().TakeDamage(dmg);
            Destroy(gameObject);
        }
        if (other.CompareTag("Explosives"))
        {
            Debug.Log("----------------Pta NHi----------------");
            other.gameObject.GetComponent<BombDamage>().TakeDamage(dmg);
            Destroy(gameObject);
        }
        if (other.CompareTag("Spawner"))
        {
            other.gameObject.GetComponent<SpawnEnemies>().TakeDamage(dmg);
            Destroy(gameObject);
        }

}}