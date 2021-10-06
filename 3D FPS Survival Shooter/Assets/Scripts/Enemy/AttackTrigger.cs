using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private EnemyAI AI;

    private void Start()
    {
        AI = GetComponentInParent<EnemyAI>();
        if(AI == null)
        {
            Debug.Log("AI script not attached");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AI.CanAttack();
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AI.Chase();
        }
    }
}
