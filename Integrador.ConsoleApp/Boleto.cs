using System;
using Integrador.ConsoleApp.Juros;

namespace Integrador.ConsoleApp
{
    public sealed class Boleto
    {
        public Boleto(string nossoNumero, string numeroDocumento, Beneficiario beneficiario, Pagador pagador, 
            string localDePagamento, string[] instrucoes, DateTime vencimento, decimal valor, 
            DescontoAntecipacao desconto, IJuros juros)
        {
            NossoNumero = nossoNumero;
            NumeroDocumento = numeroDocumento;
            Beneficiario = beneficiario;
            Pagador = pagador;
            LocalDePagamento = localDePagamento;
            Instrucoes = instrucoes;
            Vencimento = vencimento;
            Valor = valor;
            Desconto = desconto;
            Juros = juros;
        }

        private Boleto() { }

        public string NossoNumero { get; }
        public string NumeroDocumento { get;}
        public Beneficiario Beneficiario { get; }
        public Pagador Pagador { get; }
        public string LocalDePagamento { get; }
        public string[] Instrucoes { get; }
        public DateTime Vencimento { get;}
        public decimal Valor { get;  }
        public DescontoAntecipacao Desconto { get; }
        public IJuros Juros { get; }
    }
}