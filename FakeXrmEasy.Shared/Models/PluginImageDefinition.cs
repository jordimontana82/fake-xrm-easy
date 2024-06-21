using System;
using System.Collections.Generic;
using System.Text;

namespace FakeXrmEasy.Models
{
    public class PluginImageDefinition
    {
        public string Name { get; private set; }
        public ProcessingStepImageType ImageType { get; private set; }
        public IEnumerable<string> Attributes { get; private set; }

        public PluginImageDefinition(string name, ProcessingStepImageType imageType, params string[] attributes)
        {
            this.Name = name;
            this.ImageType = imageType;
            if (attributes != null && attributes.Length > 0)
            {
                this.Attributes = attributes;
            }
        }
    }
}
