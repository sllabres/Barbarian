using StructureMap;

namespace Barbarian.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Registrar.Register();            
            ObjectFactory.GetInstance<GameHandler>();
        }
    }
}