using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public BaseAIState nextState;
	public List<AIDecision> decisions;

    public bool CanChange()
	{
		bool change = true;

		foreach (AIDecision de in decisions)
		{
			change = de.Condition();
			if (change == false)
				break;
		}
		return change;
	}
}
