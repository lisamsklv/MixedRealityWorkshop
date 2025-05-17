using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    public enum CustomerState
    {
        WalkingToCounter,
        Waiting,
        Ordering,
        Leaving
    }

    public NavMeshAgent agent;
    public CounterSlotManager slotManager;

    public int assignedSlotIndex = -1;
    private CustomerState state;

    void Start()
    {
        Vector3 target;
        if (slotManager.TryReserveSlot(this, out target))
        {
            agent.SetDestination(target);
            state = CustomerState.WalkingToCounter;
        }
        else
        {
            state = CustomerState.Leaving;
            //LeaveScene(); // handle this however you want
        }
    }

    void Update()
    {
        switch (state)
        {
            case CustomerState.WalkingToCounter:
                if (!agent.pathPending && agent.remainingDistance < 0.2f)
                {
                    state = CustomerState.Ordering;
                    StartCoroutine(OrderAndLeave());
                }
                break;
        }
    }

    IEnumerator OrderAndLeave()
    {
        yield return new WaitForSeconds(3f); // simulate ordering time
        slotManager.FreeSlot(assignedSlotIndex);
        agent.SetDestination(GetExitPosition());
        state = CustomerState.Leaving;
    }

    Vector3 GetExitPosition()
    {
        // Define an exit location in your scene
        return new Vector3(0, 0, -10);
    }
}
