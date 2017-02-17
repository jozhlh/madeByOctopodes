using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleData
{
    public LaneManager.ObstacleLocation lane = LaneManager.ObstacleLocation.W;
    private const int buf = 50;
    private const int segmentLength = LaneManager.lengthOfSegment;
    [Range(buf, LaneManager.lengthOfSegment)]
    public int zPosition = 0;
}

[System.Serializable]
public class EnemyData
{
    public LaneManager.PlayerLanes lane = LaneManager.PlayerLanes.S;
    private const int buf = 50;
    private const int segmentLength = LaneManager.lengthOfSegment;
    [Range(buf, LaneManager.lengthOfSegment)]
    public int zPosition = 0;
}