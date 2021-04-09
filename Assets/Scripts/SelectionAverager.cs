using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionAverager : MonoBehaviour
{
    private List<CylinderSet> SelectedUnits = new List<CylinderSet>();
    private Camera mainCamera;
    private float Volume = 0;
    private float Area = 0;
    [SerializeField] private LayerMask layerMask = new LayerMask();


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SelectedUnits.Count);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSelection();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            SelectUnit();
        }
        else if (Mouse.current.leftButton.isPressed)
        {

        }
        if (Input.GetButtonDown("Jump"))
        {
            Averager();
        }




    }
    public void SelectUnit() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask)) { return; }

        if (!hit.collider.TryGetComponent<CylinderSet>(out CylinderSet CylinderSetObject)) { return; }

        if (SelectedUnits.Contains(CylinderSetObject)){ return; }

        SelectedUnits.Add(CylinderSetObject);

        foreach (CylinderSet selectedCylinderSet in SelectedUnits)
        {
            selectedCylinderSet.Select();
        }
        return;
    }
    public List<CylinderSet> GetCylinderSet() 
    {
        return SelectedUnits;
    }
    public void AddCylinderSet(CylinderSet InputSelection)
    {
        SelectedUnits.Add(InputSelection);
    }
    public void ClearCylinderSet()
    {
        SelectedUnits.Clear(); 
    }
    private void StartSelection()
    {
        if (!Keyboard.current.leftShiftKey.isPressed)
        {


            foreach (CylinderSet selectedCylinderSet in SelectedUnits)
            {
                selectedCylinderSet.Deselect();
            }
            SelectedUnits.Clear();
        }

    }
    private void Averager() 
    {

            if (SelectedUnits == null) { return; }
            foreach (CylinderSet selectedCylinderSet in SelectedUnits)

            {
                Volume += (Mathf.Pow(selectedCylinderSet.transform.localScale.x, 2) + Mathf.Pow(selectedCylinderSet.transform.localScale.z, 2)) * selectedCylinderSet.transform.localScale.y;
                Area += (Mathf.Pow(selectedCylinderSet.transform.localScale.x, 2) + Mathf.Pow(selectedCylinderSet.transform.localScale.z, 2));
            }
            float height = Volume / Area;
            foreach (CylinderSet selectedCylinderSet in SelectedUnits)
            {
                selectedCylinderSet.transform.localScale = new Vector3(selectedCylinderSet.transform.localScale.x, height, selectedCylinderSet.transform.localScale.z);
                selectedCylinderSet.transform.position = new Vector3(selectedCylinderSet.transform.position.x, height, selectedCylinderSet.transform.position.z);
                selectedCylinderSet.Deselect();
            Volume = 0;
            Area = 0;
        }
            SelectedUnits.Clear();


    }
}
