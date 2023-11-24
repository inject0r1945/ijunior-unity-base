using System;
using UnityEngine;

namespace RTS.Resources
{
    public class ResourcesWarehouse : MonoBehaviour
    {
        private int _detailsCount;

        public int DetailsCount => _detailsCount;

        public event Action<int> DetailsChanged;

        public void AddResource(Resource resource)
        {
            int count = 1;

            if (resource is Detail) 
            {
                AddDetails(count);
            }
        }
    
        private void AddDetails(int count)
            {
                if (count <= 0)
                    return;

                _detailsCount += count;

                DetailsChanged?.Invoke(_detailsCount);
            }
        }
    }