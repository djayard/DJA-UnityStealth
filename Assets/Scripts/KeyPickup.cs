using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour
{
    public AudioClip sound;
    private GameObject player;
    private PlayerInventory inventory;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        inventory = player.GetComponent<PlayerInventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject == player)
        {
            AudioSource.PlayClipAtPoint(sound, player.GetComponent<Transform>().position);
            inventory.hasKey = true;
            Destroy(gameObject);
        }
    }
	
}
