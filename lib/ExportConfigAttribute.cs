using System;

namespace Canaoeste.Common.FileExport
{
    public class ExportConfigAttribute : Attribute
    {
        public int Ordem { get; set; }
        public int Tamanho { get; set; }
        public FormatType Formato { get; set; }
        public bool Obrigatorio { get; set; }
    }
}
