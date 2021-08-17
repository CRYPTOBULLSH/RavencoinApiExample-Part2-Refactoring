using Newtonsoft.Json.Linq;
using Ravencoin.ApplicationCore.Models;
using System.Threading.Tasks;

namespace Ravencoin.ApplicationCore.BusinessLogic
{
     public class Transactions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txid"></param>
        /// <param name="connection"></param>
        /// <returns>"data"      (string) The serialized, hex-encoded data for 'txid'</returns>
        public static async Task<ServerResponse> GetRawTransaction(string txid, ServerConnection connection)
        {
            //Set up parameters to get the hex string of the transaction
            JObject commandParams = new JObject(
                new JProperty("txid", txid)
            );
            //Set up the Ravcencoin Object
            ServerCommand request = new ServerCommand()
            {
                commandId = "0",
                commandMethod = "getrawtransaction",
                commandParams = commandParams,
                commandJsonRpc = "2.0"
            };

            //Get the hex string of the transaction back from getrawtransaction, and then parse it to get just the raw hex string from result
            ServerResponse response = await RpcConnections.RavenCore.Connect(request, connection);

            //Parse the result for the hexstring
            JObject result = JObject.Parse(response.responseContent);
            JToken hexstring = result["result"];

            response.responseContent = hexstring.ToString();

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexstring"></param>
        /// <param name="connection"></param>
        /// <returns>
        /// {
        ///  "txid" : "id",        (string) The transaction id
        ///  "hash" : "id",        (string) The transaction hash(differs from txid for witness transactions)
        ///  "size" : n,             (numeric) The transaction size
        ///  "vsize" : n,            (numeric) The virtual transaction size(differs from size for witness transactions)
        ///  "version" : n,          (numeric) The version
        ///  "locktime" : ttt,       (numeric) The lock time
        ///  "vin" : [               (array of json objects)
        ///     {
        ///       "txid": "id",    (string) The transaction id
        ///       "vout": n,         (numeric) The output number
        ///       "scriptSig": {     (json object) The script
        ///         "asm": "asm",  (string) asm
        ///         "hex": "hex"   (string) hex
        ///       },
        ///       "txinwitness": ["hex", ...] (array of string) hex-encoded witness data(if any)
        ///       "sequence": n(numeric) The script sequence number
        ///     }
        ///     ,...
        ///  ],
        ///  "vout" : [             (array of json objects)
        ///     {
        ///       "value" : x.xxx,            (numeric) The value in RVN
        ///       "n" : n,                    (numeric) index
        ///       "scriptPubKey" : {          (json object)
        ///         "asm" : "asm",          (string) the asm
        ///         "hex" : "hex",          (string) the hex
        ///         "reqSigs" : n,            (numeric) The required sigs
        ///         "type" : "pubkeyhash",  (string) The type, eg 'pubkeyhash'
        ///         "asset" : {               (json object) optional
        ///           "name" : "name",      (string) the asset name
        ///           "amount" : n,           (numeric) the amount of asset that was sent
        ///           "message" : "message", (string optional) the message if one was sent
        ///           "expire_time" : n,      (numeric optional) the message epoch expiration time if one was set
        ///         "addresses" : [           (json array of string)
        ///           "12tvKAXCxZjSmdNbao16dKXC8tRWfcF5oc"   (string) raven address
        ///           ,...
        ///         ]
        ///       }
        ///     }
        ///     ,...
        ///  ],
        ///}
        /// </returns>
        public static async Task<ServerResponse> DecodeRawTransaction(string hexstring, ServerConnection connection)
        {
            //Set up parameters to get the hex string of the transaction
            JObject commandParams = new JObject(
                new JProperty("hexstring", hexstring)
            );
            //Set up the Ravcencoin Object
            ServerCommand request = new ServerCommand()
            {
                commandId = "0",
                commandMethod = "decoderawtransaction",
                commandParams = commandParams,
                commandJsonRpc = "2.0"
            };

            //Get the hex string of the transaction back from getrawtransaction, and then parse it to get just the raw hex string from result
            ServerResponse response = await RpcConnections.RavenCore.Connect(request, connection);

            return response;
        }
        /// <summary>
        /// Looks up the raw transaction and then decodes the raw transaction to return a verbose json response about the transactionid
        /// </summary>
        /// <param name="txid"></param>
        /// <param name="connection"></param>
        /// <returns>See return details from "DecodeRawTransaction"</returns>
        public static async Task<ServerResponse> GetPublicTransaction(string txid, ServerConnection connection)
        {
            //Get the RawTransaction and return the hexcode
            ServerResponse hexcode = await GetRawTransaction(txid, connection);

            //Get Full Transaction from Hexcode
            ServerResponse response = await DecodeRawTransaction(hexcode.responseContent, connection);

            return response;
        }
    }
}
