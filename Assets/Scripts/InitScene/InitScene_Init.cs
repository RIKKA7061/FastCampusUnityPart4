using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
	private static bool isInit = false;

	private const int PROGRESS_VALUE = 5;
	private int progressAddValue = 0;



	private SystemManager systemManager; // cache
	private InitScene_UI initSceneUI; // cache
	private ObjectPoolManager objectPoolManager; // cache
	private EffectManager effectManager; // cache
	private SoundManager soundManager; // cache
	private WindowManager windowManager; // cache
	private SceneLoadManager sceneLoadManager; // cache

	private void Awake()
	{
		initSceneUI = FindAnyObjectByType<InitScene_UI>();

		if (!isInit)
		{
			isInit = true;
			systemManager = new GameObject("SystemManager").AddComponent<SystemManager>();
			Debug.Log("InitScene_Init Length: " + systemManager.IsInit);
			objectPoolManager = new GameObject("ObjectPoolManager").AddComponent<ObjectPoolManager>();
			effectManager = new GameObject("EffectManager").AddComponent<EffectManager>();
			soundManager = new GameObject("SoundManager").AddComponent<SoundManager>();
			windowManager = new GameObject("WindowManager").AddComponent<WindowManager>();
			sceneLoadManager = new GameObject("SceneLoadManager").AddComponent<SceneLoadManager>();
		}
		else
		{
			systemManager = FindAnyObjectByType<SystemManager>();
			Debug.Log("InitScene_Init Length: " + systemManager.IsInit);
			objectPoolManager = FindAnyObjectByType<ObjectPoolManager>();
			effectManager = FindAnyObjectByType<EffectManager>();
			soundManager = FindAnyObjectByType<SoundManager>();
			windowManager = FindAnyObjectByType<WindowManager>();
			sceneLoadManager = FindAnyObjectByType<SceneLoadManager>();
		}
	}

	private IEnumerator Start()
	{
		yield return null;
		StartCoroutine(C_Manager());
	}

	private IEnumerator C_Manager()
	{
		List<Action> actions = new List<Action>
		{
			SystemManagerInit,
			ObjectPoolManagerInit,
			EffectManagerInit,
			SoundManager,
			WindowManagerInit,
			SceneLoadManagerInit,
			LoadScene
		};

		foreach (var action in actions)
		{
			yield return new WaitForSeconds(0.1f);
			action?.Invoke(); // if (== null) No Start Action
			SetProgress();
		}
	}

	private void SetProgress()
	{
		initSceneUI.SetPercent((float)++progressAddValue / (float)PROGRESS_VALUE);
	}

	private void SystemManagerInit()
	{
		systemManager.SetInit();
		// SystemManager.Instance.SetInit(); //Singleton
	}

	private void ObjectPoolManagerInit()
	{
		objectPoolManager.SetInit();
	}

	private void EffectManagerInit()
	{
		effectManager.SetInit();
	}

	private void SoundManager()
	{
		soundManager.SetInit();
	}

	private void WindowManagerInit()
	{
		windowManager.SetInit();
	}

	private void SceneLoadManagerInit()
	{
		sceneLoadManager.SetInit();
	}

	private void LoadScene()
	{
		sceneLoadManager.SceneLoad(SCENE_TYPE.Lobby);
	}
}
