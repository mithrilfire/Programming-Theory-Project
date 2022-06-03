using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class RepairShip : Ship
{
    [SerializeField] ParticleSystem _healingEffect;
    [SerializeField] float _repairCooldown = 2f;
    [SerializeField] float _healingRate = 150f;
    float _repairTimer;

    // POLYMORPHISM
    protected override void Action()
    {
        if (_target.Team == _team)
        {
            _repairTimer += Time.deltaTime;

            if (_repairTimer >= _repairCooldown)
            {
                if (_target.TakeHealing(_healingRate))
                {
                    ParticleSystem effect = Instantiate<ParticleSystem>(_healingEffect, _target.transform.position, _healingEffect.transform.rotation);
                    effect.Play();
                }
                _repairTimer = 0f;
            }
        }
        else
        {
            _target = null;
            return;
        }
    }
}
