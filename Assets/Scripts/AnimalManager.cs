using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] List<GameObject> animalPreList;
    public static AnimalManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyAnimal(int type)
    {
        SpawnAnimal(type);
    }
    void SpawnAnimal(int type)
    {
        GameObject gameObject = Instantiate(animalPreList[(int)type], CreateMovePoint.Instance.GetPoint(), Quaternion.identity);
        gameObject.transform.SetParent(this.transform);
    }
}
