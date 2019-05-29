using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {        
        GameObject bulletPool = new GameObject(string.Format("{0}-{1}", name, "-pool"));
        bulletPool.transform.position = Vector3.zero;
        bulletPool.transform.rotation = Quaternion.identity;
        
        this.pool = bulletPool.AddComponent<Pool>();
    }

    // Update is called once per frame
    void Update()
    {
        // update time
        _timeSinceLastShot += Time.deltaTime;
        if (_timeSinceLastShot > _cooldown)
        {
            Fire();
            _timeSinceLastShot = 0f;
        }
    }

    public virtual void Fire()
    {
        // Instantiate    
        GameObject go = pool.Spawn(_bullet, transform.position + transform.up * _fireDistance, _bullet.transform.rotation);
        if (pool.IsNew)
        {
            Bullet bullet = go.GetComponent<Bullet>();
            bullet.LifeTimeEnded += OnBulletLifeTimeEnded;
            bullet.CollisionEntered += OnBulletCollisionEntered;
        }

        //Bullet bullet = go.GetComponent<Bullet>();
      //  bullet.OnLifeTimeEnded += OnBulletLifeTimeEnded;
    }

    private void OnBulletCollisionEntered(GameObject enemy)
    {
        pool.Despawn(enemy);
    }

    private void OnBulletLifeTimeEnded(GameObject bullet)
    {
      //  Debug.Log("On bullet destroyed");
        pool.Despawn(bullet);
    }

    public void OnDisable()
    {
        
    }

    private Pool pool;

    protected float _timeSinceLastShot = 0f;

    [SerializeField]
    protected GameObject _bullet;

    [SerializeField]
    protected float _fireDistance = 1.5f;

    [SerializeField]
    protected float _cooldown = 1.5f;
}
