using Canaoeste.Common.FileExport;
using System.Collections.Generic;

namespace Canaoeste.Entities.FileExport
{
    public class PAAContentExport : ContentExport
    {
        private List<RegistroPAAContentExport> _registros;

        [ExportConfig(Ordem = 1)]
        public List<RegistroPAAContentExport> Registros 
        {
            get 
            {
                if (_registros == null)
                    _registros = new List<RegistroPAAContentExport>();

                return _registros;
            }
            set
            {
                _registros = value;
            }
        }
    }

    public class RegistroPAAContentExport : ContentExport
    {
        [ExportConfig(Ordem = 1, Formato = FormatType.Char)]
        public string CPF { get; set; }

        [ExportConfig(Ordem = 2, Formato = FormatType.Char)]
        public string Produtor { get; set; }

        [ExportConfig(Ordem = 3, Formato = FormatType.Char)]
        public string Endereco { get; set; }

        [ExportConfig(Ordem = 4, Formato = FormatType.Char)]
        public string CEP { get; set; }

        [ExportConfig(Ordem = 5, Formato = FormatType.Char)]
        public string CidadeProdutor { get; set; }
    
        [ExportConfig(Ordem = 6, Formato = FormatType.Char)]
        public int DDD { get; set; }

        [ExportConfig(Ordem = 7, Formato = FormatType.Char)]
        public string Telefone { get; set; }

        [ExportConfig(Ordem = 8, Formato = FormatType.Char)]
        public string CCIR { get; set; }

        [ExportConfig(Ordem = 9, Formato = FormatType.Char)]
        public string Propriedade { get; set; }

        [ExportConfig(Ordem = 10, Formato = FormatType.Char)]
        public string ModalidadeExploracao { get; set; }

        [ExportConfig(Ordem = 11, Formato = FormatType.Numeric)]
        public long CidadePropriedade { get; set; }

        [ExportConfig(Ordem = 12, Formato = FormatType.Char)]
        public string CoordenadaE { get; set; }

        [ExportConfig(Ordem = 13, Formato = FormatType.Char)]
        public string CoordenadaN { get; set; }

        [ExportConfig(Ordem = 14, Formato = FormatType.Char)]
        public string Datum { get; set; }

        [ExportConfig(Ordem = 15, Formato = FormatType.Numeric)]
        public int Fuso { get; set; }

        [ExportConfig(Ordem = 16, Formato = FormatType.Numeric)]
        public float AreaRestricao { get; set; }

        [ExportConfig(Ordem = 17, Formato = FormatType.Numeric)]
        public float AreaMecanizavel { get; set; }

        [ExportConfig(Ordem = 18, Formato = FormatType.Numeric)]
        public float AreaNaoMecanizavel { get; set; }

        [ExportConfig(Ordem = 19, Formato = FormatType.Numeric)]
        public float AreaMecanizavelCrua { get; set; }

        [ExportConfig(Ordem = 20, Formato = FormatType.Numeric)]
        public float AreaNaoMecanizavelCrua { get; set; }
    }
}