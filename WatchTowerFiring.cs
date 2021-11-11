using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTowerFiring : MonoBehaviour//watchtower behavior to make it fire a nearby enemies
{
    public GameObject Projectile, closestEnemy;
    public Transform ProjectileSpawn;
    public int Health;
    public float AttackSpeed, nextHit;
    public GameObject[] Enemies;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //create an array of enemies and check there distances
        //if the enemy is in range then fire at the enemy
        //if the watchtower takes to much damage then it will be destroyed
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float enemyDistance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in Enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < enemyDistance && enemy.GetComponent<EnemyController>().isTarget == false)
            {
                closestEnemy = enemy;
                enemyDistance = curDistance;
            }
        }
        if (closestEnemy != null)
        {
            ProjectileSpawn.transform.LookAt(closestEnemy.transform);
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)//fire at enemies while in range, fires at the closest target
    {
        if (other.tag == "Enemy")
        {
            if (Time.time > nextHit)
            {
                Debug.Log("fired");
                Instantiate(Projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                nextHit = Time.time + AttackSpeed;
            }
        }
    }
}
