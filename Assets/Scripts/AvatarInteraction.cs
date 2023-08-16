using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarInteraction : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] [Range (0, 10)] private float distance = 5;
    private NavMeshAgent nav;

    private void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if ((playerTransform.position - this.transform.position).magnitude <= distance)
        {
            nav.SetDestination(playerTransform.position);
        }

        if ((playerTransform.position - this.transform.position).magnitude <= nav.stoppingDistance + 1)
        {
            StartCoroutine(Audio());
        }
    }

    IEnumerator Audio()
    {
        Debug.Log("Audio CoroutineStarted");
        yield return null;
    }
}
