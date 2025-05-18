using UnityEngine;

public class CounterSlotManager : MonoBehaviour
{
    public Transform[] slots;
    private bool[] occupied;

    void Awake()
    {
        occupied = new bool[slots.Length];
        Debug.Log("[SlotManager] Initialized with " + slots.Length + " slots.");
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
            Debug.Log("[SlotManager] Freed slot " + index);
        }
    }
}
