using UnityEngine;


namespace CustomDebugger
{
    public class Debugger
    {
        public Debugger(string log)
        {
#if UNITY_EDITOR
            Debug.Log(log);
  #endif 
        }
        public Debugger(int log)
        {
            
#if UNITY_EDITOR
            Debug.Log(log);
  #endif
        }

        public Debugger(Vector2 log)
        {
#if UNITY_EDITOR
            Debug.Log(log);
  #endif
        }
        
        public Debugger(Vector3 log)
        {
#if UNITY_EDITOR
            Debug.Log(log);
  #endif
        }
    }
}

