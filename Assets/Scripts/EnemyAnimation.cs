using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
{
    public float deadZone = 5f;

    private Transform playerTransform;
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Animator anim;
    private HashIds hash;
    private AnimatorSetup animSetup;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tags.player).transform;
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();
        animSetup = new AnimatorSetup(anim, hash);

        nav.updateRotation = false;

        anim.SetLayerWeight(1, 1f);
        anim.SetLayerWeight(2, 1f);

        deadZone *= Mathf.Deg2Rad;
    }

    void Update()
    {
        NavAnimSetup();
    }

    void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }

    void NavAnimSetup()
    {
        float speed, angle;

        if( enemySight.playerInSight )
        {
            speed = 0f;
            angle = FindAngle(transform.forward, playerTransform.position - transform.position, transform.up);
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

            if( Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0;
            }

        }

        animSetup.Setup(speed, angle);
    }

    float FindAngle(Vector3 from, Vector3 to, Vector3 up)
    {
        if (Vector3.zero == from)
            return 0f;

        float angle = Vector3.Angle(from, to);
        Vector3 normal = Vector3.Cross(from, to);
        angle *= Mathf.Sign(Vector3.Dot(normal, up));
        angle *= Mathf.Deg2Rad;

        return angle;
    }

}
