using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using CargaImagensNASA.Models;

namespace CargaImagensNASA;

public static class OrquestradorQueueStart
{
    [FunctionName("OrquestradorImagensNASA_QueueStart")]
    public static async Task QueueStart(
        [QueueTrigger("queue-imagens-nasa")] string dataref,
        [DurableClient] IDurableOrchestrationClient starter,
        ILogger log)
    {
        if (!Regex.IsMatch(dataref, @"^\d{4}-\d{2}-\d{2}$") ||
            !DateTime.TryParse(dataref, out var dataConvertida) ||
            dataConvertida > DateTime.Now || dataConvertida.Year < 2000)
        {
            log.LogError(
                $"{nameof(QueueStart)} - " +
                $"A data da imagem ({dataref}) deve estar no formato aaaa-mm-dd, " +
                "ser menor ou igual à data atual e a partir do ano 2000!");
            return;
        }

        log.LogInformation($"{nameof(QueueStart)} - Data-base informada = {dataref}");

        // Inicia a orquestração
        string instanceId = await starter.StartNewAsync<ParametrosExecucao>(
            "OrquestradorImagensNASA",
            new ParametrosExecucao()
            {
                CodDataReferencia = dataref,
                DataReferencia = dataConvertida
            });

        log.LogInformation($"{nameof(QueueStart)} - Iniciada orquestração com ID = '{instanceId}'.");
    }
}