using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using CargaImagensNASA.HttpClients;
using CargaImagensNASA.Models;

namespace CargaImagensNASA
{
    public class FunctionObterDadosImagemPorData
    {
        private readonly IImagemDiariaAPI _imagemDiariaAPI;

        public FunctionObterDadosImagemPorData(IImagemDiariaAPI imagemDiariaAPI)
        {
            _imagemDiariaAPI = imagemDiariaAPI;
        }

        [FunctionName("ObterDadosImagemPorData")]
        public async Task<InfoImagemNASA> ObterDadosImagemPorData(
            [ActivityTrigger] ParametrosExecucao parametrosExecucao,
            ILogger log)
        {
            log.LogInformation(
                $"{nameof(ObterDadosImagemPorData)} - Iniciando a execução...");
            
            var infoImagemNASA = await _imagemDiariaAPI.GetInfoAsync(
                Environment.GetEnvironmentVariable("APIKeyNASA"),
                parametrosExecucao.CodDataReferencia);

            log.LogInformation(
                $"{nameof(ObterDadosImagemPorData)} - Dados retornados pela API: " +
                JsonSerializer.Serialize(infoImagemNASA));

            return infoImagemNASA;
        }
    }
}