using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> levels = new List<GameObject>();

	[SerializeField]
	private float radius;
	[SerializeField]
	private bool clock = false;
	[SerializeField]
	private bool anti = false;

	private float placementAngle = 0.0f;
	private Vector3 targetAngle = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 currentAngle = new Vector3(0.0f, 0.0f, 0.0f);
	[SerializeField]
	private float rotationSpeed = 10.0f;
	int selectedPlanet = 0;

	// Use this for initialization
	void Start () {
		// Initialiseinput
        GameInput.ResetSwipe();
        GameInput.OnSwipe += HandleOnSwipe;
		PlacePlanets();
		currentAngle = transform.eulerAngles;
		HidePlayButtons();
	}
	
	void PlacePlanets()
	{
		//placementAngle = (2 * Mathf.PI) / levels.Count;
		placementAngle = 360 / levels.Count;
		int iterator = 0;
		Quaternion zeroRotation = Quaternion.Euler(0,0,0);
		foreach (GameObject planet in levels )
		{
			planet.GetComponent<MenuPlanet>().Deselect();
			planet.transform.Translate(0,0,-radius);
			planet.transform.RotateAround(transform.position, transform.up, -iterator * placementAngle);
			planet.transform.localRotation = zeroRotation;
			iterator++;
		}
		selectedPlanet = 0;
		levels[selectedPlanet].GetComponent<MenuPlanet>().Select();
	}

	void RotateClockwise()
	{
		targetAngle.y += placementAngle;
		if (targetAngle.y > 360)
		{
			targetAngle.y -= 360;
		}
		HidePlayButtons();
		levels[selectedPlanet].GetComponent<MenuPlanet>().Deselect();
		selectedPlanet++;
		if (selectedPlanet > (levels.Count - 1))
		{
			selectedPlanet = 0;
		}
		levels[selectedPlanet].GetComponent<MenuPlanet>().Select();
	}

	void RotateAntiClockwise()
	{
		targetAngle.y -= placementAngle;
		if (targetAngle.y < 0)
		{
			targetAngle.y += 360;
		}
		HidePlayButtons();
		levels[selectedPlanet].GetComponent<MenuPlanet>().Deselect();
		selectedPlanet--;
		if (selectedPlanet < 0)
		{
			selectedPlanet = (levels.Count - 1);
		}
		levels[selectedPlanet].GetComponent<MenuPlanet>().Select();
	}

	// Update is called once per frame
	void Update ()
	{
		if (clock)
		{
			RotateClockwise();
			clock = false;
		}
		if (anti)
		{
			RotateAntiClockwise();
			anti = false;
		}

		currentAngle = new Vector3(
             Mathf.LerpAngle(currentAngle.x, targetAngle.x, rotationSpeed*Time.deltaTime),
             Mathf.LerpAngle(currentAngle.y, targetAngle.y, rotationSpeed*Time.deltaTime),
             Mathf.LerpAngle(currentAngle.z, targetAngle.z, rotationSpeed*Time.deltaTime));
 
        transform.eulerAngles = currentAngle;
		Quaternion zeroRotation = Quaternion.Euler(-currentAngle);
		foreach (GameObject planet in levels )
		{
			//planet.transform.rotation.eulerAngles = currentAngle;
			planet.transform.localRotation = zeroRotation;
		}
	}

	private void HandleOnSwipe(GameInput.Direction direction)
    {
        switch (direction)
		{
			case GameInput.Direction.W:
				//Move player left
				RotateClockwise();
				break;
			case GameInput.Direction.E:
				//Move player Right
				RotateAntiClockwise();
				break;
			default:
				break;
		}
    }

	private void HidePlayButtons()
	{
		foreach (GameObject planet in levels)
		{
			planet.GetComponent<MenuPlanet>().HidePlayButton();
		}
	}
}
