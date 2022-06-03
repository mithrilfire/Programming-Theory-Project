using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameState _currentState = GameState.Play;
    [SerializeField] UIManager _uIManager;
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
            _uIManager.SetActiveMenu(UIManager.Menu.GameOverMenu);
            _currentState = GameState.MainMenu;
        }/*
        else if (_currentState == GameState.MainMenu)
        {

        }
        else if (_currentState == GameState.Play)
        {

        }*/
    }

    void OnShipDestroyed(Ship.ShipInfo info)
    {
        if (info.IsItMainShip)
        {
            _currentState = GameState.GameOver;
        }
    }
}
