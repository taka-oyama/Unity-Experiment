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

	/// <summary>
	/// Instantiate a GameObject with component T.
	/// T has to be registered in the uiPrefabs list.
	/// </summary>
	public T Create<T>(bool worldPositionStays = false) where T : UIPanel
	{
		return Instantiate(LookupPrefab<T>(), canvas.transform, worldPositionStays);
	}

	/// <summary>
	/// Check if canvas has T.
	/// </summary>
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
	/// Close the top UI on canvas.
	/// </summary>
	public void Pop()
	{
		Find<UIPanel>().Close();
	}

	/// <summary>
	/// returns the number of UIPanels in canvas.
	/// </summary>
	public int Count()
	{
		return canvas.GetComponentsInChildren<UIPanel>().Length;
	}

	/// <summary>
	/// Closes all UIPanels in canvas.
	/// </summary>
	public void CloseAll()
	{
		canvas.GetComponentsInChildren<UIPanel>().Each(ui => ui.Close());
	}

	/// <summary>
	/// Lookup T in the uiPrefab list. Will throw exception if T is not found.
	/// </summary>
	T LookupPrefab<T>() where T : UIPanel
	{
		T prefab = null;
		foreach(var ui in uiPrefabs) {
			if(ui is T) {
				prefab = ui as T;
				break;
			}
		}
		if(prefab == null) {
			throw new Exception(string.Format("{0} is not found in {1}'s uiPrefab list.", typeof(T).Name, name));
		}
		return prefab;
	}

	/// <summary>
	/// Remove duplicates and sort the uiPrefabs. (Runs in Editor Only)
	/// </summary>
	void OnValidate()
	{
		this.uiPrefabs = ArrayValidator.RemoveDuplicateAndSort(uiPrefabs);
	}
}
