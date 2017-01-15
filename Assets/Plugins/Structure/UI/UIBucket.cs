using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
public sealed class UIBucket : ScriptableObject
{
	[SerializeField] UIPanel[] uiPrefabs;

	public UIPanel[] Prefabs { get { return uiPrefabs; } }

	void OnValidate()
	{
		this.uiPrefabs = ArrayValidator.RemoveDuplicateAndSort(uiPrefabs);
	}
}
