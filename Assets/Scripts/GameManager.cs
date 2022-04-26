using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        Ship.OnShipDestroy += OnShipDestroyed;
    }
    private void OnDisable()
    {
        Ship.OnShipDestroy -= OnShipDestroyed;
    }
    void OnShipDestroyed(Ship ship)
    {
        Destroy(ship);
    }
}
