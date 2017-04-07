using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlanet : MonoBehaviour {

	[SerializeField]
	private GameObject playButton = null;

	[SerializeField]
	private GameObject levelDescription = null;

	[SerializeField]
	private GameObject planetModel = null;

	[SerializeField]
	private GameObject enemyFleet = null;

	//float planetAxis = 0.0f;
	Vector3 planetAxis;
	Vector3 fleetAxis;
	float planetRotationSpeed = 10.0f;
	float fleetRotationSpeed = 40.0f;
	float orbitRadius = 30.0f;
	bool selected = false;

	// Use this for initialization
	void Start () {
		planetAxis = new Vector3(Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f));
		planetAxis.Normalize();
		planetAxis *= planetRotationSpeed;

		fleetAxis = new Vector3(0,1,0);
		enemyFleet.transform.Translate(enemyFleet.transform.right * orbitRadius);
	}
	
	// Update is called once per frame
	void Update ()
	{
		planetModel.transform.Rotate(planetAxis, planetRotationSpeed * Time.deltaTime);
		enemyFleet.transform.RotateAround(planetModel.transform.position, fleetAxis, -fleetRotationSpeed * Time.deltaTime);

	}

	public void ShowPlayButton()
	{
		if (selected)
		{
			playButton.SetActive(true);
		}
	}

	public void HidePlayButton()
	{
		playButton.SetActive(false);
	}

	public void HideLevelDescription()
	{
		levelDescription.SetActive(false);
	}

	public void ShowLevelDescription()
	{
		levelDescription.SetActive(true);
	}

	public void Select()
	{
		selected = true;
		levelDescription.SetActive(true);
	}

	public void Deselect()
	{
		selected = false;
		levelDescription.SetActive(false);
	}
}
