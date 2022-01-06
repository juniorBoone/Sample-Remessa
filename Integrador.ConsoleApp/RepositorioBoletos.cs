using System;
using System.Collections.Generic;
using System.IO;
using Integrador.ConsoleApp.Extensions;
using Integrador.ConsoleApp.Juros;
using Newtonsoft.Json;

namespace Integrador.ConsoleApp
{
    public sealed class RepositorioBoletos
    {
        private const string arquivo = "pessoas.json";

        public IEnumerable<Boleto> RecuperarTodos()
        {
            var hoje = DateTime.Now;
            var pessoas = JsonConvert.DeserializeObject<IEnumerable<PessoaDto>>(File.ReadAllText(arquivo));
            
            foreach (var pessoa in pessoas)
            {
                yield return new Boleto(
                    GerarNumero(6),
                    GerarNumero(10),
                    new Cedente("987", "8276", "221121"),
                    new Beneficiario(pessoa.nome, pessoa.nome, Cpf.Recuperar(pessoa.cpf),
                        new EnderecoCompleto(pessoa.endereco, pessoa.numero.ToString(), "", pessoa.bairro,
                            pessoa.cidade, pessoa.cep, pessoa.estado, "Brasil")),
                    "",
                    new[] {"Não receber após vencimento"},
                    hoje.AddDays(15),
                    GerarValor(1000),
                    GerarDesconto(hoje.AddDays(15)),
                    GerarJuros());
            }
        }

        private string GerarNumero(int tamanho, int maximo = 999999)
        {
            var random = new Random();
            return random.Next(maximo).ToPadLeftZeros(tamanho);
        }

        private decimal GerarValor(int maximo)
        {
            var random = new Random();
            var reais = random.Next(maximo);
            var centavos = random.Next(99);
            return Convert.ToDecimal($"{reais},{centavos}");
        }

        private IJuros GerarJuros()
        {
            var random = new Random();
            if (random.Next() % 2 == 0)
                return new SemJuros();
            return new PercentualPorDia(GerarValor(5));
        }
        
        private DescontoAntecipacao GerarDesconto(DateTime vencimento)
        {
            var random = new Random();
            if (random.Next() % 2 == 0)
                return new DescontoAntecipacao(vencimento, 0);
            return new DescontoAntecipacao(vencimento, GerarValor(10));
        }
        
        public sealed class PessoaDto
        {
            public string nome { get; set; }
            public string cpf { get; set; }
            public string data_nasc { get; set; }
            public string cep { get; set; }
            public string endereco { get; set; }
            public int numero { get; set; }
            public string bairro { get; set; }
            public string cidade { get; set; }
            public string estado { get; set; }
        } 
    }
}