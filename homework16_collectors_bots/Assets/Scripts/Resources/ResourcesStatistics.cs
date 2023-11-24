using System;
using UnityEngine;

namespace RTS.Resources
{
    public class ResourcesStatistics : MonoBehaviour
    {
        private int _detailsCount;

        public int DetailsCount => _detailsCount;

        public event Action<int> DetailsChanged;

        public void IncreaseResourceStatistic(Resource resource)
        {
            int count = 1;

            if (resource is Detail) 
            {
                IncreaseDetails(count);
            }
        }
    
        private void IncreaseDetails(int count)
            {
                if (count <= 0)
                    return;

                _detailsCount += count;

                DetailsChanged?.Invoke(_detailsCount);
            }
        }
    }