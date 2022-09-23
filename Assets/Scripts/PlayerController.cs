using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void SelectbleAction();
    public SelectbleAction selectbleAction; 
    public static PlayerController Instance;
    [SerializeField] Camera mainCamera;
    public List<Iselectable> iselectables;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        iselectables = new List<Iselectable>();
        selectbleAction = null;
    }

    // Update is called once per frame
    void Update()
    {
       selectbleAction?.Invoke();
        if (Input.GetButtonDown("Fire1"))
        {
            HandleSelection();
        }
    }
    [SerializeField] LayerMask Selectble;

    void HandleSelection()
    {
        RaycastHit hit;
        Ray inputRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(inputRay,out hit,Mathf.Infinity, Selectble))
        {
            foreach (var item in iselectables)
            {
                item.DeSelect();
            }
            iselectables.Clear();
            iselectables.Add(hit.collider.gameObject.GetComponent<Iselectable>());
            hit.collider.gameObject.GetComponent<Iselectable>().Select();
        }
    }
    public void DeselectAll()
    {
        foreach (var item in iselectables)
        {
            item.DeSelect();
        }
        DownArrowUIHandle.Instance.DeActive();
    }
}
