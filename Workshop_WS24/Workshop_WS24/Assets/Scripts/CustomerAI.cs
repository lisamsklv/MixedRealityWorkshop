using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public enum CustomerState
    {
        WalkingToCounter,
        Ordering,
        Leaving
    }

    public float moveSpeed = 2f;
    public CounterSlotManager slotManager;

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private CustomerState state;
    public int assignedSlotIndex = -1;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;

        if (slotManager == null)
        {
            Debug.LogError("[CustomerAI] slotManager is not assigned!");
            return;
        }

        Vector3 target;
        if (slotManager.TryReserveSlot(this, out target))
        {
            targetPosition = target;
            state = CustomerState.WalkingToCounter;
            isMoving = true;

            Debug.Log("[CustomerAI] Start: Walking from " + startPosition + " to " + targetPosition);
        }
        else
        {
            Debug.LogWarning("[CustomerAI] No available slot. Customer will stay idle or be removed.");
            // Optional: Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                Debug.Log("[CustomerAI] Reached target. State: " + state);

                switch (state)
                {
                    case CustomerState.WalkingToCounter:
                        state = CustomerState.Ordering;
                        Debug.Log("[CustomerAI] Ordering at slot index: " + assignedSlotIndex);
                        Invoke(nameof(FinishOrdering), 10f); // Wait 1 second
                        break;

                    case CustomerState.Leaving:
                        Debug.Log("[CustomerAI] Leaving complete. Destroying customer.");
                        DestroySelf(); // Now handled separately
                        break;
                }
            }
        }
    }

    void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.forward = direction;
    }

    void FinishOrdering()
    {
        slotManager.FreeSlot(assignedSlotIndex);
        targetPosition = startPosition;
        isMoving = true;
        state = CustomerState.Leaving;

        Debug.Log("[CustomerAI] Finished ordering. Returning to spawn.");
    }

    void DestroySelf()
    {
        // Cleanly disable XRSocketInteractor if attached
        var interactor = GetComponentInChildren<UnityEngine.XR.Interaction.Toolkit.XRSocketInteractor>();
        if (interactor != null)
        {
            interactor.enabled = false;
        }

        Destroy(gameObject);
    }
}
