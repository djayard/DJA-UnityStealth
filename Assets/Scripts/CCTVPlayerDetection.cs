using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour
{
    private GameObject player;
    private LastPlayerSighting lastplayersighting;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastplayersighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();

    }

    void OnTriggerStay(Collider other)
    {
        if( other.gameObject == player )
        {
            Vector3 relPlayerpos = player.transform.position - GetComponent<Transform>().position;
            RaycastHit hit;

            if( Physics.Raycast(GetComponent<Transform>().position, relPlayerpos, out hit) )
            {
                if (hit.collider.gameObject == player)
                    lastplayersighting.position = player.GetComponent<Transform>().position;
            }
        }

    }
}
