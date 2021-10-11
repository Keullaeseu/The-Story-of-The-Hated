using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using Ke.Inputs;

public class InGameMenu : MonoBehaviour
{
    public PlayerInitialization playerInitialization = null;

    // UI
    [SerializeField] private GameObject gameMenu = null;
    private bool isMenuCanvasEnabled = true;

    private Controls controls;
    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    private void Awake()
    {
        Controls.RealTimeStrategy.MenuButton.performed += ctx => MenuButton();
    }

    public void ExitToMenu()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }

        // Destroy current network manager for clear session
        Destroy(NetworkManager.singleton);

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }

        Application.Quit();
    }

    #region Button pressed events

    public void MenuButton()
    {
        // Disable\Enable control for in game player
        playerInitialization.ControlsDisable(!isMenuCanvasEnabled);
        gameMenu.SetActive(isMenuCanvasEnabled);

        isMenuCanvasEnabled = !isMenuCanvasEnabled;
    }

    public void MenuContinueButton()
    {
        // Disable\Enable control for in game player
        playerInitialization.ControlsDisable(!isMenuCanvasEnabled);
        gameMenu.SetActive(isMenuCanvasEnabled);

        isMenuCanvasEnabled = !isMenuCanvasEnabled;
    }

    #endregion

    private void OnEnable() => Controls.Enable();

    private void OnDisable() => Controls.Disable();
}
