using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action<GameObject> LifeTimeEnded;
    public event Action<GameObject> CollisionEntered;

    private void OnEnable()
    {
        _lifeTime = 0;
    }

    //private void OnDisable()
    //{
    //    _lifeTime = 0;
    //}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        _lifeTime += Time.deltaTime;
        if (_lifeTime > _lifeSpan)
        {
          //  Debug.Log("life time ended");
            LifeTimeEnded?.Invoke(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEntered?.Invoke(gameObject);
        //Time.timeScale = 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(name + " | trigger enter");
    }


    [SerializeField]
    private float _lifeTime = 0f;

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _lifeSpan = 5f;
}
