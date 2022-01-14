using Integrador.ConsoleApp;
using Integrador.ConsoleApp.Extensions;
using Xunit;

namespace Integrador.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        public void DadoMesValido_QuandoGerarCodigoDoMes_DevoRetornarCodigoCorreto(int Value)
        {
            string codigoMes = StringExtensions.RecuperarCodigoDoMes(Value);
            switch (Value)
            {
                case 1:
                    Assert.Equal("1", codigoMes);
                    break;
                case 2:
                    Assert.Equal("2", codigoMes);
                    break;
                case 3:
                    Assert.Equal("3", codigoMes);
                    break;
                case 5:
                    Assert.Equal("5", codigoMes);
                    break;
                case 10:
                    Assert.Equal("0", codigoMes);
                    break;
                case 11:
                    Assert.Equal("N", codigoMes);
                    break;
                case 12: 
                    Assert.Equal("D", codigoMes);
                    break;
                default:
                    break;
            }

        }

        [Fact]
        public void DadoBeneficiario_DevoRetornarCNPJCorreto()
        {
            RepositorioBoletos repositorio = new();
            Beneficiario beneficiario = repositorio.RecuperarBeneficiario();

            Assert.Equal("51450629000174", beneficiario.CNPJ);
        }

        [Fact]
        public void DadoHeaderRemessa_DevoRetornarComprimentoCorreto()
        {
            RepositorioBoletos repositorio = new();
            ArquivoRemessa arquivoRemessa = new();

            Beneficiario beneficiario = repositorio.RecuperarBeneficiario();

            string header = arquivoRemessa.GerarHeader(beneficiario);

            Assert.Equal(400, header.Length);
        }

        [Fact]
        public void DadoTrailerRemessa_DevoRetornarComprimentoCorreto()
        {
            RepositorioBoletos repositorio = new();
            ArquivoRemessa arquivoRemessa = new();

            Beneficiario beneficiario = repositorio.RecuperarBeneficiario();

            string header = arquivoRemessa.GerarTrailer(beneficiario);

            Assert.Equal(400, header.Length);
        }

    }
}