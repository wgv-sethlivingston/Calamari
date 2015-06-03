﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Calamari.Integration.Azure.CloudServicePackage.ManifestSchema
{
    public class LayoutDefinition
    {
        public static readonly XName ElementName = PackageDefinition.AzureNamespace + "LayoutDefinition";
        private static readonly XName NameElementName = PackageDefinition.AzureNamespace + "Name";
        private static readonly XName LayoutDescriptionElementName = PackageDefinition.AzureNamespace + "LayoutDescription";

        public LayoutDefinition()
        {
            FileDefinitions = new List<FileDefinition>();
        }

        public LayoutDefinition(XElement element)
        {
            Name = element.Element(NameElementName).Value;
            FileDefinitions = element.Element(LayoutDescriptionElementName)
                .Elements(FileDefinition.ElementName)
                .Select(x => new FileDefinition(x))
                .ToList();
        }

        public string Name { get; set; }
        public ICollection<FileDefinition> FileDefinitions { get; private set; }

        public XElement ToXml()
        {
            return new XElement(ElementName, 
                new XElement(NameElementName, Name),
                new XElement(LayoutDescriptionElementName, FileDefinitions.Select(x => x.ToXml()).ToArray()));
        }
    }
}