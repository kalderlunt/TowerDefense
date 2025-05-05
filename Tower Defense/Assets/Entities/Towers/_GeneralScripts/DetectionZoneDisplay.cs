using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(DecalProjector))]
public class DetectionZoneDisplay : MonoBehaviour
{
    private DecalProjector decalProjector;
    public bool isVisible { get; private set; }

    private Tower parentTower;
    private TowerData data;

    private void Awake()
    {
        decalProjector = GetComponent<DecalProjector>();
        Assert.IsNotNull(decalProjector, "Decal Projector not found");
        
        parentTower = GetComponentInParent<Tower>();
        Assert.IsNotNull(parentTower, "Parent Tower not found");

        data = parentTower.data;
    }

    private void OnEnable()
    {
        decalProjector.enabled = false;
        isVisible = false;

        UpdateRangeVisual(data);
    }

    public void UpdateRangeVisual(TowerData data)
    {
        Assert.IsNotNull(data, "Tower Data not found");

        float rangeDiameter = data.baseRange ;
        decalProjector.size = new Vector3(rangeDiameter, rangeDiameter, decalProjector.size.z);
    }

    public void ShowZone(TowerData data)
    {
        UpdateRangeVisual(data);

        decalProjector.enabled = true;
        isVisible = true;
    }

    public void HideZone()
    {
        decalProjector.enabled = false;
        isVisible = false;
    }

    public void ChangeColor(Color color)
    {
        decalProjector.material.SetColor("_Color", color);
    }
    public void ResetColor()
    {
        decalProjector.material.SetColor("_Color", Color.white);
    }
}