using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
	public abstract bool CanAttack();
	public abstract void Attack();
}
