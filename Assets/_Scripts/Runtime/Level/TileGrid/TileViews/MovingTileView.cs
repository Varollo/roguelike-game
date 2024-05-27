namespace Ribbons.RoguelikeGame
{
    public class MovingTileView : TileView
    {
        protected override ITile CreateTile()
        {
            return new MovingTransformTile(transform,
                new TileTransformComponent(
                    transform, new TileTransformDOTweenMover(.25f)),

                new TileMotorComponent(
                    new TileMoveSwipeProcessor()),

                new TileTurnListenerComponent(OnTurnAction)
            );
        }

        private void OnTurnAction(ulong turnCount)
        {
            var move = Tile.GetComponent<TileMotorComponent>().GetMove(transform.position.ToVec2Int());
            TileGridManager.SetTile(Tile, move.x, move.y);
        }
    }
}
