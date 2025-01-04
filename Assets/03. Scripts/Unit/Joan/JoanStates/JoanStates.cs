using UnityEngine;

namespace JoanStates
{
    [System.Serializable]
    public enum JoanState
    {
        Spawn, 
        Idle, 
        ToWalk, Walking, BreakWalk, 
        ToRun, Running, BreakRun, RunToWalk,
        TrickTurn, 
        Falling, 
        Land, 
        LandHard, 
        Jump, 
        ToCrounch, OutCrounch,
        LightAtk, UpLightAtk,
        Last
    }

    public enum JoanAnimation
    {
        JoanSpawn,
        JoanIdle,
        JoanToWalk, JoanWalking, JoanBreakWalk,
        JoanToRun, JoanRunning, JoanBreakRun,
        JoanTrickTurn,
        JoanFalling,
        JoanLand, JoanLandHard,
        JoanJump,
        Last
    }
}
