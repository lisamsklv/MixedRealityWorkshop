using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuButton : MonoBehaviour
{
    public GameManager gameManager;

    public void OnSelectEntered(XRBaseInteractor interactor)
    {
        gameManager.BackToMenu();
    }
}
