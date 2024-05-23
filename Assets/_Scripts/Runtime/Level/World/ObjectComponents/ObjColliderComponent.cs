namespace Ribbons.RoguelikeGame
{
    public class ObjColliderComponent : ObjectComponent
    {
        public bool IsSolid { get; set; } = true;
        
        public virtual bool Collides(ITile other)
        {
            if (!IsSolid)
                return false;
                        
            ObjColliderComponent otherCol = other.GetComponent<ObjColliderComponent>();
            
            return otherCol != null && otherCol.CanCollide(this);
        }

        protected virtual bool CanCollide(ObjColliderComponent sender)
        {
            return IsSolid;
        }
    }
}
