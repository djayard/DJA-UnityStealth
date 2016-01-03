using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float resetDelay = 5f;
    public AudioClip deathClip;

    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIds hash;
    private ScreenFadeInOut fade;
    private LastPlayerSighting lastSighting;
    private float timer;
    private bool dead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();
        fade = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFadeInOut>();
        lastSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();


    }

    void Update()
    {
        if (health <= 0f)
            if (!dead)
            {
                PlayerDying();
            }
            else
            {
                PlayerDead();
                LevelReset();
            }
                
    }

    void PlayerDying()
    {
        dead = true;
        anim.SetBool(hash.deadBool, dead);
        AudioSource.PlayClipAtPoint(deathClip, GetComponent<Transform>().position);
    }

    void PlayerDead()
    {
        if( anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.dyingState)
        {
            anim.SetBool(hash.deadBool, false);
        }

        anim.SetFloat(hash.speedFloat, 0f);
        playerMovement.enabled = false;
        lastSighting.position = lastSighting.resetPosition;
        GetComponent<AudioSource>().Stop();
    }

    void LevelReset()
    {
        timer += Time.deltaTime;

        if( timer >= resetDelay)
        {
            fade.EndScene();
        }
    }

    public void TakeDamage(float hit)
    {
        health -= hit;
    }

	
}
