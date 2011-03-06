using Barbarian.Core;

namespace Barbarian.Win
{
    public class GameHandler
    {
        private readonly IGameProxy _game;
        private readonly GraphicsHandler _graphicsHandler;

        public GameHandler(IGameProxy gameProxy, GraphicsHandler graphicsHandler)
        {
            _graphicsHandler = graphicsHandler;
            _game = gameProxy;
            _game.Window.Title = "Barbarian";            
            _game.InitializeEvent += Initialize;
        }        

        private void Initialize()
        {
        }        

        public void Start()
        {
            using (_game)
            {
                _game.Run();
            }
        }
    }
}
