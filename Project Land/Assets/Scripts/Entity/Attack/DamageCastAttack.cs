using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCastAttack : BaseAttack
{
	[SerializeField] private int damage;
	[SerializeField] private int attackRadius;
	[SerializeField] private float attackCooltime;
	private float prevAttackTime = 0;

	public DamageCaster damageCaster;

	public override void Attack()
	{
		print("Attack");
		damageCaster.SphereCastAll(attackRadius, damage);
	}

	public override bool CanAttack()
	{
		if (Time.time - prevAttackTime > attackCooltime)
		{
			prevAttackTime = Time.time;
			return true;
		}

		return false;
	}
}
