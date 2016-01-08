using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private LastPlayerSighting lastPlayerSighting;
    private GameObject player;
    private Animator playerAnim;
    private PlayerHealth playerHealth;
    private HashIds hash;
    private Vector3 pastSighting;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();

        personalLastSighting = pastSighting = lastPlayerSighting.resetPosition;
    }

    void Update()
    {
        if( lastPlayerSighting.position != pastSighting)
        {
            personalLastSighting = lastPlayerSighting.position;
        }

        pastSighting = lastPlayerSighting.position;

        if (playerHealth.health > 0f)
            anim.SetBool(hash.playerInSightBool, playerInSight);
        else
            anim.SetBool(hash.playerInSightBool, false);


    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if( angle < fieldOfViewAngle / 2)
            {
                RaycastHit hit;
                if( Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius) )
                {
                    if (hit.collider.gameObject == player)
                        playerInSight = true;

                    lastPlayerSighting.position = player.transform.position;
                }
            }
        }

        int playerLayer0StateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        int playerLayer1StateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;

        if (playerLayer0StateHash == hash.locomotionState || playerLayer1StateHash == hash.shoutState)
        {
            if (CalculatePathLength(player.transform.position) <= col.radius)
            {
                personalLastSighting = player.transform.position;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }

    float CalculatePathLength(Vector3 target)
    {
        NavMeshPath path = new NavMeshPath();
        if (nav.enabled)
            nav.CalculatePath(target, path);

        Vector3 [] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = target;

        for (uint i = 0; i < path.corners.Length; ++i)
            allWayPoints[i + 1] = path.corners[i];

        float pathLength = 0f;

        for (uint i = 0; i < allWayPoints.Length-1; ++i)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }


	
}
