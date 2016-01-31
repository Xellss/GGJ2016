using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    void Start()
    {
        target = GameObject.Find("Solaris_Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        Vector3.Distance(transform.position, target.position);
        if (Vector3.Distance(transform.position, target.position) < 4)
        {
            animator.SetInteger("Attack", 1);
        }
        else
        {
            animator.SetInteger("Attack", 0);
        }

    }
}
