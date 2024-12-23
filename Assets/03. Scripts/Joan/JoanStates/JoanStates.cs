using UnityEngine;

namespace JoanStates
{
    [System.Serializable]
    public enum JoanState
    {
        Spawn, Idle, walk, run, Last
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
