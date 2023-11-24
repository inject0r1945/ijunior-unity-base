using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RTS.Builds;
using Zenject;
using RTS.Resources;

namespace RTS.UI.Resources
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _detailsCountText;
        [SerializeField] private UnitsBase _unitsBase;

        private ResourcesStatistics _resourcesStatistics;

        [Inject]
        private void Construct(ResourcesStatistics resourcesStatistics)
        {
            _resourcesStatistics = resourcesStatistics;
        }

        private void OnEnable()
        {
            _resourcesStatistics.DetailsChanged += OnDetailsChanged;
        }

        private void OnDisable()
        {
            _resourcesStatistics.DetailsChanged -= OnDetailsChanged;
        }

        private void OnDetailsChanged(int resourcesCount)
        {
            _detailsCountText.text = resourcesCount.ToString();
        }
    }
}
