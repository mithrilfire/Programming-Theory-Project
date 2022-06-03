using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] ParticleSystem _explosionEffect;
    [SerializeField] ParticleSystem _splashEffect;
    Ship.ShipTeam _team;
    Rigidbody rb;
    float _damage;
    // ENCAPSULATION
    public float Damage { get => _damage; set => _damage = value; }
    // ENCAPSULATION
    public Ship.ShipTeam Team { get => _team; set => _team = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //? Should cannon ball knows fire function or parent cannon have the function ?
    public void Fire()
    {
        //! hard coded up vector.
        rb.AddForce((transform.forward + new Vector3(0, 0.4f, 0)).normalized * 8f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        Ship ship = other.collider.GetComponentInParent<Ship>();

        if (ship)
        {
            ship.TakeDamage(_damage, transform.position, _team);
            //Debug.Log("ship hit! n : " + ship.gameObject.name);
            ParticleSystem effect = Instantiate<ParticleSystem>(_explosionEffect, transform.position, _explosionEffect.transform.rotation);
            effect.Play();
        }
        else
        {
            ParticleSystem effect = Instantiate<ParticleSystem>(_splashEffect, transform.position, _splashEffect.transform.rotation);
            effect.Play();
        }
        Destroy(gameObject);
    }
}
