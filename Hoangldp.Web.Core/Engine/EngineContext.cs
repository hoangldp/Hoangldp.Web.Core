using System.Runtime.CompilerServices;

namespace Hoangldp.Web.Core.Engine
{
    /// <summary>
    /// Provides access to the singleton instance of the engine.
    /// </summary>
    public class EngineContext
    {
        private static IEngine _engine;

        /// <summary>
        /// Gets the singleton engine used to access services.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (_engine == null)
                {
                    Create();
                }
                return _engine;
            }
        }

        /// <summary>
        /// Create a static instance of the engine.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
            if (_engine == null)
                _engine = new CoreEngine();

            return _engine;
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            _engine = engine;
        }
    }
}
