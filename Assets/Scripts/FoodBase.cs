using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodBase : MonoBehaviour
{
    
    FoodType type;
}
public enum FoodType
{
    Apple, Lemon, Banana, Tomato
}