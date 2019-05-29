using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public event Action<GameObject> EnemyCollided;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameObject pool = new GameObject(string.Format("{0}-{1}", name, "pool"));
        pool.transform.position = Vector3.zero;
        pool.transform.rotation = Quaternion.identity;
        this.pool = pool.AddComponent<Pool>();
    } 

    // Update is called once per frame
    void Update()
    {
        // update time
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > cooldown)
        {
            Spawn();
            timeSinceLastSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject go = pool.Spawn(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        if (pool.IsNew)
        {
            Bullet enemy = go.GetComponent<Bullet>();
            enemy.CollisionEntered += OnEnemyCollisionEntered;
            enemy.LifeTimeEnded += OnEnemyLifeTimeEnded;            
        }
    }

    private void OnEnemyCollisionEntered(GameObject enemy)
    {
        pool.Despawn(enemy);
//        Debug.Log("Enemy collision !");
        EnemyCollided?.Invoke(enemy.gameObject);
    }

    private void OnEnemyLifeTimeEnded(GameObject enemy)
    {
        pool.Despawn(enemy);             
    }

    private Pool pool;
    public float timeSinceLastSpawn = 0f;
    public float cooldown = 1.5f;
    public GameObject enemyPrefab;
}
