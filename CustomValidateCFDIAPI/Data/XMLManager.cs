using System;
using System.IO;
using System.Xml;
using CustomValidateCFDIAPI.Models;

namespace CustomValidateCFDIAPI.Data
{
    public class XMLManager
    {
        public XMLManager()
        {
        }

        public string Validate(CFDIModel cfdi)
        {
            string validate = "";
            string version;

            XmlDocument document = new XmlDocument();

            XmlNamespaceManager xmlNamespace = new XmlNamespaceManager(document.NameTable);
            
            document.Load(cfdi.XML.OpenReadStream());

            // Rutas del SAT
            xmlNamespace.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3");
            xmlNamespace.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");

            version = document.SelectSingleNode("/cfdi:Comprobante/@Version", xmlNamespace).InnerText;

            if (version == "3.3")
            {
                string emisor = document.SelectSingleNode("/cfdi:Comprobante/cfdi:Emisor/@Rfc", xmlNamespace).InnerText;
                
                if (emisor == cfdi.RFC)
                {
                    decimal subtotal = Decimal.Parse(document.SelectSingleNode("/cfdi:Comprobante/@SubTotal", xmlNamespace).InnerText);

                    XmlNodeList conceptos = document.SelectNodes("/cfdi:Comprobante/cfdi:Conceptos/cfdi:Concepto", xmlNamespace);

                    decimal sum = 0;
                    foreach (XmlNode concepto in conceptos) {
                        sum += Decimal.Parse(concepto.Attributes.GetNamedItem("Importe").Value);
                    }

                    if (sum == subtotal)
                    {
                        validate = "CFDI Valido";
                    }
                    else
                    {
                        validate = "La suma del importe de los conceptos no coincide con el subtotal";
                    }
                }
                else
                {
                    validate = "El RFC del emisor en el CFDI no coincide con el parametro RFC";
                }
                
            }
            else
            {
                validate = "El CFDI no es version 3.3";
            }
                

            return validate;
        }
    }
}
