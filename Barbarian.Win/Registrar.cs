using StructureMap.Configuration.DSL;
using StructureMap;
using Barbarian.Core;
using Barbarian.Win.Animation;

namespace Barbarian.Win
{
    public class Registrar
    {
        public static void Register()
        {
            ObjectFactory.Initialize(x =>
            {                
                x.AddRegistry<CoreRegistry>();
                x.AddRegistry<BarbarianWinRegistry>();                
            });
        }
    }

    public class BarbarianWinRegistry : Registry
    {
        public BarbarianWinRegistry()
        {
            For<GameHandler>().OnCreationForAll(x => x.Start());
            
            Scan(s =>
            {
                s.TheCallingAssembly();
                s.AddAllTypesOf<IDrawable>();
            });            
        }
    }
}