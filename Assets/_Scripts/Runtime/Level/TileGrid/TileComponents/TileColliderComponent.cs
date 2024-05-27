namespace Ribbons.RoguelikeGame
{
    public class TileColliderComponent : TileComponent
    {
        public bool IsSolid { get; set; } = true;
        
        public virtual bool Collides(ITile other)
        {
            if (!IsSolid)
                return false;
                        
            TileColliderComponent otherCol = other.GetComponent<TileColliderComponent>();
            
            return otherCol != null && otherCol.CanCollide(this);
        }

        protected virtual bool CanCollide(TileColliderComponent sender)
        {
            return IsSolid;
        }
    }
}
