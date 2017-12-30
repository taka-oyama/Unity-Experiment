using UnityEngine;
using Game;

public sealed class TitleScene : SceneBase
{
	protected override void Run()
	{
		foreach (int index in IntRange.Inclusive(1, 10)) {
			Debug.Log(index);
		}
	}
}
