using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    [SerializeField] protected Item resource; // ��ȯ�� �ڿ�
    public MeleeItem requireMelee; // �ڿ��� ȹ���ϴµ� �ʿ��� ����

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

	public virtual bool Obtainable()
	{
        return select.CurrentSelected?.Item?.GetType() == requireMelee.GetType();
	}

	public abstract void OnStartObtain();
	public abstract void OnStopObtain();
	public abstract void Obtain();
}
