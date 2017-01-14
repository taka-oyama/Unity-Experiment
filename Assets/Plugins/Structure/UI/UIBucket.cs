using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
public class UIBucket : ScriptableObject
{
	[SerializeField] UIPanel[] uiPrefabs;

	/// <summary>
	/// Find UI with the same name as the class.
	/// Will return null if UI is not found.
	/// </summary>
	public T Find<T>() where T : UIPanel
	{
		foreach(var ui in uiPrefabs) {
			if(ui is T) {
				return ui as T;
			}
		}
		return null;
	}

	/// <summary>
	/// Find UI with the same name as the class.
	/// Will throw and Exception if it's not found.
	/// </summary>
	public T Fetch<T>() where T : UIPanel
	{
		T ui = Find<T>();
		if(ui == null) {
			throw new Exception(typeof(T).Name + " is not found in " + name);
		}
		return ui;
	}

	/// <summary>
	/// Validate added ui and remove duplicates.
	/// This will also sort it in alphabetical order.
	/// </summary>
	void OnValidate()
	{
		this.uiPrefabs = ArrayValidator.RemoveDuplicateAndSort(uiPrefabs);
	}
}
