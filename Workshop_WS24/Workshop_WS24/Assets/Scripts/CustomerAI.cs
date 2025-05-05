using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    public Transform counterPosition;
    public Transform startPosition;
    public float waittime = 3f;

    private NavMeshAgent agent;
    private bool waiting = false;
    private bool done = false;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goToCounter();
    }

    void goToCounter()
    {
        agent.SetDestination(counterPosition.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!done && !waiting && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StartCoroutine(WaitingAtCounter()); 
        }
    }

    System.Collections.IEnumerator WaitingAtCounter()
    {
        waiting = true;
        Debug.Log("Customer is waiting...");
        yield return new WaitForSeconds(waittime);

        // Go back after wait time is over
        BackToStart();
    }

    void BackToStart()
    {
        Debug.Log("Customer is leaving.");
        agent.SetDestination(startPosition.position);
        done = true;
    }
}
