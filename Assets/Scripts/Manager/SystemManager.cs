using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : ManagerBase
{
	private static SystemManager instance;

	public static SystemManager Instance
	{
		get
		{
			//Debug.Log("SystemManager Instance");
			if (instance == null)
				instance = new GameObject("SystemManager").AddComponent<SystemManager>();
			return instance;
		}
	}

	public bool IsInit { get; set; } = false;

	public string apiURL { get; set; } = string.Empty;

	private void Awake()
	{
		//Debug.Log("SystemManager Awake");
		Dontdestroy<SystemManager>();
	}

	public void SetInit()
    {
		//Debug.Log("SystemManager SetInit");
		IsInit = true;
    }
}
