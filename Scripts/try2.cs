using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class try2 : MonoBehaviour
{
    

    // Start is called before the first frame update
    public UnityEngine.AI.NavMeshAgent ai;
    public float health;
    public List<Transform> destinations;
    public Animator aiAnim;
    public bool walking, chasing, attacking, idle;
    public Transform player;
    public float aiDistance;
    public Vector3 rayCastOffset;
    Transform currentDest;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, chaseTime, minChaseTime, maxChaseTime, attackingDistance,attackTime;
    public Player3 playerBody;
    [SerializeField] private float bossDamage;
    Vector3 dest;
    private float remainingDistance;
    public float damage=10;

    void Start()
    {
        walking = true;
        chasing = false;
        attacking = false;
        idle = false;
        aiAnim.SetBool("dead",false);
        currentDest = destinations[Random.Range(0, destinations.Count)];
        //remainingDistance=Vector3.Distance(transform.position,currentDest.position);
      
    }

    // Update is called once per frame
    void Update()
    
    {
        
        aiAnim.SetBool("isWalking", walking);

        aiAnim.SetBool("isRunning", chasing);

        aiAnim.SetBool("isAttacking", attacking);

        aiAnim.SetBool("isIdle", idle);
        

        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        aiDistance = Vector3.Distance(player.position, this.transform.position);

        Debug.DrawRay(transform.position + rayCastOffset, direction*sightDistance, Color.green);
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            //Debug.Log(" FOUND");
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("PlayerFound");
                if(!attacking) 
                    runn();
            }
            else if(!idle)
            {
            walk();
            }
        }
        else if(!idle)
        {
            walk();
        }
        


    }
    void runn()
    {
        Debug.Log("NOW RUNNING/ATTACKING");
        dest = player.position;
        ai.destination = dest;
        ai.speed = chaseSpeed;
        if (aiDistance <= attackingDistance )
        {

            //attack
        playerBody.TakeDamage(damage);
        walking = false;
        chasing = false;
        attacking = true;
        //idle = false;
        StopCoroutine("stayIdle");
        StopCoroutine("chaseRoutine");
        StopCoroutine("Attack");
        StopCoroutine("chaseRoutine");
        stopChase();
        }
        else
        {
            //run
        walking = false;
        chasing = true;
        attacking = false;
        idle = false;
        StopCoroutine("stayIdle");
        StopCoroutine("chaseRoutine");
        
        StopCoroutine("Attack");

        StartCoroutine("chaseRoutine");

        }
    }
   
    void walk()
    {
        
        
        dest = currentDest.position;
        ai.destination = dest;
        ai.speed = walkSpeed;
        
        
        if (ai.remainingDistance <= ai.stoppingDistance)
            {
                Debug.Log("NOW STOPPING");
                walking=false;
                attacking=false;
                chasing=false;
                idle=true;
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                
                
                
            }
            // else
            // {
            //     walking=true;
            // }

    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        ai.isStopped = true;
        aiAnim.SetBool("dead", true);
        Destroy(gameObject,5);
        //Invoke("sceneTransit", 4f);
    }
    void sceneTransit()
    {
        SceneManager.LoadScene("youWin");
    }
    public void stopChase()
    {
        walking = false;
        attacking = true;
        chasing = false;
        //StopCoroutine("chaseRoutine");
        //currentDest = destinations[Random.Range(0, destinations.Count)];
        StartCoroutine("Attack");
    }


    IEnumerator stayIdle()
    {
        //play the animation
        
        // idleTime = Random.Range(minIdleTime, maxIdleTime);
        Debug.Log("Idle Start");
        currentDest = destinations[Random.Range(0, destinations.Count)];

        yield return new WaitForSeconds(3);
        Debug.Log("Idle Stoppped");
        idle=false;
        walking = true;
        attacking=false;
        
        
        
        
        
       
    }
    IEnumerator Attack()
    {
        //play the animation
        Debug.Log("ATTACKING");
        //playerBody.TakeDamage(bossDamage);
        yield return new WaitForSeconds(attackTime);
        Debug.Log("IDLE kotrue kr rhe h ");
        idle=true;
        
        StartCoroutine("stayIdle");
    }
    IEnumerator chaseRoutine()
    {
        //play the animation

        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        stopChase();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player3>().TakeDamage(damage);
        }
    }
}

