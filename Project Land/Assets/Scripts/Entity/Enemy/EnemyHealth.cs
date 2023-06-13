using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;

	private SkinnedMeshRenderer meshRen;
	private MaterialPropertyBlock propertyBlock;
	private readonly int colorHash = Shader.PropertyToID("_Color");

	public UnityEvent OnDie = null;

	private void Awake()
	{
		meshRen = GetComponentInChildren<SkinnedMeshRenderer>();
		propertyBlock = new MaterialPropertyBlock();
		meshRen.GetPropertyBlock(propertyBlock);

		Init();
	}

	public void Init()
	{
		currentHealth = maxHealth;

		propertyBlock.SetColor(colorHash, Color.white);
		meshRen.SetPropertyBlock(propertyBlock);
	}

	public void OnDamaged(int damage, Transform attacker)
	{
		currentHealth -= damage;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		if (currentHealth == 0)
		{
			OnDie?.Invoke();
		}
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
