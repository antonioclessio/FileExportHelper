using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Canaoeste.Common.FileExport
{
    public class ParseFileExport
    {
        private ContentConfig MainConfig { get; set; }

        public ParseFileExport(ContentConfig config)
        {
            MainConfig = config;
        }

        public string Parse(ContentExport content)
        {
            var listProperty = (from property in content.GetType().GetProperties()
                                let orderAttribute = property.GetCustomAttributes(typeof(ExportConfigAttribute), false).SingleOrDefault() as ExportConfigAttribute
                                orderby orderAttribute.Ordem
                                select property).ToList();

            StringBuilder sbRetorno = new StringBuilder();
            foreach (var property in listProperty)
            {
                // Verifica se é uma lista
                if (property.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null)
                {
                    var listItem = property.GetValue(content, null) as IEnumerable<ContentExport>;
                    if (listItem != null)
                    {
                        foreach (var item in listItem)
                        {
                            if (item.GetType().BaseType == typeof(ContentExport))
                            {
                                if (item.GetType().GetProperties().All(a => a.PropertyType.BaseType.Name == "Object") == true && item.GetType().GetProperties().Any(a => a.Name != "R10"))
                                {
                                    sbRetorno.Append(ExtrairConteudo(item));
                                }
                                else
                                {
                                    sbRetorno.Append(Parse(item as ContentExport));
                                }
                            }
                            else
                            {
                                sbRetorno.Append(ExtrairConteudo(item));
                            }
                        }
                    }
                }
                else
                {
                    if (property.PropertyType.BaseType == typeof(ContentExport))
                    {
                        if((property.GetValue(content, null) as ContentExport) != null)
                            sbRetorno.Append(Parse(property.GetValue(content, null) as ContentExport));
                    }
                    else
                    {
                        if (property.GetValue(content, null) != null)
                            sbRetorno.Append(ExtrairConteudo(property.GetValue(content, null)));
                    }
                }
            }

            return sbRetorno.ToString();
        }

        private string ExtrairConteudo(object view, bool isChild = false)
        {
            var sbRetorno = new StringBuilder();

            var listProperty = (from property in view.GetType().GetProperties()
                                let orderAttribute = property.GetCustomAttributes(typeof(ExportConfigAttribute), false).SingleOrDefault() as ExportConfigAttribute
                                orderby orderAttribute.Ordem
                                select property).ToList();

            foreach (var item in listProperty)
            {
                if(item.PropertyType.Name != "String" && item.PropertyType.Name != "Int32" && item.PropertyType.Name != "Int64")
                {
                    var listItem = item.GetValue(view, null) as IEnumerable<ContentExport>;
                    if (listItem != null)
                    {
                        int count = 0;
                        foreach (var obj in listItem)
                        {
                            if (count == 0 && !sbRetorno.ToString().EndsWith(Environment.NewLine))
                                sbRetorno.Append(Environment.NewLine);

                            count++;
                            sbRetorno.Append(ExtrairConteudo(obj));
                        }
                    }
                    else
                    {
                        var childItem = item.GetValue(view, null) as ContentExport;
                        if (childItem != null)
                        {
                            sbRetorno.Append(Environment.NewLine);
                            sbRetorno.Append(ExtrairConteudo(childItem, true));
                        }
                    }
                }
                else
                {
                    var valor = (item.PropertyType.Name == "Int32" && item.GetValue(view, null) != null) ? int.Parse(item.GetValue(view, null).ToString()).ToString()
                        : (item.PropertyType.Name == "Int64") ? long.Parse(item.GetValue(view, null).ToString()).ToString()
                        : item.GetValue(view, null) as string;
                    var attrConfig = item.GetCustomAttribute(typeof(ExportConfigAttribute)) as ExportConfigAttribute;
                    if (attrConfig == null) continue;

                    if (attrConfig.Obrigatorio && valor.IsNullOrEmpty())
                        throw new Exception(string.Format("Campo {0} obrigatório.", item.Name));

                    sbRetorno.Append(FormatarConteudo(valor, attrConfig));
                }
            }

            if (sbRetorno.IsNullOrEmpty() == false && isChild == false && !sbRetorno.ToString().EndsWith(Environment.NewLine) && !sbRetorno.ToString().StartsWith(Environment.NewLine))
            {
                sbRetorno.Append(Environment.NewLine);
            }

            return !MainConfig.IniciaLinhaComDelimitador ? sbRetorno.ToString() : string.Format("{0}{1}", MainConfig.DelimitadorCampo, sbRetorno.ToString());
        }

        private string FormatarConteudo(string valor, ExportConfigAttribute attrConfig)
        {
            if (valor.IsNullOrEmpty())
                return MainConfig.DelimitadorCampo.IsNullOrEmpty() ? string.Empty : MainConfig.DelimitadorCampo;

            // Truncando...
            if (!valor.IsNullOrEmpty() && attrConfig.Tamanho > 0 && valor.Length > attrConfig.Tamanho) valor = valor.Substring(0, attrConfig.Tamanho);

            // Se for numérico, configura o preenchimento dos campos em branco baseado em configurações de alinhamento.
            if (attrConfig.Formato == FormatType.Numeric)
            {
                if (!MainConfig.CaracterCompletarCampoNumerico.IsNullOrEmpty())
                {
                    if (MainConfig.CampoNumericoAlinhadoAEsquerda)
                    {
                        valor = valor.PadRight(attrConfig.Tamanho, MainConfig.CaracterCompletarCampoNumerico.Value);
                    }
                    else
                    {
                        valor = valor.PadLeft(attrConfig.Tamanho, MainConfig.CaracterCompletarCampoNumerico.Value);
                    }
                }
                else
                {
                    valor = valor.TrimEnd();
                }
            }

            if (attrConfig.Formato == FormatType.Currency)
            {
                if (!MainConfig.CaracterCompletarCampoCurrency.IsNullOrEmpty())
                {
                    if (MainConfig.CampoNumericoAlinhadoAEsquerda)
                    {
                        valor = valor.PadRight(attrConfig.Tamanho, MainConfig.CaracterCompletarCampoCurrency.Value);
                    }
                    else
                    {
                        valor = valor.PadLeft(attrConfig.Tamanho, MainConfig.CaracterCompletarCampoCurrency.Value);
                    }
                }
                else
                {
                    valor = valor.TrimEnd();
                }
            }

            // Se for campo do tipo char, fazer as configurações necessárias.
            if (attrConfig.Formato == FormatType.Char)
            {
                if (!MainConfig.CaracterCompletarCampoChar.IsNullOrEmpty())
                {
                    if (valor.IsNullOrEmpty()) valor = " ";
                    valor = valor.PadRight(attrConfig.Tamanho, MainConfig.CaracterCompletarCampoChar.Value);
                }
                else
                {
                    valor = valor.TrimEnd();
                }
            }

            if (!MainConfig.DelimitadorCampo.IsNullOrEmpty())
            {
                valor += MainConfig.DelimitadorCampo;
            }

            return valor;
        }
    }
}