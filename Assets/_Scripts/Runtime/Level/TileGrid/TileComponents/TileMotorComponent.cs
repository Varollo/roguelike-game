using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileMotorComponent : TileComponent
    {
        private readonly ITileMoveRule _moveRule;
        private readonly ITileMoveProcessor _moveProcessor;

        private bool _initialized = false;

        #region Constructors
        /// <summary>
        /// Creates a <see cref="TileMotorComponent"/> with a <see cref="TileFreeMoveRule"/> and a <see cref="TileMoveDeltaProcessor"/> as default.
        /// </summary>
        public TileMotorComponent() : this(new TileFreeMoveRule(), new TileMoveDeltaProcessor())
        {
        }

        /// <summary>
        /// Creates a <see cref="TileMotorComponent"/> with a <see cref="TileFreeMoveRule"/> as default.
        /// </summary>
        public TileMotorComponent(ITileMoveRule moveRule) : this(moveRule, new TileMoveDeltaProcessor())
        {
        }

        /// <summary>
        /// Creates a <see cref="TileMotorComponent"/> with a <see cref="TileMoveDeltaProcessor"/> as default.
        /// </summary>
        public TileMotorComponent(ITileMoveProcessor moveProcessor) : this(new TileFreeMoveRule(), moveProcessor)
        {
        }

        /// <summary>
        /// Creates a <see cref="TileMotorComponent"/> and asigns a custom <paramref name="moveRule"/>.
        /// </summary>
        public TileMotorComponent(ITileMoveRule moveRule, ITileMoveProcessor moveProcessor)
        {
            _moveRule = moveRule;
            _moveProcessor = moveProcessor;
        } 
        #endregion

        public Vector2Int LatePosition { get; private set; }

        /// <summary>
        /// Gets the best move possible towards <paramref name="targetPos"/>.
        /// </summary>
        public virtual Vector2Int GetMove(Vector2Int targetPos)
        {
            if (!_initialized)
            {
                _initialized = true;                
                return LatePosition = targetPos;
            }

            if (_moveRule.CanMove(LatePosition, targetPos))
                return _moveProcessor.ProcessMove(targetPos, LatePosition);

            else
                return Vector2Int.zero;
        }

        public override void OnTilePositionMove(Vector2Int newPos)
        {
            LatePosition = Position;
            base.OnTilePositionMove(newPos);
        }
    }
}
