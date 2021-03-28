using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject firePos;
    public ParticleSystem muzzleFlash;

    //
    void Shoot()
    {
        //stores information about what the ray hits
        RaycastHit hit;
        muzzleFlash.Play();

        //This shoots our bullets
       if (Physics.Raycast(firePos.transform.position, firePos.transform.forward, out hit, range))
       {
            Debug.Log(hit.transform.name);

            //Has the gun deal damage to the test targets
            Test_Target target = hit.transform.GetComponent<Test_Target>();
            if (target != null)
            {
                target.Damage(damage);
            }
       }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
}
