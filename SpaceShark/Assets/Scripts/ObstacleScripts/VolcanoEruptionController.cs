using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoEruptionAnimBehaviourController : MonoBehaviour
{
	public GameObject preEruption = null;
	public GameObject erupted = null;

	public void VolcanoFinishedErupting()
	{
		preEruption.SetActive(false);
		erupted.SetActive(true);
	}
}
