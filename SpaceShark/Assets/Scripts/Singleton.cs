using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T s_instance;

    public static T instance
	{ 
		get 
		{
            // Check if the instance already exists in the scene
            if (s_instance == null)
                s_instance = (T)FindObjectOfType(typeof(T));

            return s_instance;
        }
    }
}
