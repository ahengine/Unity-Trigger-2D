using UnityEngine;
using System.Collections.Generic;

namespace UTrigger2D
{
    public class Trigger2DHandler : MonoBehaviour
    {
        private static Trigger2DHandler instance;
        public static Trigger2DHandler Instance => 
            instance ?? (instance = new GameObject("Trigger2D Handler").AddComponent<Trigger2DHandler>());

        [SerializeField] private List<Trigger2D> triggers = new List<Trigger2D>();
        [SerializeField] private List<Trigger2D> triggersRemoveList = new List<Trigger2D>();

        public static void Add(Trigger2D trigger) =>
            Instance.triggers.Add(trigger);
        public static void Remove(Trigger2D trigger) =>
            Instance.triggersRemoveList.Add(trigger);

        private void FixedUpdate()
        {
            if(triggersRemoveList.Count > 0) 
                while(triggersRemoveList.Count > 0)
                {
                    triggers.Remove(triggersRemoveList[0]);
                    triggersRemoveList.RemoveAt(0);
                }

            for (int i = 0; i < triggers.Count; i++)
                for (int j = 0; j < triggers.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (triggers[i].CheckTrigger(triggers[j]))
                        triggers[i].TriggerEnter(triggers[j]);
                    else
                        triggers[i].TriggerExit(triggers[j]);
                }
        }

        private void OnLevelWasLoaded(int level) =>
            instance = null;

        private void OnDestroy() =>
            instance = null;
    }
}