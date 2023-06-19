using System;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	[SerializeField] private Slider healthSliderRed;
	[SerializeField] private Slider healthSliderYellow;
	[SerializeField] private float maxHealth;
	[SerializeField] private float currentHealth;
	[SerializeField] private float healTime;
	[SerializeField] private float healAmount;
	private float current = 0;

	public Action OnDead = null;

	private Sequence seq;

	public void Init()
	{
		if (seq.IsActive())
			seq.Kill();
		currentHealth = maxHealth;
		healthSliderRed.value = 1f;
		healthSliderYellow.value = 1f;
	}

	private void Update()
	{
		current += Time.deltaTime;
		if (current > healTime)
		{
			if (currentHealth < maxHealth)
			{
				currentHealth += Time.deltaTime * healAmount;
				currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
				healthSliderRed.value = currentHealth / maxHealth;
				healthSliderYellow.value = currentHealth / maxHealth;
			}
		}
	}

	public void OnDamaged(int damage, Transform attacker)
	{
		if (currentHealth <= 0) 
			return;

		current = 0;

		currentHealth -= damage;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		//Tween t1 = DOTween.To(() => healthSliderRed.value, x => healthSliderRed.value = x, currentHealth / maxHealth, 0.3f).SetEase(Ease.OutSine);
		healthSliderRed.value = currentHealth / maxHealth;
		Tween t2 = DOTween.To(() => healthSliderYellow.value, x => healthSliderYellow.value = x, currentHealth / maxHealth, 0.3f).SetEase(Ease.OutSine);

		seq = DOTween.Sequence();
		//seq.Append(t1);
		seq.AppendInterval(0.75f);
		seq.Append(t2);

		if (currentHealth <= 0)
			OnDead?.Invoke();
	}
}
