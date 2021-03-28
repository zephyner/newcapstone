using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Target : MonoBehaviour
{
    public float health = 100f;

    //Used for the gun dealing damage to the target
    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Defeat();
        }
    }

    void Defeat()
    {
        Destroy(gameObject);
    }
}
