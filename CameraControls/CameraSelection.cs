using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//the CameraSelection script handles the selection UI and units within the bounding box of the selection box
public class CameraSelection : MonoBehaviour
{
    [Header("Player Info")]
    public Faction PlayerFaction;
    
    [Header("Generic info")]
    public RectTransform UnitSelectionArea;

    private Vector2 _startPosition;
    private Vector2 _mousePosition;
    private Camera _mainCamera;
    private RaycastHit hit;
        

    private void Awake()
    {
        _mainCamera = Camera.main;
        PlayerFaction = GetComponent<Faction>();
    }

    public void Selection(Vector2 mousePosition, InputAction action)
    {
        _mousePosition = mousePosition;
        
        if (action.WasPressedThisFrame())
        {
            StartSelectionArea();
        }
        else if (action.WasReleasedThisFrame())
        {
            ClearSelectionArea();
        }
        else if (action.IsPressed())
        {
            UpdateSelectionArea();
        }
    }
    
    private void StartSelectionArea()
    {

        PlayerFaction.DeselectAll();

        PlayerFaction.SelectedUnits.Clear();
        UnitSelectionArea.gameObject.SetActive(true);
        
        _startPosition = _mousePosition;
        
        UpdateSelectionArea();
    }

    private void UpdateSelectionArea()
    {
        Vector2 mousePosition = _mousePosition;

        float areaWidth = mousePosition.x - _startPosition.x;
        float areaHeight = mousePosition.y - _startPosition.y;

        UnitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
        UnitSelectionArea.anchoredPosition = _startPosition + new Vector2(areaWidth / 2, areaHeight / 2);
    }

    private void ClearSelectionArea()
    {
        UnitSelectionArea.gameObject.SetActive(false);

        if (UnitSelectionArea.sizeDelta.magnitude == 0)
        {
            Ray ray = _mainCamera.GetComponent<Camera>().ScreenPointToRay(_mousePosition);

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity)) { return; }
            
            SelectSingle();
            return;
        }

        Vector2 min = UnitSelectionArea.anchoredPosition - (UnitSelectionArea.sizeDelta / 2);
        Vector2 max = UnitSelectionArea.anchoredPosition + (UnitSelectionArea.sizeDelta / 2);

        foreach (var unit in PlayerFaction.GetUnits())
        {
            if (PlayerFaction.SelectedUnits.Contains(unit)) { continue; }
            Vector3 screenPosition = _mainCamera.GetComponent<Camera>().WorldToScreenPoint(unit.transform.position);

            if (screenPosition.x > min.x && screenPosition.x < max.x && screenPosition.y > min.y && screenPosition.y < max.y)
            {
                PlayerFaction.SelectedUnits.Add(unit);
                unit.Select();
            }
        }

    }

    private void SelectSingle()
    {
        if (hit.collider.TryGetComponent(out UnitController unit))
        {
            PlayerFaction.SelectUnit(unit);
        }
        if (hit.collider.TryGetComponent(out BuildingController building))
        {
            PlayerFaction.SelectBuilding(building);
        }
    }
}
