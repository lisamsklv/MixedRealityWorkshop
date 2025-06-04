using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class VRButton : MonoBehaviour
{
    public CoffeeMachine coffeeMachine;

    public void TriggerBrew()
    {
        coffeeMachine.StartBrewing();
        PressEffect();
    }

    public void PressButton()
    {
        Debug.Log("Button pressed!");
        TriggerBrew();
        
        // Hier kommt deine Logik hin (z. B. Kaffee starten)
    }

    private IEnumerator PressEffect()
    {
        transform.localScale *= 0.9f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale /= 0.9f;
    }

}
