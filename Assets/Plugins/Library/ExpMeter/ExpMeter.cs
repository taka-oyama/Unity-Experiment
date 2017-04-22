using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class ExpMeter : MonoBehaviour
{
	int[] expList;
	int currentIndex;
	int currentMinExp;
	int currentMaxExp;
	float currentExp;
	int targetExp;

	public virtual ExpMeter Setup(int[] expRangeList, int currentExp)
	{
		this.expList = expRangeList;
		this.targetExp = currentExp;
		this.currentExp = currentExp;

		for(int i = 0; i < expRangeList.Length - 1; i++) {
			this.currentIndex = i;
			if(targetExp.IsBetween(expRangeList[i], expRangeList[i + 1])) {
				break;
			}
		}
		this.currentMinExp = expRangeList[currentIndex];
		this.currentMaxExp = expRangeList[currentIndex + 1];
		return this;
	}

	public int Level {
		get { return currentIndex + 1; }
	}

	public int MinExp {
		get { return 0; }
	}

	public int MaxExp {
		get { return currentMaxExp - currentMinExp; }
	}

	public int Exp {
		get { return (int)currentExp - currentMinExp; }
	}

	public float Ratio {
		get { return (currentExp - currentMinExp) / MaxExp; }
	}

	public void Add(int amount, float duration = 1f)
	{
		StartCoroutine(AddingCoroutine(amount, duration));
	}

	IEnumerator AddingCoroutine(int amount, float duration)
	{
		this.targetExp += amount;
		float incrRate = (amount / duration) * Time.fixedDeltaTime;

		while(currentExp < targetExp) {
			this.currentExp += IncrementTween(incrRate);
			if(CanLevelUp()) {
				this.currentExp = currentMaxExp;
				yield return StartCoroutine(OnBeforeLevelUp());
				this.currentIndex += 1;
				this.currentMinExp = expList[currentIndex];
				this.currentMaxExp = expList[currentIndex + 1];
				yield return StartCoroutine(OnAfterLevelUp());
			}
			yield return new WaitForFixedUpdate();
		}
		this.currentExp = targetExp;
	}

	protected virtual float IncrementTween(float value)
	{
		return value;
	}

	protected virtual IEnumerator OnBeforeLevelUp()
	{
		Debug.LogFormat("Exp: {0} MinExp:{1} MaxExp:{2} Ratio:{3} Level:{4}", Exp, MinExp, MaxExp, Ratio, Level);
		yield break;
	}

	protected virtual IEnumerator OnAfterLevelUp()
	{
		Debug.LogFormat("Exp: {0} MinExp:{1} MaxExp:{2} Ratio:{3} Level:{4}", Exp, MinExp, MaxExp, Ratio, Level);
		yield break;
	}

	bool CanLevelUp()
	{
		return currentExp > currentMaxExp && HasNextLevel();
	}

	bool HasNextLevel()
	{
		return currentIndex < expList.Length;
	}
}
