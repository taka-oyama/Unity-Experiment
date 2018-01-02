using UnityEngine;
using UnityEngine.UI;

public class UIModal : UIPanel
{
	[SerializeField] Text contentText;
	[SerializeField] Button okButton;
	[SerializeField] Button cancelButton;

	protected override void Awake()
	{
		base.Awake();
		okButton.onClick.AddListener(Close);
		cancelButton.onClick.AddListener(Close);
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		okButton.onClick.RemoveListener(Close);
		cancelButton.onClick.RemoveListener(Close);
	}
}
