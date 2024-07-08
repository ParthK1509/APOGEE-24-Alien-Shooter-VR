using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    
    // Start is called before the first frame update
    public UnityEngine.AI.NavMeshAgent ai;
    public float health;
    public List<Transform> destinations;
    public Animator aiAnim;
    public bool walking, chasing, attacking,idle;
    [SerializeField] private Transform player;
    public float aiDistance;
    public Vector3 rayCastOffset;
    Transform currentDest;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime,attackingDistance;

    void Start()
    {
        walking=true;
        chasing=false;
        attacking=false;
        idle=false;
    }

    // Update is called once per frame
    void Update()
    {
        aiAnim.SetBool("isWalking",walking);
        
        aiAnim.SetBool("isRunning",chasing);
        
        aiAnim.SetBool("isAttacking",attacking);
        
        aiAnim.SetBool("isIdle",idle);

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
                attacking = false;
                idle=false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
            if (aiDistance<=attackingDistance)
            {
                walking = false;
                chasing = false;
                attacking = true;
                idle=false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StopCoroutine("st");
                StopCoroutine("chaseRoutine");

                StartCoroutine("Attack");
            }
            }
        }


    }

    public void TakeDamage(float dmg)
    {
    health-=dmg;
    if (health<=0)
    {
        Die();
    }
    }
    public void Die()
    {
        aiAnim.SetBool("Die",true);
        
        Invoke("sceneTransit",4f);
    }
    void sceneTransit()
    {
        SceneManager.LoadScene("youWin");
    }
    public void stopChase()
    {
        walking = false;
        attacking=true;
        idle=false;
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
}
