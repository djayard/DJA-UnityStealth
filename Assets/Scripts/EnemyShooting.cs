using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public float maxDamage = 120f;
    public float minDamage = 45f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private Animator anim;
    private HashIds hash;
    private LineRenderer laserShotLine;
    private Light laserShotLight;
    private SphereCollider col;
    private Transform playerPosition;
    private PlayerHealth playerHealth;
    private bool shooting;
    private float scaledDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();
        laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLine.GetComponent<Light>();
        col = GetComponent<SphereCollider>();
        playerPosition = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = playerPosition.gameObject.GetComponent<PlayerHealth>();

        laserShotLine.enabled = false;
        laserShotLight.intensity = 0f;

        shooting = false;
        scaledDamage = maxDamage - minDamage;
    }

    void Update()
    {
        float shot = anim.GetFloat(hash.shotFloat);
        if (shot > 0.5f && !shooting)
        {
            Shoot();

        }
        else if( shot < 0.5f)
        {
            shooting = false;
            laserShotLine.enabled = false;
        }

        laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
    }

    void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(hash.aimWeightFloat);
        anim.SetIKPosition(AvatarIKGoal.RightHand, playerPosition.position + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }

    void Shoot()
    {
        shooting = true;
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, playerPosition.position)) / col.radius;
        float damage = scaledDamage * fractionalDistance + minDamage;
        playerHealth.TakeDamage(damage);
        ShotEffects();
    }

    void ShotEffects()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, playerPosition.position + Vector3.up * 1.5f);
        laserShotLine.enabled = true;
        laserShotLight.intensity = flashIntensity;
        AudioSource.PlayClipAtPoint(shotClip, laserShotLine.transform.position);

    }

	
}
