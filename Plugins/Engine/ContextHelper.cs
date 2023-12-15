using System.Threading;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Redcode.Awaiting.Engine
{
    /// <summary>
    /// This class capture <see cref="UnitySynchronizationContext"/> and <see cref="Thread.ManagedThreadId"/> <br/>
    /// before scene loads and allows you access to it.
    /// </summary>
	internal static class ContextHelper 
	{
        /// <summary>
        /// Main thread ID.
        /// </summary>
        internal static int MainThreadID { get; private set; }

        /// <summary>
        /// Synchronization context which created by Unity for main thread.
        /// </summary>
        internal static SynchronizationContext UnitySynchronizationContext { get; private set; }

        /// <summary>
        /// Is the current thread is main?
        /// </summary>
        internal static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == MainThreadID;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#endif
		private static void SaveContext()
		{
            MainThreadID = Thread.CurrentThread.ManagedThreadId;
            UnitySynchronizationContext = SynchronizationContext.Current;
		}
	}
}