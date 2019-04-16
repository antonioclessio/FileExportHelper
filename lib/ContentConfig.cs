namespace Canaoeste.Common.FileExport
{
    public class ContentConfig
    {
        /// <summary>
        /// Configura qual o delimitiador entre os campos.
        /// </summary>
        public string DelimitadorCampo { get; set; }

        /// <summary>
        /// Configura qual o delimitiador entre os campos.
        /// </summary>
        public bool IniciaLinhaComDelimitador { get; set; }

        /// <summary>
        /// Informar o caracter no caso de campos numéricos que necessitam de ter os espaços completos.
        /// </summary>
        public char? CaracterCompletarCampoNumerico { get; set; }

        /// <summary>
        /// Informar o caracter no caso de campos char que necessitam de ter os espaços completos.
        /// </summary>
        public char? CaracterCompletarCampoChar { get; set; }

        /// <summary>
        /// Informar o caracter no caso de campos de valor (currency) que necessitam de ter os espaços completos.
        /// </summary>
        public char? CaracterCompletarCampoCurrency { get; set; }

        /// <summary>
        /// Por padrão os campos numéricos são alinhados à direita.
        /// </summary>
        public bool CampoNumericoAlinhadoAEsquerda { get; set; }
    }
}
