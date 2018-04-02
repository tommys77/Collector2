using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.Utilities
{
    public static class DTOMapper
    {

        /* THIS CODE WAS BORROWED FROM: 
         * 
         * https://weblogs.asp.net/gunnarpeipman/performance-using-dynamic-code-to-copy-property-values-of-two-objects
         * 
         * CREDIT GOES TO Gunnar Peipman
         * 
         */

        public static void CopyProperties(object source, object target)
        {
            var sourceType = target.GetType();
            var targetType = target.GetType();

            var propMap = GetMatchingProperties(sourceType, targetType);

            for (var i = 0; i < propMap.Count; i++)
            {
                var prop = propMap[i];
                var sourceValue = prop.SourceProperty.GetValue(source, null);
                prop.TargetProperty.SetValue(target, sourceValue, null);
            }
        }            



        // non-optimized version.
        private static IList<PropertyMap> GetMatchingProperties(Type sourceType, Type targetType)
        {
            var sourceProp = sourceType.GetProperties();
            var targetProp = targetType.GetProperties();

            var properties = (from s in sourceProp
                             from t in targetProp
                             where s.Name == t.Name &&
                             s.CanRead &&
                             t.CanRead &&
                             s.PropertyType.IsPublic &&
                             t.PropertyType.IsPublic &&
                             s.PropertyType == t.PropertyType &&
                             (
                             (s.PropertyType.IsValueType && t.PropertyType.IsValueType) ||
                             (s.PropertyType == typeof(string) && t.PropertyType == typeof(string))
                             )
                             select new PropertyMap
                             {
                                 SourceProperty = s,
                                 TargetProperty = t
                             }).ToList();

            return properties;
        }
    }
}
