using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour
{
    public float timeToCloseDoors = 2f;
    public float timeToLiftStart = 3f;
    public float timeToLevelEnd = 6f;
    public float liftSpeed = 3f;

    private GameObject player;
    private Animator playerAnim;
    private HashIds hash;
    private CameraMovement camMovement;
    private ScreenFadeInOut fade;
    private LiftDoorsTracking liftDoorsTracking;
    private bool playerInLift;
    private float timer = 0f;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerAnim = player.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIds>();
        camMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
        fade = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFadeInOut>();
        liftDoorsTracking = GetComponent<LiftDoorsTracking>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInLift = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && timer < timeToLiftStart)
        {
            playerInLift = false;
            timer = 0;
        }
    }

    void Update()
    {
        if (playerInLift)
            LiftActivation();

        if (timer < timeToCloseDoors)
        {
            liftDoorsTracking.DoorFollowing();
        }
        else
            liftDoorsTracking.CloseDoors();

    }

    void LiftActivation()
    {
        timer += Time.deltaTime;

        if( timer >= timeToLiftStart)
        {
            playerAnim.SetFloat(hash.speedFloat, 0);
            camMovement.enabled = false;
            player.transform.parent = GetComponent<Transform>();

            GetComponent<Transform>().Translate(Vector3.up * liftSpeed * Time.deltaTime);

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();

            if (timer >= timeToLevelEnd)
                fade.EndScene();
        }
    }


	
}
