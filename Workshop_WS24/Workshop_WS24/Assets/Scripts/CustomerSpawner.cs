using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs; // assign 3 customer prefabs in Inspector
    public Transform spawnPoint;         // assign a spawn point in Inspector
    public CounterSlotManager slotManager;

    public void SpawnCustomer()
{
    Vector3 slotTarget;
    GameObject preview = Instantiate(customerPrefabs[0]);
    CustomerAI previewAI = preview.GetComponent<CustomerAI>();

    if (slotManager.TryReserveSlot(previewAI, out slotTarget))
    {
        Destroy(preview);

        int randomIndex = Random.Range(0, customerPrefabs.Length);
        GameObject customer = Instantiate(customerPrefabs[randomIndex]);
        CustomerAI customerAI = customer.GetComponent<CustomerAI>();

        Vector3 spawnPos = spawnPoint.position;
        customerAI.Initialize(slotManager, spawnPos, slotTarget);
    }
    else
    {
        Destroy(preview);
    }
}

}
