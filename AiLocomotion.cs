using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform playerTransform;
    NavMeshAgent agent;

    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;

    public Animator animator;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0.0f)
        {
            float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if(sqDistance > maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

    }
}
