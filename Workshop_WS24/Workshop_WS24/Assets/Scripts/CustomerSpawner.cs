using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs;
    public Transform spawnPoint;
    public CounterSlotManager slotManager;
    public RecipeManager recipeManager;
    public float spawnInterval = 5f; // Time in seconds between spawn attempts

    void Start()
    {
        // Validate prefabs
        for (int i = 0; i < customerPrefabs.Length; i++)
        {
            if (customerPrefabs[i] == null)
                Debug.LogError("[Spawner] Missing prefab at index " + i);
            else
                Debug.Log("[Spawner] Prefab at index " + i + ": " + customerPrefabs[i].name);
        }

        // Start repeated spawning
        InvokeRepeating(nameof(AttemptSpawn), 1f, spawnInterval);
    }

    void AttemptSpawn()
    {
        // Only spawn if there is a free slot
        int availableSlot = slotManager.GetAvailableSlotIndex();
        if (availableSlot == -1)
        {
            Debug.Log("[Spawner] No available slots. Skipping spawn.");
            return;
        }

        SpawnCustomer();
    }

   public void SpawnCustomer()
{
    Vector3 spawnPos = spawnPoint.position;
    int randomIndex = Random.Range(0, customerPrefabs.Length);

    GameObject customer = Instantiate(customerPrefabs[randomIndex], spawnPos, Quaternion.identity);

    CustomerAI customerAI = customer.GetComponent<CustomerAI>();
    customerAI.slotManager = slotManager;
    customerAI.recipeManager = recipeManager;

    customerAI.Initialize(); // 🔥 This line ensures safe setup

    Debug.Log("[Spawner] Spawned customer: " + customer.name);
}

}
