using StructureMap.Configuration.DSL;
using StructureMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Barbarian.Core
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For<Game>().Singleton().Use<GameProxy>();
            For<IGameProxy>().Use(x => (GameProxy)x.GetInstance<Game>());            
            For<IGraphicsDeviceManager>().Use<GraphicsDeviceManager>();            
        }
    }
}