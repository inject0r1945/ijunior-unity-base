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

        private ResourcesWarehouse _warehouse;

        [Inject]
        private void Construct(ResourcesWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        private void OnEnable()
        {
            _warehouse.DetailsChanged += OnDetailsChanged;
        }

        private void OnDisable()
        {
            _warehouse.DetailsChanged -= OnDetailsChanged;
        }

        private void OnDetailsChanged(int resourcesCount)
        {
            _detailsCountText.text = resourcesCount.ToString();
        }
    }
}
