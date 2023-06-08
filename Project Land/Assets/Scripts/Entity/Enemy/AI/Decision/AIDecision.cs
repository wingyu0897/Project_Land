using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected EnemyActionData actionData;
	protected EnemyBrain brain;
	public bool isReverse = false;

	private void Awake()
	{
		actionData = transform.parent.GetComponent<EnemyActionData>();
		brain = transform.parent.parent.GetComponent<EnemyBrain>();
	}

	public abstract bool Condition();
}
