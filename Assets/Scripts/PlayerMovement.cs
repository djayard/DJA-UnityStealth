using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;
    public float damping = 0.1f;

    private Animator anim;
    private HashIds hash;
    private Vector3 targetDirection;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();
        targetDirection = new Vector3();

        anim.SetLayerWeight(1, 0.8f);
    }

    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");

        MovementManagement(h, v, sneak);


    }

    void MovementManagement(float horizontal, float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (horizontal != 0f || vertical != 0f)
        {
            Rotating(horizontal, vertical);
            anim.SetFloat(hash.speedFloat, 5.5f, damping, Time.deltaTime);
        }
        else
            anim.SetFloat(hash.speedFloat, 0f);

    }

    void Rotating(float horizontal, float vertical)
    {
        targetDirection.Set(horizontal, 0f, vertical);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }

    void AudioManagement(bool shout)
    {
        if( anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState )
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
            GetComponent<AudioSource>().Stop();

        if (shout)
            AudioSource.PlayClipAtPoint(shoutingClip, GetComponent<Transform>().position);

    }


}
