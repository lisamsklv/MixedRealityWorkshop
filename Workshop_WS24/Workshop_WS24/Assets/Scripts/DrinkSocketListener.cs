using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrinkSocketListener : MonoBehaviour
{
    public CounterSlotManager slotManager;

    private XRSocketInteractor socket;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnDrinkPlaced);
    }

    void OnDrinkPlaced(SelectEnterEventArgs args)
    {
        CoffeeCupVR cup = args.interactableObject.transform.GetComponent<CoffeeCupVR>();
        if (cup != null)
        {
            CustomerAI customer = slotManager.GetCustomerAtSocket(socket);
            if (customer != null)
            {
                customer.ReceiveDrink(cup);
                Destroy(cup.gameObject); // optional
            }
        }
    }

    void OnDestroy()
    {
        if (socket != null)
            socket.selectEntered.RemoveListener(OnDrinkPlaced);
    }
}
