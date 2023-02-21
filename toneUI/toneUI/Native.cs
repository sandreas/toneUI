using System.Runtime.InteropServices;

namespace toneUI;

public static class ImportHelper
{
    public struct Native
    {
        /// <summary>
        /// Initializes the X threading system
        /// </summary>
        /// <remarks>Linux X11 only</remarks>
        /// <returns>non-zero on success, zero on failure</returns>
        [DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
        public static extern int XInitThreads();

    }
}