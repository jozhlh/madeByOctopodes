using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public static float cooldown;


	[Header("Input")]
	[SerializeField]
	private float swipeOrTapSensitivity = 200.0f;

	[Header("Player")]
	[SerializeField]
	private float speed = 40.0f;
	[SerializeField]
	private float laneChangeSpeed = 10.0f;

	[Header("Obstacle Fade")]
	[SerializeField]
    // The lowest level the alpha value will lerp to
    private float lowestPossibleTransparency = 0.2f;     
    [SerializeField]
    // The distance from the player an obstacle in the top lane will begin to fade out
    private float fadeDistanceTop = 40.0f;
    [SerializeField]
    // The distance from the player an obstacle in the middle lane will begin to fade out
    private float fadeDistanceMid = 20.0f;

	[Header("Enemy")]
	[SerializeField]
	private float firingCooldown = 2.0f;

	// Use this for initialization
	void Start ()
	{
		sensitivity = swipeOrTapSensitivity;
		gameSpeed = speed;
		laneMoveSpeed = laneChangeSpeed;
		lowestTransparency = lowestPossibleTransparency;
		playerDistanceTop = fadeDistanceTop;
		playerDistanceMid = fadeDistanceMid;
		cooldown = firingCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
