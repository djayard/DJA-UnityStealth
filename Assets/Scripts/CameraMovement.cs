using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float smooth = 1.5f;

    private Transform player;
    private Vector3 relativePos;
    private float relativeCameraPosMagnitude;
    private Vector3 newPos;
    private Vector3[] checkPoints;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Transform>();
        relativePos = GetComponent<Transform>().position - player.position;
        relativeCameraPosMagnitude = relativePos.magnitude - 0.5f;
        checkPoints = new Vector3[5];
    }

    void FixedUpdate()
    {
        Vector3 standard = player.position + relativePos;
        Vector3 abovePos = player.position + Vector3.up * relativeCameraPosMagnitude;

        for (uint i = 0; i < 5; ++i)
        {
            checkPoints[i] = Vector3.Lerp(standard, abovePos, i / 4f);
        }

        for ( uint i = 0; i < 5; ++i)
        {
            if (ViewingPositionCheck(checkPoints[i]))
                break;
        }

        GetComponent<Transform>().position = Vector3.Lerp(GetComponent<Transform>().position, newPos, smooth * Time.deltaTime);
        SmoothLookAt();     
        
    }

    bool ViewingPositionCheck(Vector3 posToCheck)
    {
        RaycastHit hit;
        if( Physics.Raycast(posToCheck, player.position - posToCheck, out hit, relativeCameraPosMagnitude) )
        {
            if( hit.transform != player)
            {
                return false;
            }
        }

        newPos = posToCheck;
        return true;
    }

    void SmoothLookAt()
    {
        Vector3 relativePlayerPos = player.position - GetComponent<Transform>().position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relativePlayerPos, Vector3.up);
        GetComponent<Transform>().rotation = Quaternion.Lerp(GetComponent<Transform>().rotation, lookAtRotation, smooth * Time.deltaTime);

    }
    
	
}
