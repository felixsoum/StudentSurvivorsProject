using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    enum GiantState : int
    {
        Idle = 0,
        Chasing = 1,
        Attacking = 2
    }

    [SerializeField] GameObject knifePrefab;
    private Animator animator;
    GiantState giantState = GiantState.Idle;
    float waitTimer = 2f;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }

    protected override void Update()
    {
        switch (giantState)
        {
            case GiantState.Idle:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    giantState = GiantState.Chasing;
                }
                break;
            case GiantState.Chasing:
                base.Update();
                float distance = Vector3.Distance(transform.position, player.transform.position);
                animator.SetBool("IsWalking", true);
                if (distance < 5f)
                {
                    giantState = GiantState.Attacking;
                }
                break;
            case GiantState.Attacking:
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("Attack");
                giantState = GiantState.Idle;
                waitTimer = 5f;
                break;
        }
    }

    public void SpawnKnife(int numberOfKnives)
    {
        Instantiate(knifePrefab, transform.position, Quaternion.identity);
    }
}
