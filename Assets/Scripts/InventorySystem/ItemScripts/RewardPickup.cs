using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardPickup : MonoBehaviour
{
    public float PickupRadius = 1.0f;
    private SphereCollider col;

    public TextMeshProUGUI scoreText;
    public int scoreValue = 10;

    private void Awake()
    {
        col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = PickupRadius;

        PlayerActions.OnReward += GetReward;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (!inventory) return;


        //if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        //{
        //    Destroy(this.gameObject);
        //}
    }

    public void GetReward()
    {

    }
}
