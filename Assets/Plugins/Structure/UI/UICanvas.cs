using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections.Generic;

[DisallowMultipleComponent]
public sealed class UICanvas : MonoBehaviour
{
	[SerializeField] Canvas canvas;
	[SerializeField] UIPanel[] uiPrefabs;
	UIBucket essentialBucket;

	public Canvas Canvas { get { return canvas; } }

	void Awake()
	{
		this.essentialBucket = Resources.Load<UIBucket>("EssentialUIBucket");
		Assert.IsNotNull(essentialBucket, "EssentialUIBucket not found in Resources");
	}

	public T Create<T>(bool worldPositionStays = false) where T : UIPanel
	{
		T prefab = uiPrefabs.FirstOrDefault(u => u.GetType() == typeof(T)) as T;
		prefab = prefab ?? essentialBucket.Fetch<T>();
		return Instantiate(prefab, canvas.transform, worldPositionStays);
	}

	public bool Has<T>() where T : UIPanel
	{
		return canvas.GetComponentInChildren<T>() != null;
	}

	public T Find<T>() where T : UIPanel
	{
		for(int i = canvas.transform.childCount - 1; i >= 0; i--) {
			Transform child = canvas.transform.GetChild(i);
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
		canvas.GetComponentsInChildren<UIPanel>().Each(ui => ui.Close());
	}

	void OnValidate()
	{
		this.uiPrefabs = ArrayValidator.RemoveDuplicateAndSort(uiPrefabs);
	}
}
