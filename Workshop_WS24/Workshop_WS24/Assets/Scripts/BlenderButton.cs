using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlenderButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public AudioSource blenderSound;
    public AudioSource fillSound;
    public GameObject[] cutObjects;
    public GameObject cup;

    public Image buttonImage; // if using icons later
    public Sprite fillCupIcon;

    private enum ButtonState { Insert, StartBlender, FillCup }
    private ButtonState currentState = ButtonState.Insert;

    void Start()
    {
        UpdateButtonVisuals();
    }

    public void HandleButtonPress()
    {
        switch (currentState)
        {
            case ButtonState.Insert:
                if (AreYouHoldingCutObjects())
                {
                    InsertIngredients();
                }
                break;

            case ButtonState.StartBlender:
                StartBlender();
                break;

            case ButtonState.FillCup:
                if (IsHoldingCup())
                {
                    FillCup();
                }
                break;
        }
    }

    void InsertIngredients()
    {
        foreach (GameObject obj in cutObjects)
        {
            if (obj != null) obj.SetActive(false);
        }

        currentState = ButtonState.StartBlender;
        UpdateButtonVisuals();
    }

    void StartBlender()
    {
        blenderSound.Play();

        // After blend sound finishes, advance to fill stage
        Invoke(nameof(EnableFillCupState), blenderSound.clip.length);
    }

    void EnableFillCupState()
    {
        currentState = ButtonState.FillCup;
        UpdateButtonVisuals();
    }

    void FillCup()
    {
        fillSound.Play();
        Debug.Log("Cup filled!");
        // Optional: show fill animation or enable liquid in cup
        // Optionally disable the button now
    }

    void UpdateButtonVisuals()
    {
        switch (currentState)
        {
            case ButtonState.Insert:
                buttonText.text = "Insert";
                break;
            case ButtonState.StartBlender:
                buttonText.text = "Start Blender";
                break;
            case ButtonState.FillCup:
                buttonText.text = "Fill Cup";
                if (buttonImage != null && fillCupIcon != null)
                {
                    buttonImage.sprite = fillCupIcon;
                }
                break;
        }
    }

    bool AreYouHoldingCutObjects()
    {
        foreach (GameObject obj in cutObjects)
        {
            if (obj != null && obj.transform.parent != null &&
                obj.transform.parent.CompareTag("PlayerHand"))
            {
                return true;
            }
        }
        return false;
    }

    bool IsHoldingCup()
    {
        return cup != null &&
               cup.transform.parent != null &&
               cup.transform.parent.CompareTag("PlayerHand");
    }
}
