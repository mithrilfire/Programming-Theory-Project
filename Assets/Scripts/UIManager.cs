using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup _gameOverCanvas;
    [SerializeField] CanvasGroup _mainMenuCanvas;
    public enum Menu
    {
        None,
        MainMenu,
        GameOverMenu
    }

    public void OnPlayButtonClick()
    {
        //todo start game
    }

    public void OnExitButtonClick()
    {
#if UNITY_EDITOR
        Debug.Log("Exit button has been Clicked.");
#else
        Application.Quit();
#endif
    }

    public void OnMainMenuButtonClick()
    {
        //todo back to main menu
        SetActiveMenu(Menu.MainMenu);
    }

    public void SetActiveMenu(Menu activeMenu)
    {
        switch (activeMenu)
        {
            case Menu.MainMenu:
                _gameOverCanvas.EnableCanvasGroup(false);
                _mainMenuCanvas.EnableCanvasGroup(true);
                break;
            case Menu.GameOverMenu:
                _gameOverCanvas.EnableCanvasGroup(true);
                _mainMenuCanvas.EnableCanvasGroup(false);
                break;
            default:
                _gameOverCanvas.EnableCanvasGroup(false);
                _mainMenuCanvas.EnableCanvasGroup(false);
                break;
        }

    }
}

static class ExtensionMethods
{
    public static CanvasGroup EnableCanvasGroup(this CanvasGroup cnvGrp, bool isActive)
    {
        cnvGrp.alpha = isActive ? 1 : 0;
        cnvGrp.blocksRaycasts = isActive;
        cnvGrp.interactable = isActive;

        return cnvGrp;
    }
}
