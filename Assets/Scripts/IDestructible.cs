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

