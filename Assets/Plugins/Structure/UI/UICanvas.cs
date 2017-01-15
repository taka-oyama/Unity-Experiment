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

	public Canvas Canvas { get { return canvas; } }

	void Awake()
	{
		UIBucket essentialBucket = Resources.Load<UIBucket>("CommonUIBucket");
		Assert.IsNotNull(essentialBucket, "EssentialUIBucket not found in Resources");
		this.uiPrefabs = uiPrefabs.Concat(essentialBucket.Prefabs).ToArray();
	}

	public T Create<T>(bool worldPositionStays = false) where T : UIPanel
	{
		return Instantiate(Lookup<T>(), canvas.transform, worldPositionStays);
	}

	public bool Has<T>() where T : UIPanel
	{
		return canvas.GetComponentInChildren<T>() != null;
	}

	/// <summary>
	/// Find UI with the same name as the class.
	/// Will return null if UI is not found.
	/// </summary>
	public T Find<T>() where T : UIPanel
	{
		for(int i = canvas.transform.childCount - 1; i >= 0; i--) {
			Transform child = canvas.transform.GetChild(i);
			T panel = child.GetComponent<T>();
			if(panel != null) return panel;
		}
		return null;
	}

	/// <summary>
	/// Find UI with the same name as the class.
	/// Will throw and Exception if it's not found.
	/// </summary>
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

	T Lookup<T>() where T : UIPanel
	{
		T target = null;
		foreach(var ui in uiPrefabs) {
			if(ui is T) {
				target = ui as T;
				break;
			}
		}
		if(target == null) {
			throw new Exception(typeof(T).Name + " is not found in " + name);
		}
		return target;
	}

	/// <summary>
	/// Remove duplicates and sort the uiPrefabs. (Runs in Editor Only)
	/// </summary>
	void OnValidate()
	{
		this.uiPrefabs = ArrayValidator.RemoveDuplicateAndSort(uiPrefabs);
	}
}
