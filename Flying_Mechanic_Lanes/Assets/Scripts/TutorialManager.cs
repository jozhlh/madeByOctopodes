using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	[SerializeField]
	private Text horizontalText = null;

	[SerializeField]
	private Text verticalText = null;

	[SerializeField]
	private Text diagonalText = null;

	[SerializeField]
	private Text shootText = null;

	public static Text horizontal;
	public static Text vertical;
	public static Text diagonal;
	public static Text shoot;

	// Use this for initialization
	void Start () {
		horizontal = horizontalText;
		vertical = verticalText;
		diagonal = diagonalText;
		shoot = shootText;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void DisableUI()
	{
		if (shoot.enabled)
        {
            shoot.enabled = false;
        }
        if (horizontal.enabled)
        {
            horizontal.enabled = false;
        }
        if (vertical.enabled)
        {
            vertical.enabled = false;
        }
        if (diagonal.enabled)
        {
            diagonal.enabled = false;
        }
	}
}
