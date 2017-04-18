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
    public static float fragmentSpeed;
    public static float fallTime;
    public static float fallPeriod;
    public static float detectionRange;
    //public static float segmentLength;
    public static bool blowBack;
    public static GameObject enemy;
    public static GameObject enemyDeath;
    public static GameObject obstacle;
    public static GameObject powerUpCollectable;

    //[Header("Level")]
    //[SerializeField]
    //private float lengthOfASegment;

	[Header("Input")]
	[SerializeField]
	private float swipeOrTapSensitivity = 200.0f;

	[Header("Player")]
	[SerializeField]
	private float speed = 40.0f;
	[SerializeField]
	private float laneChangeSpeed = 10.0f;
    [SerializeField]
    private GameObject powerUpPrefab;

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
    // The enemy prefab for this level
    private GameObject enemyDeathPrefab;
    [SerializeField]
    // The distance from the player the enemy will activate
    private float enemyDetectionRange;
    [SerializeField]
    // The enemy's rate of fire
	private float firingCooldown = 2.0f;
    [SerializeField]
    // Speed at which the fragments fall;
    private float fragmentFallSpeed = 9.8f;
    [SerializeField]
    // Direction of enemy destruction
    private bool blastBackward = true;
    [SerializeField]
    // Time before fragments start to fall
    private float fallDelay = 1.0f;
    [SerializeField]
    // How quickly fragments fall after one another
    private float fallSpread = 1.0f;

    [Header("Debug")]
    [SerializeField]
    // Whether to play level without menu
    private bool testing = true;

    void Awake()
    {
        Init();
    }

	// Use this for initialization
	void Start ()
	{
        //if (testing)
		//{
		//	StateManager.gameState = StateManager.States.play;
		//}
        Init();
		//else
		//{
		//	StateManager.gameState = StateManager.States.menu;
		//}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Init()
    {
        sensitivity = swipeOrTapSensitivity;
        gameSpeed = speed;
        laneMoveSpeed = laneChangeSpeed;
        lowestTransparency = lowestPossibleTransparency;
        playerDistanceTop = fadeDistanceTop;
        playerDistanceMid = fadeDistanceMid;
        eruptionRange = eruptionDistance;
        cooldown = firingCooldown;
        fragmentSpeed = fragmentFallSpeed;
        enemy = enemyPrefab;
        enemyDeath = enemyDeathPrefab;
        obstacle = obstaclePrefab;
        blowBack = blastBackward;
        fallTime = fallDelay;
        fallPeriod = fallSpread;
        detectionRange = enemyDetectionRange;
        powerUpCollectable = powerUpPrefab;
        //segmentLength = lengthOfASegment;
    }
}
