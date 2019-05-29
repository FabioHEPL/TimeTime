using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstCannon : Cannon
{
    protected override void Start()
    {
        base.Start();
        _defaultCooldown = _cooldown;
    }

    public override void Fire()
    {
        // Instantiate    
        base.Fire();
        //Instantiate(_bullet, transform.position + transform.up * _fireDistance, _bullet.transform.rotation);
        _burstCount++;

        if (_burstCount == 1)
        {
            _cooldown = _burstBulletInterval;
        }
        // If it's last bullet, wait for default cooldown
        else if (_burstCount == _burstBulletsAmount)
        {
            _cooldown = _defaultCooldown;
            _burstCount = 0;
        }
    }

    
    protected int _burstCount = 0;
    protected float _defaultCooldown = 0f;

    [Header("Burst Settings")]
    [SerializeField]
    protected int _burstBulletsAmount = 3;
    [SerializeField]
    protected float _burstBulletInterval = 0.5f;



}
