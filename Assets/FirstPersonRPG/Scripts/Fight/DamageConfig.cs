﻿[System.Serializable]
public class DamageConfig
{
    public int minPhysical = 0;
    public int maxPhysical = 1;
    public int fire = 0;
    public int ice = 0;
    public int electro = 0;

    public DamageConfig Copy()
    {
        DamageConfig newConfig = new DamageConfig();
        newConfig.minPhysical = this.minPhysical;
        newConfig.maxPhysical = this.maxPhysical;
        newConfig.fire = this.fire;
        newConfig.ice = this.ice;
        newConfig.electro = this.electro;
        return newConfig;
    }
}
