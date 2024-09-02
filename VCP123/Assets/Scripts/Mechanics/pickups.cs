using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{
    public int pickupID;

    public enum PickupType
    {
        Life,
        PowerupJump,
        PowerupSpeed,
        Score
    }

    [SerializeField] private PickupType type;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            switch (type)
            {
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    break;
                case PickupType.PowerupJump:
                case PickupType.PowerupSpeed:
                    Playermovement pc = collider.GetComponent<Playermovement>();
                    pc.PowerupValueChange(type);
                    break;
                case PickupType.Score:
                    break;
            }
            Destroy(gameObject);
        }

    }
}