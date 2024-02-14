using RTS.Builds.ResourcesBaseBuild.StateMachine;
using RTS.Core;
using RTS.Detectors;
using RTS.Management;
using RTS.Resources;
using RTS.Units;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace RTS.Builds.ResourcesBaseBuild
{
    [RequireComponent(typeof(ResourcesDetector))]
    public class ResourcesBase : Build, IResourceReceiver, IResourceCounter
    {
        [Title("Resource Collectors Units Settings")]
        [SerializeField, MinValue(0)] private int _startDetailsCount = 9;
        [SerializeField, MinValue(0)] private int _resourceCollectorCost = 3;
        [SerializeField] private LayerMask _environmentObjectsMask;

        [Title("Build Settings")]
        [SerializeField, MinValue(0)] private int _buildFlagsCount = 1;

        [SerializeField, MinValue(1)] private int _resourceBaseCost = 5;
        [SerializeField, Required, AssetsOnly] private BuildFlag _buildFlagPrefab;
        [SerializeField] private LayerMask _flagSurfaceMask = ~0;

        private ResourcesWarehouse _resourcesWarehouse;
        private ResourcesDetector _resourcesDetector;
        private FlagSetter _flagSetter;
        private ResourcesBaseStateMachine _stateMachine;
        private ResourceCollectorsUnitsFabric _unitFabric;
        private BuildFlag[] _totalBuildFlags;
        private ResourceCollectorsGarage _unitsGarage;
        private BuildResourceBaseContractor _buildContractor;

        public void Initialize(UnitsConfigurations unitsConfigurations, ScreenObjectMover screenObjectMover, 
            ResourcesBaseFabric resourcesBaseFabric, ICoroutine coroutiner)
        {
            SelectableModel.Initialize();

            InitializeUnitsFabric(unitsConfigurations);
            InitializeWarehouse();
            InitializeFlagFunctions(screenObjectMover);
            InitializeCollectResourcesFunctions();
            InitializeGarageAndContractors(resourcesBaseFabric);
            InitializeStateMachine(coroutiner);
        }

        public event Action<int> DetailsChanged;

        public int DetailsCount => _resourcesWarehouse.DetailsCount;

        public bool CanCreateResourceCollector => _unitsGarage.CanCreateResourceCollector;


        private void OnEnable()
        {
            _resourcesWarehouse.DetailsChanged += OnDetailsChange;
        }

        private void OnDisable()
        {
            _resourcesWarehouse.DetailsChanged -= OnDetailsChange;
        }

        private void OnDestroy()
        {
            _stateMachine.Dispose();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public bool TryReceiveResource(ResourcesCollectorUnit unit, Resource resource)
        {
            if (_unitsGarage.IsOwnedUnit(unit) == false)
                return false;

            _resourcesWarehouse.AddResource(resource);
            Destroy(resource.gameObject);

            return true;
        }

        public bool TryStartSetFlagBehaviour()
        {
            return _flagSetter.TryStartSetFlagBehaviour();
        }

        public bool TryCreateResourceCollector()
        {
            return _unitsGarage.TryCreateResourceCollector();
        }

        public void Take(ResourcesCollectorUnit unit)
        {
            unit.Initialize(SelectableModel.ModelCollider);
            _unitsGarage.Take(unit);
        }

        private void InitializeUnitsFabric(UnitsConfigurations unitsConfigurations)
        {
            Vector3 baseSize = SelectableModel.ModelMeshFilter.mesh.bounds.size;

            _unitFabric = new ResourceCollectorsUnitsFabric(unitsConfigurations, baseSize, 
                SelectableModel.ModelCollider, _environmentObjectsMask);
        }

        private void InitializeWarehouse()
        {
            _resourcesWarehouse = new ResourcesWarehouse();
            _resourcesWarehouse.AddDetails(_startDetailsCount);
        }

        private void InitializeCollectResourcesFunctions()
        {
            _resourcesDetector = GetComponent<ResourcesDetector>();
            _resourcesDetector.Initialize();
        }

        private void InitializeFlagFunctions(ScreenObjectMover screenObjectMover)
        {
            _totalBuildFlags = new BuildFlag[_buildFlagsCount];

            for (int x = 0; x < _buildFlagsCount; x++)
            {
                _totalBuildFlags[x] = Instantiate(_buildFlagPrefab, transform);
                _totalBuildFlags[x].Initialize();
            }

            _flagSetter = new FlagSetter(_totalBuildFlags, screenObjectMover, _flagSurfaceMask);
        }

        private void InitializeGarageAndContractors(ResourcesBaseFabric resourcesBaseFabric)
        {
            _unitsGarage = new ResourceCollectorsGarage(_resourcesWarehouse, _resourceCollectorCost, _unitFabric, SelectableModel.transform);

            _buildContractor = new BuildResourceBaseContractor(_unitsGarage, _resourcesWarehouse, _resourceBaseCost, 
                _totalBuildFlags, resourcesBaseFabric, transform.parent);
        }

        private void InitializeStateMachine(ICoroutine coroutiner)
        {
            _stateMachine = new ResourcesBaseStateMachine(_resourcesDetector, _totalBuildFlags, _unitsGarage, _buildContractor, coroutiner);
        }

        private void OnDetailsChange(int currentCount)
        {
            DetailsChanged?.Invoke(currentCount);
        }
    }
}

