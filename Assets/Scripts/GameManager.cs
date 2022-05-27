using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameState _currentState = GameState.Play;
    enum GameState
    {
        MainMenu,
        Play,
        GameOver
    }

    private void OnEnable()
    {
        Ship.OnShipDestroy += OnShipDestroyed;
    }
    private void OnDisable()
    {
        Ship.OnShipDestroy -= OnShipDestroyed;
    }
    private void Update()
    {
        if (_currentState == GameState.GameOver)
        {
            Debug.Log("Game Over");
            _currentState = GameState.MainMenu;
        }
    }

    void OnShipDestroyed(Ship.ShipInfo info)
    {
        if (info.IsItMainShip)
        {
            _currentState = GameState.GameOver;
        }
    }
}
