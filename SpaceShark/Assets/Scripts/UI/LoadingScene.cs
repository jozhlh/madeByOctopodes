using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : Singleton<LoadingScene> 
{
	public static void UnloadLoadingScene()
	{
		GameObject.Destroy(instance.gameObject);
		
	}
}
