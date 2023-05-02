using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    [SerializeField] protected Item resource; // ��ȯ�� �ڿ�
    public Melee requireMelee; // �ڿ��� ȹ���ϴµ� �ʿ��� ����

    protected PlayerInput input;
    protected PlayerInventory inven;
	protected PlayerActionData actionData;
	protected PlayerMovement movement;

	protected virtual void Start()
	{
		input = Define.Instance.player.GetComponent<PlayerInput>();
		inven = Define.Instance.player.GetComponent<PlayerInventory>();
		actionData = Define.Instance.player.GetComponent<PlayerActionData>();
		movement = Define.Instance.player.GetComponent<PlayerMovement>();
	}

	public virtual bool Obtainable()
	{
        return inven.CurrentHoldSpace?.GetItemType() == requireMelee.GetType();
	}

	public abstract void OnStartObtain();
	public abstract void OnStopObtain();
	public abstract void Obtain();
}
