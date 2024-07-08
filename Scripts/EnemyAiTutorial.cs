using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAiTutorial : MonoBehaviour

    {
    public NavMeshAgent ai;
    public float health;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public bool walking, chasing, attacking;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    public Vector3 rayCastOffset;
    public string deathScene;
    public float aiDistance;
    [SerializeField] Transform bulletSpawn;

    [SerializeField] float attackDistance;
    [SerializeField] private GameObject jumpScarecam;
    [SerializeField] private AudioSource scareSound;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource chaseSound;
    [SerializeField] private GameObject projectile;
    
    //public GameObject hideText, stopHideText;

    void Start()
    {
        walking = true;
        attacking=false;
        chasing = false;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        aiDistance = Vector3.Distance(player.position, this.transform.position);
        
        Debug.DrawRay(transform.position+ rayCastOffset,direction,Color.green);
        if(Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                chasing = true;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
            }
        }
        if(chasing == true && walking == false)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            // aiAnim.ResetTrigger("walk");
            // aiAnim.ResetTrigger("idle");
            // aiAnim.SetTrigger("sprint");
            if (!chaseSound.isPlaying){
            chaseSound.Play();
            walkSound.Stop();}

            if (aiDistance<=attackDistance)
            {
                Instantiate(projectile,bulletSpawn.position,Quaternion.identity);
                aiAnim.SetTrigger("attack");


            }
            // if (aiDistance <= catchDistance)
            // {
            //     player.gameObject.SetActive(false);
            //      aiAnim.ResetTrigger("walk");
            //      aiAnim.ResetTrigger("idle");
            //      hideText.SetActive(false);
            //      stopHideText.SetActive(false);
            //      aiAnim.ResetTrigger("sprint");
            //     jumpScarecam.SetActive(true);
            //     aiAnim.SetTrigger("jumpscare");
            //     scareSound.Play();
            //     chaseSound.Stop();
            //     walkSound.Stop();
            //     flashLight.SetActive(false);
            //     StartCoroutine(deathRoutine());
            //     chasing = false;
            // }
        }
        if((walking == true) && (chasing == false))
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            // aiAnim.ResetTrigger("sprint");
            // aiAnim.ResetTrigger("idle");
            // aiAnim.SetTrigger("walk");
             if (!walkSound.isPlaying){
            walkSound.Play();
            chaseSound.Stop();}
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                // aiAnim.ResetTrigger("sprint");
                // aiAnim.ResetTrigger("walk");
                // aiAnim.SetTrigger("idle");
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }
    public void stopChase()
    {
        walking = true;
        chasing = false;
        StopCoroutine("chaseRoutine");
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        stopChase();
    }
    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        
        SceneManager.LoadScene(deathScene);
    }
    public void TakeDamage(float dmg)
    {
        health-=dmg;
        if(health<=0)
        {
            Die();
        }
    }
    public void Die()
    {
        aiAnim.SetTrigger("Death");
        Destroy(gameObject,2f);
    }
}

