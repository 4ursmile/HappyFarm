using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "SaveFile", menuName = "SaveDataBase/SaveStorage", order = 1)]
public class AnimalStorage : ScriptableObject
{
    public List<GameObject> gameObjects;
    public int CurrentGold;

}
