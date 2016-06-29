using System.ComponentModel;

namespace abc_bank.Enum
{
  public static class EnumHelper
  {
    public static string Description<T>(this T value)
    {
      // variables  
      var enumType = value.GetType();
      var field = enumType.GetField(value.ToString());
      var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

      // return  
      return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
    }
  }
}
