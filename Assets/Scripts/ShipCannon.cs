using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCannon : MonoBehaviour
{
    [SerializeField] CannonBall _cannonBallPrefab;
    [SerializeField] GameObject _cannonMuzzle;
    [SerializeField] ParticleSystem _fireEffect;

    //todo add fire effect
    public void Fire(Vector3 target, float damage)
    {
        transform.LookAt(target);
        Vector3 eul = transform.rotation.eulerAngles;
        eul.x = 0f;
        eul.z = 0f;
        transform.rotation = Quaternion.Euler(eul);
        CannonBall ball = Instantiate<CannonBall>(_cannonBallPrefab, _cannonMuzzle.transform.position, _cannonMuzzle.transform.rotation);
        ball.Damage = damage;
        ball.transform.LookAt(target);
        ball.Fire();
        _fireEffect.Play();
        //*_cannonObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
