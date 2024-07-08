using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject enemy;
    [SerializeField] private float health;
    private float spawnGap;
    public List<Transform> enemySpawn;

    public List<Transform> destinations;
    public int range;
    public Transform player;
    public Player3 playerBody;

    try2 try2Script;

    // Update is called once per frame
    void Start()
    {
        
        StartCoroutine("spawn");
    }
    public void TakeDamage(float dmg)
    {
        health-=dmg;
        if (health<=0)
        {
            Die();
        }
    }
    void Die()
    {
        SceneManager.LoadScene("YouWin");
        Destroy(gameObject);
    }
    IEnumerator spawn()
    {
        while(true)
        {
            Debug.Log("SPAWNING ENEMIES");
            range=Random.Range(0,enemySpawn.Count);
            spawnGap=Random.Range(7,10);
            yield return new WaitForSeconds(spawnGap);
            GameObject spawnedEnemy = Instantiate(enemy,enemySpawn[range].position,Quaternion.identity);
            try2Script=spawnedEnemy.GetComponent<try2>();
            try2Script.destinations = destinations;
            try2Script.playerBody=playerBody;
            try2Script.player=player;
            


        }
    }
}
