using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button quitButton;

    public List<Button> returnButtons;

    void Start()
    {
        Debug.Log("[GameStartMenu] Initializing...");
     

    Debug.Log("Camera active: " + Camera.main.gameObject.activeSelf);
    Debug.Log("Camera position: " + Camera.main.transform.position);


        EnableMainMenu();

        // Hook button events
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            if (item != null)
                item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("Loading game scene...");
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void HideAll()
    {
        if (mainMenu != null)
            mainMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        if (mainMenu != null)
            mainMenu.SetActive(true);
    }
}
