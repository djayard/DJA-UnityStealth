using UnityEngine;
using System.Collections;

public class LiftDoorsTracking : MonoBehaviour
{
    public float doorSpeed = 7f;
    private Transform leftOuterDoor;
    private Transform rightOuterDoor;
    private Transform leftInnerDoor;
    private Transform rightInnerDoor;
    private float leftClosedPosX;
    private float rightClosedPosX;

    void Awake()
    {
        leftOuterDoor = GameObject.Find("door_exit_outer_left_001").GetComponent<Transform>();
        rightOuterDoor = GameObject.Find("door_exit_outer_right_001").GetComponent<Transform>();
        leftInnerDoor = GameObject.Find("door_exit_inner_left_001").GetComponent<Transform>();
        rightInnerDoor = GameObject.Find("door_exit_inner_right_001").GetComponent<Transform>();

        leftClosedPosX = leftInnerDoor.position.x;
        rightClosedPosX = rightInnerDoor.position.x;

        
    }

    void MoveDoor(float leftTarget, float rightTarget)
    {
        float newX = Mathf.Lerp(leftInnerDoor.position.x, leftTarget, doorSpeed * Time.deltaTime);
        leftInnerDoor.position = new Vector3(newX, leftInnerDoor.position.y, leftInnerDoor.position.z);

        newX = Mathf.Lerp(rightInnerDoor.position.x, rightTarget, doorSpeed * Time.deltaTime);
        rightInnerDoor.position = new Vector3(newX, rightInnerDoor.position.y, rightInnerDoor.position.z);

    }

    public void DoorFollowing()
    {
        MoveDoor(leftOuterDoor.position.x, rightOuterDoor.position.x);
    }

    public void CloseDoors()
    {
        MoveDoor(leftClosedPosX, rightClosedPosX);
    }


}
