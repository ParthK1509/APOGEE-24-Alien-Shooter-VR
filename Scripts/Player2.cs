using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player2 : MonoBehaviour
{
public float Health;

public void TakeDamage(float dmg)
{
    Health-=dmg;
    if (Health<=0)
    {
        SceneManager.LoadScene("gameOver");
    }
}
}