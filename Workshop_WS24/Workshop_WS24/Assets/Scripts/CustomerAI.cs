using System.Collections.Generic;
using UnityEngine;
using TMPro; // make sure this is at the top if using TextMeshPro

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

    [Header("Drink Order (Tag-Based)")]
    
    public List<string> expectedIngredientTags;

    public RecipeManager recipeManager;
    private CoffeeRecipe assignedRecipe;

    private GameObject orderBubble;
    private TextMeshProUGUI orderText   ;


    void Start()
    {
        startPosition = transform.position;

        orderBubble = transform.Find("OrderBubble")?.gameObject;

        if (orderBubble != null)
        {
    orderText = orderBubble.transform.Find("Order")?.GetComponent<TextMeshProUGUI>();

    if (orderText != null)
    {
        orderText.text = assignedRecipe.recipeName; // or ingredients
    }

    orderBubble.SetActive(false); // Hide until ready
}

        
        

    // if (recipeManager == null || slotManager == null)
        // {
        //     Debug.LogError("[CustomerAI] RecipeManager or SlotManager not assigned!");
        //     return;
        // }

        // assignedRecipe = recipeManager.GetRandomRecipe();

        // expectedIngredientTags = assignedRecipe.ingredientTags;

        //     Vector3 target;
        //     if (slotManager.TryReserveSlot(this, out target))
        //     {
        //         targetPosition = target;
        //         state = CustomerState.WalkingToCounter;
        //         isMoving = true;

        //         Debug.Log("[CustomerAI] Start: Walking from " + startPosition + " to " + targetPosition);
        //     }
        //     else
        //     {
        //         Debug.LogWarning("[CustomerAI] No available slot. Customer will stay idle or be removed.");
        //         // Optional: Destroy(gameObject);
        //     }
    }

    public void Initialize()
{
    if (recipeManager == null || slotManager == null)
    {
        Debug.LogError("[CustomerAI] RecipeManager or SlotManager not assigned!");
        return;
    }

    assignedRecipe = recipeManager.GetRandomRecipe();
    expectedIngredientTags = assignedRecipe.ingredientTags;

    Debug.Log("[CustomerAI] Assigned drink: " + assignedRecipe.recipeName);
    Debug.Log("[CustomerAI] Required ingredients: " + string.Join(", ", expectedIngredientTags));

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

                        if (orderBubble != null)
                            orderBubble.SetActive(true);

                        Invoke(nameof(FinishOrdering), 100f);
                        break;


                    case CustomerState.Leaving:
                        if (orderBubble != null)
                            orderBubble.SetActive(false);

                        DestroySelf();
                        break;

                }
            }
        }
        
        if (orderBubble != null && Camera.main != null)
{
    orderBubble.transform.LookAt(Camera.main.transform);
    orderBubble.transform.Rotate(0, 0f, 0); // correct the facing direction
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
        var interactor = GetComponentInChildren<UnityEngine.XR.Interaction.Toolkit.XRSocketInteractor>();
        if (interactor != null)
        {
            interactor.enabled = false;
        }

        Destroy(gameObject);
    }

    // 🔍 Called when a drink is placed in front of the customer
    public void ReceiveDrink(CoffeeCupVR cup)
{
    if (cup == null)
    {
        Debug.LogWarning("[CustomerAI] No cup provided.");
        PlayBadReaction();
        GameManager.Instance?.RegisterServed(false);
        return;
    }

    if (cup.isFinalized && cup.MatchesFinalRecipe(assignedRecipe.recipeName))
    {
        Debug.Log("[CustomerAI] Received correct finalized drink!");
        PlayGoodReaction();
        GameManager.Instance?.RegisterServed(true);
    }
    else if (!cup.isFinalized && cup.MatchesRecipe(expectedIngredientTags))
    {
        Debug.Log("[CustomerAI] Received correct unfinalized drink (by ingredients)!");
        PlayGoodReaction();
        GameManager.Instance?.RegisterServed(true);
    }
    else
    {
        Debug.Log("[CustomerAI] Wrong drink!");
        PlayBadReaction();
        GameManager.Instance?.RegisterServed(false);
    }
}



    void PlayGoodReaction()
    {
        Debug.Log("[CustomerAI] 😀 Happy reaction!");
        // Add sound, animation, tip, etc.
    }

    void PlayBadReaction()
    {
        Debug.Log("[CustomerAI] 😠 Angry reaction!");
        // Add negative sound, shake head, etc.
    }
}
