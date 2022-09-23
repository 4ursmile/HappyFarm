using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPerPet : MonoBehaviour
{
    [SerializeField]GameObject UIHandle;
    AnimalBase animHandle;
    
    private void Awake()
    {
        animHandle = UIHandle.GetComponent<AnimalBase>();
    }
    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

}
