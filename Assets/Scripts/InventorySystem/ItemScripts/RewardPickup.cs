using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPickup : MonoBehaviour
{
    public float PickupRadius = 1.0f;
    public ScoreItemData ScoreData;
    private SphereCollider col;

    private void Awake()
    {
        col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = PickupRadius;
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
