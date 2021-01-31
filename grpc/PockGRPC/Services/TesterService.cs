using Grpc.Core;
using Microsoft.Extensions.Logging;
using PockGRPC.Controllers;
using PockGRPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testeProto
{
    public class TesterService : Tester.TesterBase
    {
        private readonly ILogger<TesterService> _logger;

        public TesterService(ILogger<TesterService> logger)
        {
            _logger = logger;
        }

        public override Task<ModelResponse> SalvarUsuario(ModeloRequest request, ServerCallContext context)
        {
            var model = new Modelo
            {
                Endereco = request.Endereco,
                Id = request.Id,
                IntNull = request.IntNull,
                Nome = request.Nome,
                Telefone = request.Telefone,
                ListaString = request.ValorRepetido.Select(x => x).ToList()
            };

            ValuesController._modelo.Add(model);

            return Task.FromResult(
                new ModelResponse
                {
                    Resposta = request.Nome
                });

            return base.SalvarUsuario(request, context);
        }
    }
}
