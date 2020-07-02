using EXILED;

namespace HandCuffedExplode
{
    public class MainSetting : Plugin
    {
        public override string getName => "HandCuffedExplode";
        private SetEvents SetEvent;

        public override void OnEnable()
        {
            SetEvent = new SetEvents();
            Events.WaitingForPlayersEvent += SetEvent.OnWaitingForPlayers;
            Events.RoundStartEvent += SetEvent.OnRoundStart;
            Events.PlayerSpawnEvent += SetEvent.OnSpawnPlayer;
            Events.ConsoleCommandEvent += SetEvent.OnCallCommand;
            Events.PlayerHandcuffedEvent += SetEvent.OnHandcuffed;
            Events.PocketDimEscapedEvent += SetEvent.OnPocketDimensionExit;
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Events.WaitingForPlayersEvent -= SetEvent.OnWaitingForPlayers;
            Events.RoundStartEvent -= SetEvent.OnRoundStart;
            Events.PlayerSpawnEvent -= SetEvent.OnSpawnPlayer;
            Events.ConsoleCommandEvent -= SetEvent.OnCallCommand;
            Events.PlayerHandcuffedEvent -= SetEvent.OnHandcuffed;
            Events.PocketDimEscapedEvent -= SetEvent.OnPocketDimensionExit;
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}