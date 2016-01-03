﻿using UnityEngine;
using System.Collections;

public class HashIds : MonoBehaviour
{
    public int dyingState;
    public int deadBool;
    public int locomotionState;
    public int shoutState;
    public int speedFloat;
    public int sneakingBool;
    public int shoutingBool;
    public int shotFloat;
    public int aimWeightFloat;
    public int angularSpeedFloat;
    public int openBool;
    public int playerInSightBool;
    public int emptyState;


    void Awake()
    {
        dyingState = Animator.StringToHash("Base Layer.Dying");
        locomotionState = Animator.StringToHash("Base Layer.Locomotion");
        shoutState = Animator.StringToHash("Shouting.Shout");
        deadBool = Animator.StringToHash("Dead");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        shoutingBool = Animator.StringToHash("Shouting");
        playerInSightBool = Animator.StringToHash("PlayerInSight");
        shotFloat = Animator.StringToHash("Shot");
        aimWeightFloat = Animator.StringToHash("AimWeight");
        angularSpeedFloat = Animator.StringToHash("AngularSpeed");
        openBool = Animator.StringToHash("Open");
        emptyState = Animator.StringToHash("Shouting.Empty");

    }

}