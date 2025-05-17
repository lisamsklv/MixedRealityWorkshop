using System.Collections.Generic;
using UnityEngine;

public class CounterSlotManager : MonoBehaviour
{
    public Transform[] slots; // Set in inspector
    private bool[] occupied;

    void Awake()
    {
        occupied = new bool[slots.Length];
    }

    public int GetAvailableSlotIndex()
    {
        for (int i = 0; i < occupied.Length; i++)
        {
            if (!occupied[i])
                return i;
        }
        return -1; // No slots available
    }

    public bool TryReserveSlot(CustomerAI customer, out Vector3 spot)
    {
        int index = GetAvailableSlotIndex();
        if (index == -1)
        {
            spot = Vector3.zero;
            return false;
        }

        occupied[index] = true;
        spot = slots[index].position;
        customer.assignedSlotIndex = index;
        return true;
    }

    public void FreeSlot(int index)
    {
        if (index >= 0 && index < occupied.Length)
            occupied[index] = false;
    }
}
