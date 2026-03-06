using System;
using System.Collections.Generic;
using EchoGridLibrary.Events;

namespace EchoGridLibrary.Puzzle
{
    public class PressurePlateLogic
    {
        private int _objectsOnPlate = 0;
        public int SwitchId { get; set; }
        public bool IsActive => _objectsOnPlate > 0;

        public void HandleEnter()
        {
            _objectsOnPlate++;
            if (_objectsOnPlate == 1)
            {
                EventBus.TriggerSwitchActivated(SwitchId);
            }
        }

        public void HandleExit()
        {
            _objectsOnPlate--;
            if (_objectsOnPlate < 0) _objectsOnPlate = 0;

            if (_objectsOnPlate == 0)
            {
                EventBus.TriggerSwitchDeactivated(SwitchId);
            }
        }
    }
}
