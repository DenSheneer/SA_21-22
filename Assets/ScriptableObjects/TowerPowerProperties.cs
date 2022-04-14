using UnityEngine;

[System.Serializable]
public class TowerPowerProperties : ScriptableObject
{
    public string tierName;
    public uint costs;
    public ATTACK_MODE attackMode;
    public int damage;
    public float attackTick;
    public float attackRadius;
    public POISON_EFFECT_STRENGTH poisonDamage;
    public float aoeAttackRange;

}
