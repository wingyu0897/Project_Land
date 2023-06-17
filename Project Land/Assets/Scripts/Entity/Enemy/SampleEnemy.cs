using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour, IDamageable
{
	private MeshRenderer meshRen;
	private MaterialPropertyBlock propertyBlock;
	private readonly int blinkHash = Shader.PropertyToID("_Blink");

	private void Awake()
	{
		meshRen = GetComponent<MeshRenderer>();
		propertyBlock = new MaterialPropertyBlock();
		meshRen.GetPropertyBlock(propertyBlock);
	}

	public void OnDamaged(int damage, Transform attacker)
	{
		propertyBlock.SetFloat(blinkHash, 1f);
		meshRen.SetPropertyBlock(propertyBlock);
		StartCoroutine(OnHit());
	}

	private IEnumerator OnHit()
	{
		yield return new WaitForSeconds(0.2f);

		propertyBlock.SetFloat(blinkHash, 0f);
		meshRen.SetPropertyBlock(propertyBlock);
	}
}
