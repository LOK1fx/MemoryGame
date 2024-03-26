namespace LOK1game
{
    public class TimeTravelActorInteractable : InteractebleActor
    {
        public override void OnInteract(Player.Player sender)
        {
            base.OnInteract(sender);

            sender.GetComponent<Eyeblink>().Blink();
        }
    }
}