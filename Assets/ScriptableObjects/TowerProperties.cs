using UnityEngine;

[CreateAssetMenu]
public class TowerProperties : ScriptableObject
{
    [SerializeField]
    ATTACK_MODE attackMode;

    [SerializeField]
    int damage;

    [SerializeField]
    float attackTick;

    [SerializeField]
    float attackRadius;

    public ATTACK_MODE AttackMode
    {
        get { return attackMode; }
        set { attackMode = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float AttackTick
    {
        get { return attackTick; }
        set { attackTick = value; }
    }
    public float AttackRadius
    {
        get { return attackRadius; }
        set { attackRadius = value; }
    }
}
