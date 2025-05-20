using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CounterSlotManager : MonoBehaviour
{
    public Transform[] slots;                         // Customer positions
    public XRSocketInteractor[] sockets;              // Socket interactors, must be same length/order
    private bool[] occupied;
    private CustomerAI[] assignedCustomers;           // Track who's at each slot

    

    void Awake()
    {
        int length = slots.Length;

        occupied = new bool[length];
        assignedCustomers = new CustomerAI[length];

        if (sockets.Length != length)
            Debug.LogWarning("[SlotManager] Sockets and slots count mismatch!");

        Debug.Log("[SlotManager] Initialized with " + length + " slots.");
    }

    public int GetAvailableSlotIndex()
    {
        for (int i = 0; i < occupied.Length; i++)
        {
            if (!occupied[i])
            {
                Debug.Log("[SlotManager] Found available slot at index " + i);
                return i;
            }
        }
        Debug.Log("[SlotManager] No available slots.");
        return -1;
    }

    public bool TryReserveSlot(CustomerAI customer, out Vector3 slotPosition)
    {
        int index = GetAvailableSlotIndex();
        if (index == -1)
        {
            slotPosition = Vector3.zero;
            return false;
        }

        occupied[index] = true;
        assignedCustomers[index] = customer;
        slotPosition = slots[index].position;
        customer.assignedSlotIndex = index;
        Debug.Log("[SlotManager] Reserved slot " + index + " for customer.");
        return true;
    }

    public void FreeSlot(int index)
    {
        if (index >= 0 && index < occupied.Length)
        {
            occupied[index] = false;
            assignedCustomers[index] = null;
            Debug.Log("[SlotManager] Freed slot " + index);
        }
    }

    public CustomerAI GetCustomerAtSocket(XRSocketInteractor socket)
    {
        for (int i = 0; i < sockets.Length; i++)
        {
            if (sockets[i] == socket)
            {
                return assignedCustomers[i];
            }
        }
        return null;
    }
}
