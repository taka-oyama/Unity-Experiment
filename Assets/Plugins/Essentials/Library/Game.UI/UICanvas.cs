using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Game.UI
{
	[DisallowMultipleComponent]
	public sealed class UICanvas : MonoBehaviour
	{
		[SerializeField] Canvas canvas;
		[SerializeField] UIPanel[] uiPrefabs;

		public Canvas Canvas { get { return canvas; } }

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
		public bool Has<T>(bool includeInactive = false) where T : UIPanel
		{
			return canvas.GetComponentInChildren<T>(includeInactive) != null;
		}

		/// <summary>
		/// Find UI with the same name as the class.
		/// Will return null if UI is not found.
		/// </summary>
		public T Find<T>(bool includeInactive = false) where T : UIPanel
		{
			return canvas.GetComponentInChildren<T>(includeInactive);
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
		public int Count(bool includeInactive = false)
		{
			return canvas.GetComponentsInChildren<UIPanel>(includeInactive).Length;
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
			foreach(UIPanel uiPrefab in uiPrefabs) {
				if(uiPrefab is T) {
					prefab = uiPrefab as T;
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
			this.uiPrefabs = RemoveDuplicateAndSort(uiPrefabs);
		}

		T[] RemoveDuplicateAndSort<T>(T[] source) where T : UnityEngine.Object
		{
			var hashSet = new HashSet<T>();
			for(int i = 0; i < source.Length; i++) {
				if(source[i] == null) {
					continue;
				}
				if(hashSet.Contains(source[i])) {
					Debug.LogErrorFormat("{0} already exists in array!", source[i].name);
					source[i] = null;
					continue;
				}
				hashSet.Add(source[i]);
			}
			int currentCount = source.Length;
			T[] newSource = source.Where(o => o != null).OrderBy(o => o.name).ToArray();
			Array.Resize(ref newSource, currentCount);
			return newSource;
		}
	}
}
