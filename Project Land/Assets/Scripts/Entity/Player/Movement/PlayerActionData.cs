using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionData : MonoBehaviour
{
    public bool isActive = true;        // 움직임 입력이 활성화 되어있는가
    public bool isRolling = false;      // 구르고 있는가
    public bool isAttacking = false;    // 공격 중인가
    public bool isObtaining = false;
    public bool canChange = true;       // 현재 들고있는 아이템 변경이 가능한가
    public bool canClick = true;        // 클릭이 가능한가
}
