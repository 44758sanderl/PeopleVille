using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVille_GUI
{
    internal class EventManager
    {
        private static EventManager? _eventManager = null;
        public event Action VillagerMovedAction;

        public void VillagerMoved()
        {
            VillagerMovedAction.Invoke();
        }
        public static EventManager GetEventManager()
        {
            return _eventManager ?? (_eventManager = new EventManager());
        }
    }
}
