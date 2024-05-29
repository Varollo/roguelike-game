using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class MovableTile : Tile
    {
        public MovableTile(Vector2Int tilePosition, params ITileComponent[] components) : base(tilePosition, components)
        {
        }

        protected TileMotorComponent Motor { get; private set; }

        protected override IEnumerable<ITileComponent> SetupComponents() => new ITileComponent[] 
        { 
            Motor = CreateMotorComponent(CreateMoveRule(), CreateMoveProcessor())
        };

        protected virtual TileMotorComponent CreateMotorComponent(ITileMoveRule rule, ITileMoveProcessor processor)
        {
            return new(rule, processor);
        }

        protected virtual ITileMoveRule CreateMoveRule()
        {
            return new TileFreeMoveRule();
        }

        protected virtual ITileMoveProcessor CreateMoveProcessor()
        {
            return new TileMoveDeltaProcessor();
        }

        /// <summary>
        /// Moves the tile by a given vector <paramref name="move"/>:
        /// <code>Position += Move</code>
        /// </summary>
        public void MoveTile(Vector2Int move)
        {
            if (move != Vector2Int.zero)
                base.SetPosition(Position + move);
        }
    }
}
