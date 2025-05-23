using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRPG : MonoBehaviour
{
    public float health = 100f;
    public float attackDamage = 5f;
    public float attackInterval = 1f;

    private float timer;
    private bool isAttackReady = true;

    public Image attackReadyImage;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPosition;
    public float projectileForce = 800;
    protected BaseEnemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttackReady == false)
        {
            timer += Time.deltaTime;

            if (timer >= attackInterval)
            {
                isAttackReady = true;
                attackReadyImage.gameObject.SetActive(isAttackReady);
                timer = 0f;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(isAttackReady == true)
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
                {
                    BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();

                    if (enemy != null)
                    {
                        Attack(enemy);
                    }
                }
            }
        }
    }

    public void Attack(BaseEnemy enemy)
    {
       // enemy.EnemyTakeDamage(attackDamage); //idk why this isnt working here but works for enemy
        isAttackReady = false;
        attackReadyImage.gameObject.SetActive(isAttackReady);
    }

    public void Shoot()
    {
        GameObject go = Instantiate(projectilePrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation);
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * projectileForce);
        //enemy.TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("YOU DIED");
        }
    }

    public virtual void Heal(float heal)
    {
        Debug.Log("Player has recieved healing power up");
    }
}
