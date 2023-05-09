using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PoolableMono
{
	public ItemDataSO data;

	protected Collider coll;
	protected Rigidbody rigid;

	protected bool isDrop = true;
	protected float dropTime;

	protected virtual void Awake()
	{
		coll = GetComponent<Collider>();
		rigid = GetComponent<Rigidbody>();
	}

	public virtual void OnPickUp()
	{
		isDrop = false;

		gameObject.SetActive(false);
		transform.SetParent(Define.Instance?.inventoryTrm);
		coll.enabled = false;
		rigid.useGravity = false;
		rigid.isKinematic = true;

		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;
	}

	public virtual void OnDrop()
	{
		isDrop = true;
		dropTime = Time.time;

		transform.position = Define.Instance.player.transform.position + Define.Instance.player.transform.forward * 1.5f;
		transform.eulerAngles = Vector3.zero;

		gameObject.SetActive(true);
		transform.SetParent(null);
		coll.enabled = true;
		rigid.useGravity = true;
		rigid.isKinematic = false;
	}

	public virtual void OnSelect()
	{
		gameObject.SetActive(true);
	}

	public virtual void OnDeselect()
	{
		gameObject.SetActive(false);
	}

	public override void Init()
	{
		isDrop = true;
		dropTime = Time.time;
		gameObject.SetActive(true);
		transform.SetParent(null);
		coll.enabled = true;
		rigid.useGravity = true;
		rigid.isKinematic = false;
	}

	protected virtual void Update()
	{
		if (isDrop)
		{
			if (Time.time - dropTime >= data.disappearTime)
			{
				print("disappear");
				PoolManager.Instance.Push(this);
			}
		}
	}
}
