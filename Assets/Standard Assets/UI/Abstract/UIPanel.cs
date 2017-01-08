using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public abstract class UIPanel : MonoBehaviour
{
	CanvasGroup canvasGroup;
	Animator animator;
	[HideInInspector] public UnityEvent onOpened;
	[HideInInspector] public UnityEvent onClosed;

	protected virtual void Awake()
	{
		animator = GetComponent<Animator>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	protected virtual void Start()
	{
		canvasGroup.interactable = false;
		StartCoroutine(OpenCoroutine());
	}

	IEnumerator OpenCoroutine()
	{
		yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
		canvasGroup.interactable = true;
		onOpened.Invoke();
	}

	public virtual void Close()
	{
		canvasGroup.interactable = false;
		animator.SetTrigger("Close");
		StartCoroutine(CloseCoroutine());
	}

	IEnumerator CloseCoroutine()
	{
		yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Remove"));
		onClosed.Invoke();
		Destroy(gameObject);
	}
}
