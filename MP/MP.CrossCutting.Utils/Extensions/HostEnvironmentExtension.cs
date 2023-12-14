using Microsoft.Extensions.Hosting;

namespace MP.CrossCutting.Utils.Extensions
{
    /// <summary>
    /// Extension method adding Local environtment to <see cref="IHostEnvironment"/>.<br/>
    /// Based on <see cref="Microsoft.Extensions.Hosting.HostEnvironmentEnvExtensions"/>
    /// </summary>
    public static class HostEnvironmentExtension
    {

        /// <summary>
        /// Local environment name. Based on <see cref="Microsoft.Extensions.Hosting.Environments"/>.
        /// </summary>
        public static readonly string Local = "Local";

        /// <summary>
        /// Checks if the current host environment name is <see cref="Local"/>.
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>True if the environment name is <see cref="Local"/>, otherwise false.</returns>
        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment(Local);
        }
    }
}