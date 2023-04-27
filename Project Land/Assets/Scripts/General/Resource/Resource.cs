using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private Item resource; // 반환할 자원
    [SerializeField] private Melee requireMelee; // 자원을 획득하는데 필요한 도구
}
