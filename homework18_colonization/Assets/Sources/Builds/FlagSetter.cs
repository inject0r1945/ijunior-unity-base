using RTS.Management;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTS.Builds
{
    public class FlagSetter
    {
        private BuildFlag _currentFlag;
        private IEnumerable<BuildFlag> _buildFlags;
        private ScreenObjectMover _screenObjectMover;
        private LayerMask _flagSurfaceMask;

        public FlagSetter(IEnumerable<BuildFlag> buildFlags, ScreenObjectMover screenObjectMover, LayerMask flagSurfaceMask)
        {
            _buildFlags = buildFlags;
            _screenObjectMover = screenObjectMover;
            _flagSurfaceMask = flagSurfaceMask;
        }

        public bool TryStartSetFlagBehaviour()
        {
            if (TryGetFreeBuildFlag(out BuildFlag buildFlag) == false)
                return false;

            _currentFlag = buildFlag;
            _currentFlag.Positioning();

            _screenObjectMover.Move(_currentFlag, _flagSurfaceMask);
            _screenObjectMover.Moved += OnFlagMove;

            return true;
        }

        private void OnFlagMove(Vector3 position)
        {
            _screenObjectMover.Moved -= OnFlagMove;
            _currentFlag.Put(position);
            _currentFlag = null;
        }

        private bool TryGetFreeBuildFlag(out BuildFlag buildFlag)
        {
            buildFlag = null;

            if (_buildFlags.Count() == 0)
                return false;

            buildFlag = _buildFlags.Where(x => x.State == BuildFlagState.Free).FirstOrDefault();

            if (buildFlag == null)
            {
                buildFlag = _buildFlags.First();
                buildFlag.Free();
            }

            return true;
        }
    }
}
