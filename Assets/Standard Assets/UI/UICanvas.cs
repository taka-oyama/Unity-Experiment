using UnityEngine;
using System.Collections.Generic;

public class UICanvas : MonoBehaviour
{
	[SerializeField] UIBucket bucket;

	public T Append<T>(bool worldPositionStays = false) where T : UIPanel
	{
		T prefab = bucket.Fetch<T>();
		T ui = Instantiate(prefab, transform, worldPositionStays);
		return ui;
	}

	public void CloseAll()
	{
		GetComponentsInChildren<UIPanel>().Each(ui => ui.Close());
	}
}
