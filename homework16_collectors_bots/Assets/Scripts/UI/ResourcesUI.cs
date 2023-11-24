using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RTS.Builds;
using Zenject;
using RTS.Resources;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _detailsCountText;
    [SerializeField] private UnitsBase _unitsBase;

    private ResourcesStatistics _resourcesStatistict;

    [Inject]
    private void Construct(ResourcesStatistics resourcesStatistict)
    {
        _resourcesStatistict = resourcesStatistict;
    }

    private void OnEnable()
    {
        _resourcesStatistict.DetailsChanged += OnDetailsChanged;
    }

    private void OnDisable()
    {
        _resourcesStatistict.DetailsChanged -= OnDetailsChanged;
    }

    private void OnDetailsChanged(int resourcesCount)
    {
        _detailsCountText.text = resourcesCount.ToString();
    }
}
