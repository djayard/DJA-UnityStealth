using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour
{
    public bool requireKey;
    public AudioClip opening;
    public AudioClip buzzer;

    private Animator anim;
    private HashIds hash;
    private GameObject player;
    private PlayerInventory inventory;
    private int count;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        inventory = player.GetComponent<PlayerInventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (requireKey)
            {
                if (inventory.hasKey)
                    ++count;
                else
                {
                    GetComponent<AudioSource>().clip = buzzer;
                    GetComponent<AudioSource>().Play();
                }
            }
            else
                ++count;
        }
        else if( other.gameObject.tag == Tags.enemy)
        {
            if (other is CapsuleCollider)
                ++count;

        }
            

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.player || (other.gameObject.tag == Tags.enemy && other is CapsuleCollider))
            count = Mathf.Max(0, count - 1);

    }

    void Update()
    {
        anim.SetBool(hash.openBool, count > 0);
        if( anim.IsInTransition(0) && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = opening;
            GetComponent<AudioSource>().Play();

        }

    }


	
}
