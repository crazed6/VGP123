using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSource aud;

    [Range(0, 10)]
    public float xVel;
    [Range(0,10 )]
    public float yVel;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public Projectile projectilePrefab;

    public AudioClip firesound;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (xVel == 0 && yVel == 0)
            xVel = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("Please set default valuse on the shoot script for object" + gameObject.name);

    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectile curprojectile = Instantiate(projectilePrefab, spawnPointLeft.position, Quaternion.identity);
            curprojectile.SetVelocity(xVel, yVel);
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, Quaternion.identity);
            curProjectile.SetVelocity(-xVel, yVel);
        }
        if (firesound 
    }
}
