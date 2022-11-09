using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int Health;
    public void TakeDamage(int dmg) 
    {
       Health--;
    }
}
