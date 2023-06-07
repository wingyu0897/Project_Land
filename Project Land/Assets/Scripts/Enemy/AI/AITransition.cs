using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AITransition : MonoBehaviour
{
    protected EnemyBrain brain;
    public BaseAIState nextState;
    public bool isReverse = false;

	private void Awake()
	{
		brain = transform.parent.parent.GetComponent<EnemyBrain>();
	}

	public abstract bool Change();
}
