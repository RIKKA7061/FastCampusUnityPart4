using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInfo
{
	public readonly GameObject prefab;
	public readonly GameObject gameObject;

	public WindowInfo(GameObject prefab, GameObject gameObject)
	{
		this.prefab = prefab;
		this.gameObject = gameObject;
	}
}

public class WindowManager : ManagerBase
{
	// Start is called before the first frame update
	private List<WindowInfo> lists = new List<WindowInfo>();

	private void Awake()
	{
		Dontdestroy<WindowManager>();
	}

	public void SetInit()
	{
	}
	public GameObject NewWindow(GameObject prefab, Transform parent)
	{
		foreach (var list in lists)
		{
			if (list.prefab == prefab)
			{
				list.gameObject.SetActive(true);
				list.gameObject.transform.SetAsLastSibling();
				return list.gameObject;
			}
		}

		GameObject obj = Instantiate(prefab, parent);
		lists.Add(new WindowInfo(prefab, obj));
		return obj;
	}

	public GameObject ChangeWindow(GameObject prefab, Transform parent)
	{
		foreach (var list in lists)
			list.gameObject.SetActive(false);

		foreach (var list in lists)
		{
			if (list.prefab == prefab)
			{
				list.gameObject.SetActive(true);
				list.gameObject.transform.SetAsLastSibling();
				return list.gameObject;
			}
		}

		GameObject obj = Instantiate(prefab, parent);
		lists.Add(new WindowInfo(prefab, obj));
		return obj;
	}
}