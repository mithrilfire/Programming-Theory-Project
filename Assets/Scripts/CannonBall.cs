using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] ParticleSystem _explosionEffect;
    Rigidbody rb;
    float _damage;
    public float Damage { get => _damage; set => _damage = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //? Should cannon ball knows fire function or parent cannon have the function ?
    public void Fire()
    {
        //! hard coded up vector. may be needs to be ballistic accurate.
        rb.AddForce((transform.forward + new Vector3(0, 0.4f, 0)).normalized * 8f, ForceMode.Impulse);
        Debug.Log(transform.forward);
    }

    //todo add collision effects. for water and ship collision.
    private void OnCollisionEnter(Collision other)
    {
        Ship ship = other.collider.GetComponentInParent<Ship>();

        if (ship)
        {
            ship.TakeDamage(_damage);
            Debug.Log("ship hit! n : " + ship.gameObject.name);
            ParticleSystem effect = Instantiate<ParticleSystem>(_explosionEffect, transform.position, _explosionEffect.transform.rotation);
            effect.Play();
        }
        Destroy(gameObject);
    }
}
