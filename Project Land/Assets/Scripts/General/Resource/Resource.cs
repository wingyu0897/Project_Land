using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : PoolableMono
{
    [SerializeField] protected Item resource; // 반환할 자원
	[SerializeField] public float minimumSpace = 1f;

    protected PlayerInput input;
    protected SelectItem select;
	protected PlayerActionData actionData;
	protected PlayerMovement movement;

	protected virtual void Start()
	{
		gameObject.layer = LayerMask.NameToLayer("Resource");
		input = Define.Instance.player.GetComponent<PlayerInput>();
		select = Define.Instance.player.GetComponent<SelectItem>();
		actionData = Define.Instance.player.GetComponent<PlayerActionData>();
		movement = Define.Instance.player.GetComponent<PlayerMovement>();
	}

	public abstract bool Obtainable();
	public abstract void Obtain();

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (UnityEditor.Selection.activeGameObject == gameObject)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, minimumSpace);
			Gizmos.color = Color.white;
		}
	}
#endif
}
