using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GotoScene : MonoBehaviour
{
    [MenuItem("Scene/Fast Start")]
    public static void FastStart()
    {
        EditorSceneManager.OpenScene($"Assets/Scenes/{SCENE_TYPE.InGame}.unity");
        EditorApplication.isPlaying = true;
    }

	[MenuItem("Scene/Init")]
	public static void GoToScene_Init()
	{
		EditorSceneManager.OpenScene($"Assets/Scenes/{SCENE_TYPE.Init}.unity");
	}

	[MenuItem("Scene/InGame")]
	public static void GoToScene_InGame()
	{
		EditorSceneManager.OpenScene($"Assets/Scenes/{SCENE_TYPE.InGame}.unity");
	}

	[MenuItem("Scene/Lobby")]
	public static void GoToScene_Lobby()
	{
		EditorSceneManager.OpenScene($"Assets/Scenes/{SCENE_TYPE.Lobby}.unity");
	}
}
