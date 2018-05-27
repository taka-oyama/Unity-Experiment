namespace UnityEngine
{
	public static class GameObjectExtension
	{
		public static GameObject Append(this GameObject parent, GameObject child, bool worldPositionStays = true)
		{
			child.transform.SetParent(parent.transform, worldPositionStays);
			return child;
		}

		public static GameObject Append(this GameObject parent, MonoBehaviour child, bool worldPositionStays = true)
		{
			child.transform.SetParent(parent.transform, worldPositionStays);
			return child.gameObject;
		}
	}
}
