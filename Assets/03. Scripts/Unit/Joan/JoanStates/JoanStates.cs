using UnityEngine;

namespace JoanStates
{
    [System.Serializable]
    public enum JoanState
    {
        Spawn, Idle, ToWalk, Walking, BreakWalk, run, Last
    }

    public enum JoanAnimation
    {
        JoanSpawn,
        JoanIdle,
        JoanWalk,
        JoanRun,
        Last
    }
}
