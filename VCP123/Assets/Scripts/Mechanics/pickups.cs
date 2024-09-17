using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SpriteRenderer))]
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

    public AudioClip pickupsound;

    SpriteRenderer sr;
    AudioSource aud;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Collider2D myCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(myCollider, collider);

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
            sr.enabled = false;
            aud.PlayOneShot
            Destroy(gameObject);
        }

    }
}