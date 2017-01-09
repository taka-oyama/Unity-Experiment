using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIDebugError : UIPanel
{
	[SerializeField] Text title;
	[SerializeField] Text content;
	[SerializeField] Button closeButton;

	protected override void Awake()
	{
		base.Awake();
	}

	public void Init(ErrorCatcher.Info info, UnityAction onClose)
	{
		title.text = string.Format("{0} Info - {1}", info.type, Application.productName);
		content.text = info.condition + "\n\n" + info.trace;
		closeButton.onClick.AddListener(Close);
		closeButton.onClick.AddListener(onClose);
	}
}
