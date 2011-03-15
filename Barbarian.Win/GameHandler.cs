using Barbarian.Core;
using Microsoft.Xna.Framework;

namespace Barbarian.Win
{
    public class GameHandler
    {
        private readonly IGameProxy _game;
        private readonly GraphicsHandler _graphicsHandler;
        private Barbarian _barbarian;

        public GameHandler(IGameProxy gameProxy, GraphicsHandler graphicsHandler, Barbarian barbarian)
        {

            _graphicsHandler = graphicsHandler;
            _game = gameProxy;
            _game.Window.Title = "Barbarian";            
            _game.InitializeEvent += Initialize;
            _game.UpdateEvent += Update;
            _barbarian = barbarian;
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

        private void Update(GameTime gameTime)
        {
            _barbarian.Update(gameTime);
        }
    }
}
