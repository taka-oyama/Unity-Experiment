using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIDebugError : UIPanel
{
	[SerializeField] Text title;
	[SerializeField] Text sysInfo;
	[SerializeField] Text content;
	[SerializeField] Button closeButton;

	protected override void Awake()
	{
		base.Awake();
	}

	public void Init(ErrorCatcher.Info info, UnityAction onClose)
	{
		title.text = string.Format("{0} in Scene: {1}", info.type, info.sceneName);
		sysInfo.text = string.Join("\n",new []{
			string.Format("{0} - {1}", SystemInfo.operatingSystem, SystemInfo.deviceModel),
			string.Format("Bundle Version: {0}", Application.version),
			info.time.ToString(),
		});
		content.text = (info.condition + "\n\n" + info.trace).Replace("(at ", "\n(");
		closeButton.onClick.AddListener(Close);
		closeButton.onClick.AddListener(onClose);
	}
}
