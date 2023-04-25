using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public void SphereCast(float radius)
	{
		RaycastHit[] hits = Physics.SphereCastAll(transform.position - -transform.forward * 1, radius, transform.forward, 1f);

		foreach (RaycastHit hit in hits)
		{
			if (hit.transform.TryGetComponent(out IDamageable damageable))
			{
				if (Vector3.Dot(transform.forward, hit.collider.transform.position - transform.position) > 0)
					damageable.OnDamaged();
			}
		}
	}
}
