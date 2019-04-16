using Canaoeste.Common.FileExport;
using System.Collections.Generic;

namespace Canaoeste.Entities.FileExport
{
    public class ProdutorContentExport : ContentExport
    {
        private List<RegistroProdutorContentExport> _registros;

        [ExportConfig(Ordem = 1)]
        public List<RegistroProdutorContentExport> Registros 
        {
            get 
            {
                if (_registros == null)
                    _registros = new List<RegistroProdutorContentExport>();

                return _registros;
            }
            set
            {
                _registros = value;
            }
        }
    }

    public class RegistroProdutorContentExport : ContentExport
    {
        [ExportConfig(Ordem = 1, Formato = FormatType.Char, Obrigatorio = true)]
        public string RazaoSocial { get; set; }

        [ExportConfig(Ordem = 2, Formato = FormatType.Char)]
        public string CPF { get; set; }

        [ExportConfig(Ordem = 3, Formato = FormatType.Char)]
        public string TipoLogradouro { get; set; }

        [ExportConfig(Ordem = 4, Formato = FormatType.Char)]
        public string Logradouro { get; set; }

        [ExportConfig(Ordem = 5, Formato = FormatType.Char)]
        public string Numero { get; set; }

        [ExportConfig(Ordem = 6, Formato = FormatType.Char)]
        public string Complemento { get; set; }
    
        [ExportConfig(Ordem = 7, Formato = FormatType.Char)]
        public string Bairro { get; set; }

        [ExportConfig(Ordem = 8, Formato = FormatType.Char)]
        public string Cidade { get; set; }

        [ExportConfig(Ordem = 9, Formato = FormatType.Char)]
        public string Estado { get; set; }

        [ExportConfig(Ordem = 10, Formato = FormatType.Char)]
        public string Cep { get; set; }

        [ExportConfig(Ordem = 11, Formato = FormatType.Numeric)]
        public int CaixaPostal { get; set; }

        [ExportConfig(Ordem = 12, Formato = FormatType.Char)]
        public string Email { get; set; }

        [ExportConfig(Ordem = 13, Formato = FormatType.Char)]
        public string Telefone { get; set; }

        [ExportConfig(Ordem = 14, Formato = FormatType.Numeric)]
        public long CanaIndustria { get; set; }

        [ExportConfig(Ordem = 15, Formato = FormatType.Numeric)]
        public long CanaTablet { get; set; }

        [ExportConfig(Ordem = 16, Formato = FormatType.Numeric)]
        public long CanaTaxa { get; set; }

        [ExportConfig(Ordem = 17, Formato = FormatType.Numeric)]
        public int CodigoProdutor { get; set; }
    }
}