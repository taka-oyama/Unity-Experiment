using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class UIBucket : ScriptableObject
{
	[SerializeField] internal GameObject[] uiObjects;

	void OnEnable()
	{
		this.uiObjects = uiObjects.OrderBy(o => o.name).Distinct().ToArray();
	}
}
