using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
	[SerializeField] private GameObject PrefabPopUpMessage;
	[SerializeField] private Transform PosPopUpMessage;

	private static bool isInit = false;

	private const int PROGRESS_VALUE = 5;
	private int progressAddValue = 0;



	private InitScene_UI initSceneUI; // cache
	private ObjectPoolManager objectPoolManager; // cache
	private EffectManager effectManager; // cache
	private SoundManager soundManager; // cache
	private WindowManager windowManager; // cache
	private NetworkManager networkManager; // cache

	private void Awake()
	{
		initSceneUI = FindAnyObjectByType<InitScene_UI>();

		if (!isInit)
		{
			isInit = true;
			objectPoolManager = new GameObject("ObjectPoolManager").AddComponent<ObjectPoolManager>();
			effectManager = new GameObject("EffectManager").AddComponent<EffectManager>();
			soundManager = new GameObject("SoundManager").AddComponent<SoundManager>();
			windowManager = new GameObject("WindowManager").AddComponent<WindowManager>();
			networkManager = new GameObject("NetworkManager").AddComponent<NetworkManager>();
		}
		else
		{
			objectPoolManager = FindAnyObjectByType<ObjectPoolManager>();
			effectManager = FindAnyObjectByType<EffectManager>();
			soundManager = FindAnyObjectByType<SoundManager>();
			windowManager = FindAnyObjectByType<WindowManager>();
			networkManager = FindAnyObjectByType<NetworkManager>();
		}
	}

	private IEnumerator Start()
	{
		yield return null;
		StartCoroutine(C_Manager());
	}

	private IEnumerator C_Manager()
	{
		NetworkManagerInit();

		IEnumerator eNumeRator = NetworkManagerInit();
		yield return StartCoroutine(eNumeRator);
		bool isNetworkManagerSuCCESS = (bool)eNumeRator.Current;
		if (!isNetworkManagerSuCCESS)
		{
			Debug.Log("서버오류, 안내창 띄어주기~");
			GameObject ObjPopUpMessage = Instantiate(PrefabPopUpMessage, PosPopUpMessage);

			PopupMessageInfo popUpMessageInfo = new PopupMessageInfo(POPUP_MESSAGE_TYPE.ONE_BUTTON, "서버오류", "서버오류가 발생하였습니다. 다시 접속하여 주세요.");
			PopupMessage popUpMessage = ObjPopUpMessage.GetComponent<PopupMessage>();
			popUpMessage.OpenMessage(popUpMessageInfo, null, () =>
			{
				// 앱 종료
				Application.Quit();
			});

			yield break;
		}

		yield return StartCoroutine(ETCManager());

	}

	private IEnumerator ETCManager()
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
		SystemManager.Instance.SetInit(); //Singleton
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
		SceneLoadManager.Instance.SetInit();
	}

	private IEnumerator NetworkManagerInit()
	{
		networkManager.SetInit();

		ApplicationConfigSendPacket applicationConfigSendPacket
			= new ApplicationConfigSendPacket(
			Config.SERVER_App_API_URL,
			PACKET_NAME_TYPE.ApplicationConfig,
			Config.E_ENVIRONMENT_TYPE,
			Config.E_OS_TYPE,
			Config.APP_VERSION);

		// Upload on Github, use CallBack..
		//networkManager.GetDataFromServer<ApplicationConfigReceiveLetter>(applicationConfigSendPacket, AppConFig);

		IEnumerator eNumeRator = networkManager.GetDataFromServer<ApplicationConfigReceiveLetter>(applicationConfigSendPacket);
		yield return StartCoroutine(eNumeRator);
		ApplicationConfigReceiveLetter receiveLetter = eNumeRator.Current as ApplicationConfigReceiveLetter;
		if (receiveLetter != null && receiveLetter.ReturenCode == (int)RETURN_CODE.Success)
		{
			SystemManager.Instance.apiURL = receiveLetter.apiURL;
			yield return true;
		}
		else
		{
			yield return false;
		}
	}

	// Upload on Github, use CallBack..
	//private void AppConFig(ReceiveLetterBase receiveLetterBase)
	//{
	//	ApplicationConfigReceiveLetter receiveLetter = receiveLetterBase as ApplicationConfigReceiveLetter;
	//	if (receiveLetter != null && receiveLetter.ReturenCode == (int)RETURN_CODE.Success)
	//	{
	//		SystemManager.Instance.apiURL = receiveLetter.apiURL;
	//		Debug.Log("SUCCESS"); // NEXT Behaviors...
	//		StartCoroutine(ETCManager());
	//	}
	//	else
	//	{
	//		Debug.Log("Error");
	//	}
	//}

	private void LoadScene()
	{
		SceneLoadManager.Instance.SceneLoad(SceneLoadManager.Instance.InitSceneType);
	}


}
