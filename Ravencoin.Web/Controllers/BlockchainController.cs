using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ravencoin.ApplicationCore.BusinessLogic;
using Ravencoin.ApplicationCore.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;

namespace Ravencoin.Web.Controllers
{
    [ApiController]
    [Route("/api/Blockchain/[action]")]
    public class BlockchainController : Controller
    {
        //Inject Server Configuration
        private readonly ILogger logger;
        private readonly IOptions<ServerConnection> serverConnection;
        public BlockchainController(IOptions<ServerConnection> serverConnection, ILogger<AssetsController> logger)
        {
            this.serverConnection = serverConnection;
            this.logger = logger;
        }

        public async Task<ServerResponse> GetBlockchainInfo(){
            logger.LogInformation($"Getting Blockchain Info");
            try{
                ServerResponse response = await Blockchain.GetBlockchainInfo(serverConnection.Value);
                return response;
            }
            catch (Exception ex){
                logger.LogError($"Exception: {ex.Message}");
                ServerResponse errResponse = new ServerResponse(){
                    statusCode = System.Net.HttpStatusCode.InternalServerError,
                    errorEx = ex.Message
                };
                return errResponse;
            }
        }

        public async Task<ServerResponse> GetBlockCount(){
            logger.LogInformation($"Getting latest block count");
            try{
                ServerResponse response = await Blockchain.GetBlockCount(serverConnection.Value);
                return response;
            }
            catch (Exception ex){
                logger.LogError($"Exception: {ex.Message}");
                ServerResponse errResponse = new ServerResponse(){
                    statusCode = System.Net.HttpStatusCode.InternalServerError,
                    errorEx = ex.Message
                };
                return errResponse;
            }
        }
    }
}
