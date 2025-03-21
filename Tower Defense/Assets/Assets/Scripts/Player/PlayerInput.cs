using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Assets.Scripts.Managers;

public class PlayerInput : MonoBehaviour
{
    public static event Action<GameObject> OnTowerSelected;
    public static event Action<GameObject> OnTowerDeselected;

    [SerializeField] private LayerMask maskToExclude;
    [SerializeField] private InGameInventory playerInventory;
    
    private GameObject targetSelected;
    private Vector2 mousePosition;

    public void Select(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        UpdateSelection();
    }

    private void UpdateSelection()
    {
        
        if (playerInventory.PreviewTower != null) return;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~maskToExclude))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1);
            
            GameObject newTarget = hit.collider.gameObject;
            
            if (newTarget.GetComponent<Tower>())
            {
                if (targetSelected == newTarget)
                {
                    return;
                }

                ChangeTarget(newTarget);
                return;
            }
            
            DeselectCurrentTower();
        }
    }

    private void ChangeTarget(GameObject newTarget)
    {
        if (targetSelected == newTarget)
        {
            return;
        }

        if (targetSelected != null)
            targetSelected.GetComponent<TowerSelectable>().Deselect();

        targetSelected = newTarget;
        
        if (targetSelected != null)
            targetSelected.GetComponent<TowerSelectable>().Select();
    }

    public void Deselect(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if (targetSelected != null)
        {
            OnTowerDeselected?.Invoke(targetSelected);
            targetSelected = null;
        }
    }

    private void DeselectCurrentTower()
    {
        if (targetSelected == null)
        {
            return;
        }

        targetSelected.GetComponent<TowerSelectable>().Deselect();
        targetSelected = null;
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
            //OnMouseMove?.Invoke(mouseWorldPosition);
        }

        mousePosition = context.ReadValue<Vector2>();
        //Debug.Log($"mouse World Position : {mouseWorldPosition}");
    }

    public void CancelPlaceTower(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.instance.onCancelPlaceTower?.Invoke();
        }
    }





    /*public static event Action<Vector3> OnMouseMove;*/

    // faire une fonction qui permet de mettre a jour la targetToucher et si celle d'une tower jouer son OnSelect et desactiver l'ancienne s'il clique sur une autre tower et allumer la nouvelle
}