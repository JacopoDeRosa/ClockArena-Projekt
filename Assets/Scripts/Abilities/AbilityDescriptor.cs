
[System.Serializable]
public class AbilityDescriptor
{
    public int tierIndex;
    public int abilityIndex;
    public bool available;

    public AbilityDescriptor(int tier, int ability)
    {
        tierIndex = tier;
        abilityIndex = ability;
        available = true;
    }
    public AbilityDescriptor(int tier, int ability, bool available)
    {
        tierIndex = tier;
        abilityIndex = ability;
        this.available = available;
    }
}
