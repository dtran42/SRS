using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SRS.Common
{
    public class Utils
    {
        public static void Errors(IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (IdentityError error in result.Errors)
                modelState.AddModelError("", error.Description);
        }
    }

    public enum Priority
    {
        [Description("Emergency")]
        Emergency = 0,
        [Description("Hight")]
        Hight = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("Low")]
        Low = 3
    }
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
