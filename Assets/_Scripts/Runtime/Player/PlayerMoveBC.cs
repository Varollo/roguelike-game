namespace Ribbons.RoguelikeGame.Player
{
    public class PlayerMoveBC : BehaviourController
    {
        protected override TurnBasedBehaviour GetNewBehaviour() => new PlayerMoveBehaviour();
    }
}
