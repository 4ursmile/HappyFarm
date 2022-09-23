using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DownArrowUIHandle : MonoBehaviour
{
    [SerializeField] GameObject CanvasUI;
    [SerializeField] Vector3 offset;
    public static DownArrowUIHandle Instance;
    bool isFirst = true;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CanvasUI.SetActive(false);
    }
    public void SetUIArrow(Transform Othertransform)
    {
        
        if (isFirst)
        {
            isFirst = false;
            CanvasUI.SetActive(true);
            transform.position =  Othertransform.position + offset;
        }
        transform.DOMove(Othertransform.position + offset, 0.1f).OnComplete(() => SetParent(Othertransform));

    }
    Transform TransFormSelect;
    void SetParent(Transform otherTransform)
    {
        TransFormSelect = otherTransform;
    }
    public void DeActive()
    {
        isFirst = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (TransFormSelect !=  null)
        transform.position = TransFormSelect.position + offset;
    }

}
