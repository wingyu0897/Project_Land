using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;

	public Action OnDead = null;

	public void Init()
	{
		currentHealth = maxHealth;
	}

	public void OnDamaged(int damage)
	{
		if (currentHealth <= 0) 
			return;
		currentHealth -= damage;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		if (currentHealth <= 0)
			OnDead?.Invoke();
	}
}
