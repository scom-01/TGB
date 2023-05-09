using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Unit unit;
    private UnitStats stats;
    [SerializeField] private RectTransform healthbarTrasform;
    [SerializeField] private float ZeroPosX;
    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponentInParent<Unit>();
        stats = unit.Core.GetCoreComponent<UnitStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats == null)
        {
            if (unit == null)
            {
                unit = GetComponentInParent<Unit>();
            }
            stats = unit.Core.GetCoreComponent<UnitStats>();
        }
        healthbarTrasform.anchoredPosition = new Vector2(ZeroPosX * (1 - (stats.CurrentHealth / stats.StatsData.MaxHealth)), 0);
    }
}
