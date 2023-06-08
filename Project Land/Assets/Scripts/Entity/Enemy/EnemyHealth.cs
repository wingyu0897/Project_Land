using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;

	public UnityEvent OnDie = null;

	private void Awake()
	{
		Init();
	}

	public void Init()
	{
		currentHealth = maxHealth;
	}

	public void OnDamaged(int damage)
	{
		currentHealth -= damage;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		if (currentHealth == 0)
		{
			OnDie?.Invoke();
		}
	}
}
