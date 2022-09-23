using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GUIPerPet : MonoBehaviour
{
    [SerializeField]GameObject UIHandle;
    public AnimalBase animHandle;
    [SerializeField] Vector3 Offset;
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject StatusPanel;
    [SerializeField] List<TextMeshProUGUI> TextList;
    Transform ParentTransform;
    private void Awake()
    {
        animHandle = UIHandle.GetComponent<AnimalBase>();
        ParentTransform = UIHandle.transform;
        transform.SetParent(null);
    }
    private void OnEnable()
    {
        sellValue.text = animHandle.ValueEachAges[(int)animHandle.AniAge].ToString() + "G";
        HealthBar.SetActive(true);
        StatusPanel.SetActive(false);
        animHandle.HealbarAction += HandleHealtBarNormal;
        TextList[0].text = "Name: " + animHandle.name + "\n" +
            "Description: " + animHandle.animalDescription;

    }
    void UpdateStatus()
    {
        TextList[1].text = "Status: " + animHandle.AniAge.ToString();
        TextList[2].text = animHandle.CurrentHeath.ToString() + "/" + animHandle.MaxHealth.ToString();
    }
    [SerializeField] Image healthBarImage;
    void HandleHealtBarNormal()
    {
        healthBarImage.DOFillAmount(animHandle.GetCurrentHealt(),0.1f);
    }
    [SerializeField] Image helthStatus;
    void HandleHealthStatus()
    {
        helthStatus.DOFillAmount(animHandle.GetCurrentHealt(), 0.1f);
        TextList[2].text = animHandle.CurrentHeath.ToString() + "/" + animHandle.MaxHealth.ToString();
        TextList[1].text = "Status: " + animHandle.AniAge.ToString();
        sellValue.text = animHandle.ValueEachAges[(int)animHandle.AniAge].ToString() + "G";
    }
    private void Update()
    {
        transform.position = ParentTransform.position + Offset;
    }
    public void SelectPetAction()
    {
        HealthBar.SetActive(false);
        StatusPanel.SetActive(true);
        animHandle.HealbarAction -= HandleHealtBarNormal;
        animHandle.HealbarAction += HandleHealthStatus;
        UpdateStatus();

    }
    [SerializeField] TextMeshProUGUI sellValue;
    public void SellButtonPressed()
    {
        PlayerController.Instance.UpDateGold(-animHandle.ValueEachAges[(int)animHandle.AniAge]);
        animHandle.DeathAction();
    }
   
    private void OnDisable()
    {
        healthBarImage.DOKill();
        helthStatus.DOKill();
        animHandle.HealbarAction = null;
        Destroy(this.gameObject);
    }
    public void DeSelectPetAction()
    {
        animHandle.HealbarAction += HandleHealtBarNormal;
        animHandle.HealbarAction -= HandleHealthStatus;

        HealthBar.SetActive(true);
        StatusPanel.SetActive(false);
    }
    public void DeselectAction()
    {
        PlayerController.Instance.DeselectAll();
    }

}
