using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
public class UIBucket : ScriptableObject
{
	[SerializeField] UIPanel[] uis;

	/// <summary>
	/// Find UI with the same name as the class.
	/// Will return null if UI is not found.
	/// </summary>
	public T Find<T>() where T : UIPanel
	{
		foreach(var ui in uis) {
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
		var hashSet = new HashSet<UIPanel>();
		for(int i = 0; i < uis.Length; i++) {
			if(uis[i] == null) {
				continue;
			}
			if(hashSet.Contains(uis[i])) {
				Debug.LogErrorFormat("{0} already exists in {1}!", uis[i].name, name);
				uis[i] = null;
				continue;
			}
			hashSet.Add(uis[i]);
		}

		int currentCount = uis.Length;
		uis = uis.Where(o => o != null).OrderBy(o => o.name).ToArray();
		Array.Resize(ref uis, currentCount);
	}
}
