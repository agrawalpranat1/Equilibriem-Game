using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;




public class CylinderSet : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField] public Scale RadiusFill = 0.5f;
    //[SerializeField] public Scale HeightFill = 0.5f;
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private GameObject ScriptAverager;
    [SerializeField] private UnityEvent onDeselected = null;
    [SerializeField] private BoxCollider BoxCylinderSet;



    // Start is called before the first frame update
    void Start()
    {
        
        float xy = UnityEngine.Random.Range(0f, 1f);
        float zscale = UnityEngine.Random.Range(0f, 1f);
        transform.position = new Vector3(transform.position.x, zscale, transform.position.y);
        transform.localScale = new Vector3(xy, zscale, xy);
        BoxCylinderSet.transform.localScale = new Vector3(xy, zscale, xy);
        BoxCylinderSet.transform.position = new Vector3(transform.position.x, zscale, transform.position.y);

        //HeightFill = UnityEngine.Random.Range(0f, 1f);
        //RadiusFill = UnityEngine.Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select()
    {
        onSelected?.Invoke();
    }

    public void Deselect()
    {
        onDeselected?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        List<CylinderSet> SelectedCylinderSet = ScriptAverager.GetComponent<SelectionAverager>().GetCylinderSet();
        if (eventData.button != PointerEventData.InputButton.Left) { return; }
        if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {

            ScriptAverager.GetComponent<SelectionAverager>().ClearCylinderSet();
            ScriptAverager.GetComponent<SelectionAverager>().AddCylinderSet(this);
        }
        else
        {
            ScriptAverager.GetComponent<SelectionAverager>().AddCylinderSet(this);
        }
    }

}
