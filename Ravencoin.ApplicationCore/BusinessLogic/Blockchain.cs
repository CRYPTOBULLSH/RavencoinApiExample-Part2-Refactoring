using Ravencoin.ApplicationCore.Models;
using System.Threading.Tasks;

namespace Ravencoin.ApplicationCore.BusinessLogic
{
    public class Blockchain
    {
        /// <summary>
        /// Returns an object containing various state info regarding blockchain processing.
        /// </summary>
        /// <param name="connection">ServerConnection (required)</param>
        /// <returns>A Json response with the current state of the blockchain</returns>
        public static async Task<ServerResponse> GetBlockchainInfo(ServerConnection connection)
        {
            ServerCommand request = new ServerCommand()
            {
                commandId = "0",
                commandMethod = "getblockchaininfo",
                commandJsonRpc = "2.0"
            };

            ServerResponse response = await RpcConnections.RavenCore.Connect(request, connection);

            return response;
        }

        /// <summary>
        /// Returns the number of blocks in the longest blockchain.
        /// </summary>
        /// <param name="connection">ServerConnection (required)</param>
        /// <returns>
        /// Result:
        /// n(numeric) The current block count
        /// </returns>
        public static async Task<ServerResponse> GetBlockCount(ServerConnection connection)
        {
            ServerCommand request = new ServerCommand()
            {
                commandId = "0",
                commandMethod = "getblockcount",
                commandJsonRpc = "2.0"
            };

            ServerResponse response = await RpcConnections.RavenCore.Connect(request, connection);

            return response;
        }
    }
}
