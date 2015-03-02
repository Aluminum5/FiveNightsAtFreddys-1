using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNAF.Common
{
    public enum FormBaseType
    {
        FormBase,
        Camera,
        Office,
        StartMenu
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    class FormBaseAttribute : Attribute
    {
        public FormBaseType Type;

        public FormBaseAttribute()
            : this(FormBaseType.FormBase)
        {

        }
        public FormBaseAttribute(FormBaseType type)
        {
            this.Type = type;
        }
    }
}
