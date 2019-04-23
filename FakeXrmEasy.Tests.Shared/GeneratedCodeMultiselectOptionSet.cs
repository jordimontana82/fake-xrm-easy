#if FAKE_XRM_EASY_9
namespace Crm
{
    public partial class Contact
    {
        /// <summary>
        /// Sample multiselect optionset attribute.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("new_multiselectattribute")]
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
        public Microsoft.Xrm.Sdk.OptionSetValueCollection new_MultiSelectAttribute
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValueCollection>("new_multiselectattribute");
            }

            set
            {
                this.OnPropertyChanging("new_multiselectattribute");
                this.SetAttributeValue("new_multiselectattribute", value);
                this.OnPropertyChanged("new_multiselectattribute");
            }
        }
    }
}
#endif