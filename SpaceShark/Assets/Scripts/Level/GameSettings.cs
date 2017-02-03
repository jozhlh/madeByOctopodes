using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameSettings : MonoBehaviour
{
	public enum LevelTypes {Lava, Grass, Desert, Ice, Cave};
	public static int numberOfLevelTypes = 5;
	public static float sensitivity;
	public static float gameSpeed;
	public static float laneMoveSpeed;
	public static float lowestTransparency;
	public static float playerDistanceTop;
	public static float playerDistanceMid;
    public static float eruptionRange;
	public static float cooldown;
    public static GameObject enemy;
    public static GameObject obstacle;

	[Header("Input")]
	[SerializeField]
	private float swipeOrTapSensitivity = 200.0f;

	[Header("Player")]
	[SerializeField]
	private float speed = 40.0f;
	[SerializeField]
	private float laneChangeSpeed = 10.0f;

    [Header("Obstacles")]
    [SerializeField]
    // The obstacle prefab for this level
    private GameObject obstaclePrefab;
	[SerializeField]
    // The lowest level the alpha value will lerp to
    private float lowestPossibleTransparency = 0.2f;     
    [SerializeField]
    // The distance from the player an obstacle in the top lane will begin to fade out
    private float fadeDistanceTop = 40.0f;
    [SerializeField]
    // The distance from the player an obstacle in the middle lane will begin to fade out
    private float fadeDistanceMid = 20.0f;
    [SerializeField]
    // The distance from the player an obstacle will erupt
    private float eruptionDistance = 100.0f;

    [Header("Enemy")]
    [SerializeField]
    // The enemy prefab for this level
    private GameObject enemyPrefab;
    [SerializeField]
	private float firingCooldown = 2.0f;

    [Header("Debug")]
    [SerializeField]
    // Whether to play level without menu
    private bool testing = true;

    void Awake()
    {
        sensitivity = swipeOrTapSensitivity;
        gameSpeed = speed;
        laneMoveSpeed = laneChangeSpeed;
        lowestTransparency = lowestPossibleTransparency;
        playerDistanceTop = fadeDistanceTop;
        playerDistanceMid = fadeDistanceMid;
        eruptionRange = eruptionDistance;
        cooldown = firingCooldown;
        enemy = enemyPrefab;
        obstacle = obstaclePrefab;
    }

	// Use this for initialization
	void Start ()
	{
        if (testing)
		{
			StateManager.gameState = StateManager.States.play;
		}
		else
		{
			StateManager.gameState = StateManager.States.menu;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
