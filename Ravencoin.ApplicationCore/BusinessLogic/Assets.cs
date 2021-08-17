using Ravencoin.ApplicationCore.Models;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Ravencoin.ApplicationCore.BusinessLogic
{
    public class Assets
    {
        /// <summary>
        /// Gets data about a particular asset. 
        /// </summary>
        /// <param name="assetName"> asset_name (string, required)</param>
        /// <param name="connection"> ServerConnection (required) </param>
        /// <returns>
        /// Result:
        ///{
        ///  name: (string),
        ///  amount: (number),
        ///  units: (number),
        ///  reissuable: (number),
        ///  has_ipfs: (number),
        ///  ipfs_hash: (hash), (only if has_ipfs = 1 and that data is a ipfs hash)
        ///  txid_hash: (hash), (only if has_ipfs = 1 and that data is a txid hash)
        ///  verifier_string: (string)
        /// }
        /// </returns>
        /// 
        public static async Task<ServerResponse> GetAssetData(string assetName, ServerConnection connection)
        {

            //Wrap properties in a JObject
            JObject commandParams = new JObject();
            commandParams.Add("asset_name", assetName);
            

            //Set up the ServerCommand
            ServerCommand request = new ServerCommand()
            {
                commandId = "0",
                commandMethod = "getassetdata",
                commandParams = commandParams,
                commandJsonRpc = "2.0"
            };

            //Send the ServerCommand to RavenCore. See comments for Response Value
            ServerResponse response = await RpcConnections.RavenCore.Connect(request, connection);

            return response;
        }
    }
}
