using System;

namespace Smokey
{
    public class Utils
    {
        public static void CheckMainScene(string sceneName, ref bool isMainScene, bool unloading)
        {
            if (sceneName == "Main" && unloading)
            {
                isMainScene = false;
            }
            else if (sceneName == "Main" && !unloading)
            {
                isMainScene = true;
            }
        }

        public class FlipFlop
        {
            private bool _state;

            public FlipFlop(bool initialState = false)
            {
                _state = initialState;
            }

            public bool SetState(bool newState)
            {
                if (newState != _state)
                {
                    _state = newState;
                    return true;
                }
                return false;
            }

            public bool GetState() => _state;
        }
    }
}
