using UnityEngine;

[System.Serializable]
public class HitInfo{
    public enum HitSources
    {
        None = 0,
        Player = 1,
        Enemy = 2,
        Enviroment = 3
    }

    public DamageConfig damage;
    public Vector3 point;
    public bool isShowEffect = true;
    public HitSources source;

    public HitInfo() { }

    public HitInfo(DamageConfig damage, Vector3 point, bool isShowEffect, HitSources source)
    {
        this.damage = damage;
        this.point = point;
        this.isShowEffect = isShowEffect;
        this.source = source;
    }
}
