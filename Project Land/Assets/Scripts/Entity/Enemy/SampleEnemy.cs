using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour, IDamageable
{
	private MeshRenderer meshRen;
	private MaterialPropertyBlock propertyBlock;
	private readonly int colorHash = Shader.PropertyToID("_Color");

	private void Awake()
	{
		meshRen = GetComponent<MeshRenderer>();
		propertyBlock = new MaterialPropertyBlock();
		meshRen.GetPropertyBlock(propertyBlock);
	}

	public void OnDamaged(int damage, Transform attacker)
	{
		propertyBlock.SetColor(colorHash, Color.red);
		meshRen.SetPropertyBlock(propertyBlock);
		StartCoroutine(OnHit());
	}

	private IEnumerator OnHit()
	{
		yield return new WaitForSeconds(0.2f);

		propertyBlock.SetColor(colorHash, Color.white);
		meshRen.SetPropertyBlock(propertyBlock);
	}
}
