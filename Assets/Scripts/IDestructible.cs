using Asteroid;
using UnityEngine;

namespace Asteroid
{
    public interface IDestructible 
    {
        public uint Health { get; set; }

        public virtual void TakeDamage(uint i)
        {
            if (Health > 0)
            {
                Health -= i;
                return;
            }
            DestroyObject();
            
        }
        public virtual void DestroyObject()
        {
            
        }
    }
}

[CreateAssetMenu(order = 0, menuName = "Asteriod")]
public class Asteriod : ScriptableObject , IDestructible
{

    public uint Health { get; set; }

    public void TakeDamage(uint i)
    {
        
    }
}

