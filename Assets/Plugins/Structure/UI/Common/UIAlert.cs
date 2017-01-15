using UnityEngine;
using UnityEngine.UI;

public class UIAlert : UIPanel
{
	[SerializeField] Text contentText;
	[SerializeField] Button okButton;

	protected override void Awake()
	{
		base.Awake();
		okButton.onClick.AddListener(Close);
	}
}
