using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalShopUI : MonoBehaviour
{

    [SerializeField] public List<GameObject> uiElements;
    // Start is called before the first frame update
    void Start()
    {
        uiElements[0].SetActive(true);
        uiElements[1].SetActive(false);
    }
    private void OnEnable()
    {
        uiElements[0].SetActive(true);
        uiElements[1].SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShopButtonPressed()
    {
        uiElements[1].SetActive(true);
        uiElements[0].SetActive(false);
    }
    public void CloseButtonPressed()
    {
        uiElements[0].SetActive(true);
        uiElements[1].SetActive(false);
    }
    public void AnimalShopPressed()
    {
        uiElements[2].SetActive(true);
        uiElements[3].SetActive(false);
    }
    public void FoodShopPressed()
    {
        uiElements[3].SetActive(true);
        uiElements[2].SetActive(false);
    }
    
}
