using UnityEngine;

namespace JoanStates
{
    [System.Serializable]
    public enum JoanState
    {
        Spawn, Idle, ToWalk, Walking, BreakWalk, ToRun, Running, BreakRun, TrickTurn, Last
    }

    public enum JoanAnimation
    {
        JoanSpawn,
        JoanIdle,
        JoanToWalk,
        JoanWalking,
        JoanBreakWalk,
        JoanToRun,
        JoanRunning,
        JoanBreakRun,
        JoanTrickTurn,
        Last
    }
}
