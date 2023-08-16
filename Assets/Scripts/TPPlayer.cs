using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 positionFinale;

    public void ChangePosition()
    {
        player.transform.position = positionFinale;
    }
}
