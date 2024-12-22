using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase : MonoBehaviour
{
    protected void Dontdestroy<T>() where T : Object
    {
		T[] effectManagers = FindObjectsByType<T>(FindObjectsSortMode.None);
		if (effectManagers.Length > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
