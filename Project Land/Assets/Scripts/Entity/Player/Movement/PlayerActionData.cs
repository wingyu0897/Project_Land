using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionData : MonoBehaviour
{
    public bool isActive = true;        // ������ �Է��� Ȱ��ȭ �Ǿ��ִ°�
    public bool isRolling = false;      // ������ �ִ°�
    public bool isAttacking = false;    // ���� ���ΰ�
    public bool isInteracting = false;  // ��ȣ�ۿ� ���ΰ�
    public bool canChange = true;       // ���� ����ִ� ������ ������ �����Ѱ�
    public bool canClick = true;        // Ŭ���� �����Ѱ�
    public bool isDead = false;         // �׾��°�
}
