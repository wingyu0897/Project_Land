using System;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	[SerializeField] private Slider healthSliderRed;
	[SerializeField] private Slider healthSliderYellow;
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;

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

	public void OnDamaged(int damage, Transform attacker)
	{
		if (currentHealth <= 0) 
			return;
		currentHealth -= damage;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		Tween t1 = DOTween.To(() => healthSliderRed.value, x => healthSliderRed.value = x, (float)currentHealth / maxHealth, 0.3f).SetEase(Ease.OutSine);
		Tween t2 = DOTween.To(() => healthSliderYellow.value, x => healthSliderYellow.value = x, (float)currentHealth / maxHealth, 0.3f).SetEase(Ease.OutSine);

		seq = DOTween.Sequence();
		seq.Append(t1);
		seq.AppendInterval(0.3f);
		seq.Append(t2);

		if (currentHealth <= 0)
			OnDead?.Invoke();
	}
}
