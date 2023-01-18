using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// attacker��  victim���� amount��ŭ�� ���ظ� ��
    /// </summary>
    /// <param name="attacker">�����ϴ� Object</param>
    /// <param name="victim">���ع޴� Object</param>
    /// <param name="amount">���ط�</param>
    public void Damage(GameObject attacker, GameObject victim, float amount);
}
