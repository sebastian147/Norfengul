using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType {Heal, Effect, Unknown, Venom}
public enum PotionQuality {Poor, Normal, Good, Excellent}
public class Potions : Items
{
    public PotionType potionType;
    public int AmountEffect;
    public PotionQuality potionQuality;
}
