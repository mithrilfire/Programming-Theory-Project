using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipClass", menuName = "Programming Theory Project/ShipClass", order = 0)]
public class ShipClass : ScriptableObject
{
    [SerializeField] string _shipName = "ShipClass";
    [SerializeField] float _actionRange = 3f;
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _maxSpeed = 3.5f;
    [SerializeField] float _angularSpeed = 120f;
    [SerializeField] float _acceleration = 8f;

    public string ShipName { get => _shipName; set => _shipName = value; }
    public float ActionRange { get => _actionRange; set => _actionRange = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }
    public float AngularSpeed { get => _angularSpeed; set => _angularSpeed = value; }
    public float Acceleration { get => _acceleration; set => _acceleration = value; }
}