using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player3 : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health;
    

public void TakeDamage(float dmg)
{
    Debug.Log("PLAYER TOOK DAMAGE");
    Health-=dmg;
    if (Health<=0)
    {
        SceneManager.LoadScene("YouLose");
    }
}
}
