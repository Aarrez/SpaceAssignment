using UnityEngine;


namespace CustomDebugger
{
    public class Debugger
    {
#if UNITY_EDITOR
        public Debugger(string log)
        {

            Debug.Log(log);

        }
        public Debugger(int log)
        {
            

            Debug.Log(log);
        }
        
        public Debugger(float log)
        {
            Debug.Log(log);
        }

        public Debugger(Vector2 log)
        {
            Debug.Log(log);
        }
        
        public Debugger(Vector3 log)
        {
            Debug.Log(log);

        }
#endif
        
    }
}

