using System.Collections;
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
    public Animator animator;

    private Vector3 targetPosition;
    private Vector3 startPosition; // ⬅️ Remember where the customer spawned
    private CustomerState state;
    public int assignedSlotIndex = -1;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position; // Save spawn position

        Vector3 target;
        if (slotManager.TryReserveSlot(this, out target))
        {
            targetPosition = target;
            state = CustomerState.WalkingToCounter;
            isMoving = true;
            animator.SetBool("IsWalking", true);
        }
        // else
        // {
        //     targetPosition = startPosition; // No slot? Go back immediately
        //     state = CustomerState.Leaving;
        //     isMoving = true;
        //     animator.SetBool("IsWalking", true);
        // }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                animator.SetBool("IsWalking", false);

                switch (state)
                {
                    case CustomerState.WalkingToCounter:
                        state = CustomerState.Ordering;
                        StartCoroutine(OrderAndLeave());
                        break;

                    case CustomerState.Leaving:
                        Destroy(gameObject); // Remove after reaching the exit
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

    public void StartWalkingTo(Vector3 target)
    {
        targetPosition = target;
        state = CustomerState.WalkingToCounter;
        isMoving = true;
        animator.SetBool("IsWalking", true);
    }

    IEnumerator OrderAndLeave()
    {
        yield return new WaitForSeconds(5f); // Wait at the counter

        slotManager.FreeSlot(assignedSlotIndex); // Free the counter spot
        targetPosition = startPosition; // Go back to spawn position
        isMoving = true;
        state = CustomerState.Leaving;
        animator.SetBool("IsWalking", true);
    }
    public void Initialize(CounterSlotManager manager, Vector3 spawnPos, Vector3 counterTarget)
{
    slotManager = manager;
    startPosition = spawnPos;

    transform.position = spawnPos;
    targetPosition = counterTarget;
    state = CustomerState.WalkingToCounter;
    isMoving = true;

    animator.SetBool("IsWalking", true);
}

}
