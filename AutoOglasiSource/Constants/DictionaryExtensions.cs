
using AutoOglasiSource.Model.Advertisement;

namespace AutoOglasiSource.Constants
{
    public static class DictionaryExtensions
    {
        public static List<EnumValue> GetValues(Dictionary<int, string> dictionary)
        {
            List<EnumValue> values = new();
            foreach (var item in dictionary)
            {
                //For each value in dictionary, add a new EnumValue instance
                values.Add(new EnumValue()
                {
                    Name = item.Value,
                    Id = item.Key
                });
            }
            return values;
        }
    }
}
