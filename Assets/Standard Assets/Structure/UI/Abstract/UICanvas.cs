using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;

[DisallowMultipleComponent]
public class UICanvas : MonoBehaviour
{
	[SerializeField] UIBucket sceneBucket;
	
	UIBucket essentialBucket;
	Canvas targetCanvas;

	void Awake()
	{
		this.targetCanvas = GetComponent<Canvas>();
		this.essentialBucket = Resources.Load<UIBucket>("EssentialUIBucket");

		Assert.IsNotNull(essentialBucket, "EssentialUIBucket not found in Resources");
	}
	
	public T Create<T>(bool worldPositionStays = false) where T : UIPanel
	{
		T prefab = sceneBucket.Find<T>() ?? essentialBucket.Fetch<T>();
		return Instantiate(prefab, targetCanvas.transform, worldPositionStays);
	}

	public bool Has<T>() where T : UIPanel
	{
		return targetCanvas.GetComponentInChildren<T>() != null;
	}

	public T Find<T>() where T : UIPanel
	{
		for(int i = targetCanvas.transform.childCount - 1; i >= 0; i--) {
			Transform child = targetCanvas.transform.GetChild(i);
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
		targetCanvas.GetComponentsInChildren<UIPanel>().Each(ui => ui.Close());
	}
}
