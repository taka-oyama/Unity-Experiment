namespace UnityEngine
{
	public static class MonoBehaviourExtension
	{
		public static MonoBehaviour Append(this MonoBehaviour parent, MonoBehaviour child, bool worldPositionStays = true)
		{
			child.transform.SetParent(parent.transform, worldPositionStays);
			return child;
		}

		public static T FetchComponent<T>(this MonoBehaviour go) where T : MonoBehaviour
		{
			T behaviour = go.GetComponent<T>();
			if(behaviour == null) {
				string message = string.Format("Component<{0}> not found on GameObject({1})", typeof(T).Name, go.name);
				throw new UnityException(message);
			}
			return behaviour;
		}

		public static bool HasComponent<T>(this MonoBehaviour go) where T : MonoBehaviour
		{
			return go.GetComponent<T>() != null;
		}

		public static void RemoveComponent<T>(this MonoBehaviour go) where T : MonoBehaviour
		{
			Object.Destroy(go.GetComponent<T>());
		}

		public static void RemoveComponentImmediate<T>(this MonoBehaviour go) where T : MonoBehaviour
		{
			Object.DestroyImmediate(go.GetComponent<T>());
		}
	}
}
