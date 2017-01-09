using UnityEngine;
using System;

[RequireComponent(typeof(Canvas))]
[DisallowMultipleComponent]
public abstract class UICanvas : GlobalBehaviour<UICanvas>
{
	[SerializeField] UIBucket bucket;

	public T Create<T>(bool worldPositionStays = false) where T : UIPanel
	{
		return Instantiate(bucket.Fetch<T>(), transform, worldPositionStays);
	}

	public bool Has<T>() where T : UIPanel
	{
		return GetComponentInChildren<T>() != null;
	}

	public T Find<T>() where T : UIPanel
	{
		for(int i = transform.childCount - 1; i >= 0; i--) {
			Transform child = transform.GetChild(i);
			T panel = child.GetComponent<T>();
			if(panel != null) return panel;
		}
		return null;
	}

	public T Fetch<T>() where T : UIPanel
	{
		T panel = Find<T>();
		if(panel == null) {
			throw new Exception(typeof(T).Name + " is not found in " + name);
		}
		return panel;
	}

	public void Pop()
	{
		Fetch<UIPanel>().Close();
	}

	public void CloseAll()
	{
		GetComponentsInChildren<UIPanel>().Each(ui => ui.Close());
	}
}
