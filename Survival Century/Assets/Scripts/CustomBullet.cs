using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask theTarget;

    //Bounciness of the bullet
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    public int Damage;
    public float explosionRange;

    //Life of the bullet
    public int maxCollisions;
    //public float maxLifetime;
    public bool explodeOnImpact = true;

    int collisions;
    PhysicMaterial phys_mat;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        //creates the physic material
        phys_mat = new PhysicMaterial();
        phys_mat.bounciness = bounciness;
        phys_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        phys_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        //puts material to the collider
        GetComponent<SphereCollider>().material = phys_mat;

        //Sets the gravity
        rb.useGravity = useGravity;
    }

    void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, theTarget);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().TakeDamage(Damage);
            enemies[i].GetComponent<PlayerController>();
        }

        Invoke("Delay", 0.05f);
    }

    void Delay()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        collisions++;

        if (collision.collider.CompareTag("Enemy") && explodeOnImpact) Explode();
        if (collision.collider.CompareTag("Player") && explodeOnImpact) Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (collisions > maxCollisions) Explode();

        //maxLifetime -= Time.deltaTime;
        //if (maxLifetime <= 0) Explode();
    }
}
