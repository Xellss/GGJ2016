using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour 
{
    public int Damage = 10;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private Health health;
    private Transform myTransform;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        target = GameObject.Find("Solaris_Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(target.position);
        Vector3.Distance(transform.position, target.position);

        if (Vector3.Distance(transform.position, target.position) < 4)
        {
            animator.SetInteger("Attack", 1);
            if (Random.Range(1, 36) == 20)
            {
                RaycastHit hit = new RaycastHit();

                Ray ray = new Ray(myTransform.position, myTransform.forward);

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.transform.gameObject.GetComponent<Health>() as Health != null)
                    {
                        GameObject tempG = hit.transform.gameObject;

                        tempG.GetComponent<Health>().RemoveHealth(this.Damage);
                    }
                }
            }
        }
        else
        {
            animator.SetInteger("Attack", 0);
        }

    }
}
