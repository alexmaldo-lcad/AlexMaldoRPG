using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public GameObject projectilePrefab;

    public Transform projectileSpawnPosition;

    public float projectileForce = 1500f;

    // Start is called before the first frame update
    protected override void Start()
    {
        Debug.Log("I want to shoot you...");

        GameObject go = Instantiate(projectilePrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation);
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * projectileForce);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void EnemyTakeDamage(int damageTaken)
    {
        base.EnemyTakeDamage(damageTaken);
    }
}
