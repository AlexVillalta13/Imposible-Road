﻿using System;
using System.Collections.Generic;
using System.Reflection;
#if NET20
using Bayat.Json.Utilities.LinqBridge;
#else
using System.Linq;
#endif

namespace Bayat.Core.Reflection
{

    public static class TypeExtensions
    {
#if DOTNET || (UNITY_WSA || UNITY_WINRT)
#if !DOTNET
        private static BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;

        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetGetMethod(false);
        }

        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo, bool nonPublic)
        {
            MethodInfo getMethod = propertyInfo.GetMethod;
            if (getMethod != null && (getMethod.IsPublic || nonPublic))
            {
                return getMethod;
            }

            return null;
        }

        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetSetMethod(false);
        }

        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo, bool nonPublic)
        {
            MethodInfo setMethod = propertyInfo.SetMethod;
            if (setMethod != null && (setMethod.IsPublic || nonPublic))
            {
                return setMethod;
            }

            return null;
        }
#endif

        public static bool IsSubclassOf(this Type type, Type c)
        {
            return type.GetTypeInfo().IsSubclassOf(c);
        }

#if !DOTNET
        public static bool IsAssignableFrom(this Type type, Type c)
        {
            return type.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
        }
#endif

        public static bool IsInstanceOfType(this Type type, object o)
        {
            if (o == null)
            {
                return false;
            }

            return type.IsAssignableFrom(o.GetType());
        }
#endif

        public static MethodInfo Method(this Delegate d)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return d.Method;
#else
            return d.GetMethodInfo();
#endif
        }

        public static MemberTypes MemberType(this MemberInfo memberInfo)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT) || PORTABLE40)
            return memberInfo.MemberType;
#else
            if (memberInfo is PropertyInfo)
            {
                return MemberTypes.Property;
            }
            else if (memberInfo is FieldInfo)
            {
                return MemberTypes.Field;
            }
            else if (memberInfo is EventInfo)
            {
                return MemberTypes.Event;
            }
            else if (memberInfo is MethodInfo)
            {
                return MemberTypes.Method;
            }
            else
            {
                return MemberTypes.Custom;
            }
#endif
        }

        public static bool ContainsGenericParameters(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.ContainsGenericParameters;
#else
            return type.GetTypeInfo().ContainsGenericParameters;
#endif
        }

        public static bool IsInterface(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsInterface;
#else
            return type.GetTypeInfo().IsInterface;
#endif
        }

        public static bool IsGenericType(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsGenericType;
#else
            return type.GetTypeInfo().IsGenericType;
#endif
        }

        public static bool IsGenericTypeDefinition(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsGenericTypeDefinition;
#else
            return type.GetTypeInfo().IsGenericTypeDefinition;
#endif
        }

        public static Type BaseType(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.BaseType;
#else
            return type.GetTypeInfo().BaseType;
#endif
        }

        public static Assembly Assembly(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.Assembly;
#else
            return type.GetTypeInfo().Assembly;
#endif
        }

        public static bool IsBasic(this Type type)
        {
            if (type == typeof(string) || type == typeof(decimal))
            {
                return true;
            }

            if (type.IsEnum())
            {
                return true;
            }

            if (type.IsPrimitive())
            {
                if (type == typeof(IntPtr) || type == typeof(UIntPtr))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public static bool IsNumeric(this Type type)
        {
            return TypeUtility.NumericTypes.Contains(type);
        }

        public static bool IsNumericConstruct(this Type type)
        {
            return TypeUtility.NumericConstructTypes.Contains(type);
        }

        public static bool IsStatic(this Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }

        public static bool IsConcrete(this Type type)
        {
            return !type.IsAbstract && !type.IsInterface && !type.ContainsGenericParameters;
        }

        public static bool IsReferenceType(this Type type)
        {
            return !type.IsValueType;
        }

        public static bool IsNullable(this Type type)
        {
            // http://stackoverflow.com/a/1770232
            return type.IsReferenceType() || Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsStruct(this Type type)
        {
            return type.IsValueType && !type.IsPrimitive && !type.IsEnum;
        }

        public static bool IsEnum(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsEnum;
#else
            return type.GetTypeInfo().IsEnum;
#endif
        }

        public static bool IsPrimitive(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsPrimitive;
#else
            return type.GetTypeInfo().IsPrimitive;
#endif
        }

        public static bool IsClass(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsClass;
#else
            return type.GetTypeInfo().IsClass;
#endif
        }

        public static bool IsSealed(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsSealed;
#else
            return type.GetTypeInfo().IsSealed;
#endif
        }

#if (PORTABLE40 || DOTNET || (UNITY_WSA || UNITY_WINRT))
        public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags, object placeholder1, Type propertyType, IList<Type> indexParameters, object placeholder2)
        {
            IEnumerable<PropertyInfo> propertyInfos = type.GetProperties(bindingFlags);

            return propertyInfos.Where(p =>
            {
                if (name != null && name != p.Name)
                {
                    return false;
                }
                if (propertyType != null && propertyType != p.PropertyType)
                {
                    return false;
                }
                if (indexParameters != null)
                {
                    if (!p.GetIndexParameters().Select(ip => ip.ParameterType).SequenceEqual(indexParameters))
                    {
                        return false;
                    }
                }

                return true;
            }).SingleOrDefault();
        }

        public static IEnumerable<MemberInfo> GetMember(this Type type, string name, MemberTypes memberType, BindingFlags bindingFlags)
        {
#if (UNITY_WSA || UNITY_WINRT)
            return type.GetMemberInternal(name, memberType, bindingFlags);
#else
            return type.GetMember(name, bindingFlags).Where(m =>
            {
                if (m.MemberType() != memberType)
                {
                    return false;
                }

                return true;
            });
#endif
        }
#endif

#if (DOTNET || (UNITY_WSA || UNITY_WINRT))
        public static MethodInfo GetBaseDefinition(this MethodInfo method)
        {
            return method.GetRuntimeBaseDefinition();
        }
#endif

#if (DOTNET || (UNITY_WSA || UNITY_WINRT))
        public static bool IsDefined(this Type type, Type attributeType, bool inherit)
        {
            return type.GetTypeInfo().CustomAttributes.Any(a => a.AttributeType == attributeType);
        }

#if !DOTNET
        public static MethodInfo GetMethod(this Type type, string name)
        {
            return type.GetMethod(name, DefaultFlags);
        }

        public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetDeclaredMethod(name);
        }

        public static MethodInfo GetMethod(this Type type, IList<Type> parameterTypes)
        {
            return type.GetMethod(null, parameterTypes);
        }

        public static MethodInfo GetMethod(this Type type, string name, IList<Type> parameterTypes)
        {
            return type.GetMethod(name, DefaultFlags, null, parameterTypes, null);
        }

        public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags, object placeHolder1, IList<Type> parameterTypes, object placeHolder2)
        {
            return type.GetTypeInfo().DeclaredMethods.Where(
                m =>
                {
                    if (name != null && m.Name != name)
                    {
                        return false;
                    }

                    if (!TestAccessibility(m, bindingFlags))
                    {
                        return false;
                    }

                    return m.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes);
                }).SingleOrDefault();
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type)
        {
            return type.GetConstructors(DefaultFlags);
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags bindingFlags)
        {
            return type.GetConstructors(bindingFlags, null);
        }

        private static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags bindingFlags, IList<Type> parameterTypes)
        {
            return type.GetTypeInfo().DeclaredConstructors.Where(
                c =>
                {
                    if (!TestAccessibility(c, bindingFlags))
                    {
                        return false;
                    }

                    if (parameterTypes != null && !c.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes))
                    {
                        return false;
                    }

                    return true;
                });
        }

        public static ConstructorInfo GetConstructor(this Type type, IList<Type> parameterTypes)
        {
            return type.GetConstructor(DefaultFlags, null, parameterTypes, null);
        }

        public static ConstructorInfo GetConstructor(this Type type, BindingFlags bindingFlags, object placeholder1, IList<Type> parameterTypes, object placeholder2)
        {
            return type.GetConstructors(bindingFlags, parameterTypes).SingleOrDefault();
        }

        public static MemberInfo[] GetMember(this Type type, string member)
        {
            return type.GetMemberInternal(member, null, DefaultFlags);
        }

        public static MemberInfo[] GetMember(this Type type, string member, BindingFlags bindingFlags)
        {
            return type.GetMemberInternal(member, null, bindingFlags);
        }

        public static MemberInfo[] GetMemberInternal(this Type type, string member, MemberTypes? memberType, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetMembersRecursive().Where(m =>
                m.Name == member &&
                // test type before accessibility - accessibility doesn't support some types
                (memberType == null || m.MemberType() == memberType) &&
                TestAccessibility(m, bindingFlags)).ToArray();
        }

        public static MemberInfo GetField(this Type type, string member)
        {
            return type.GetField(member, DefaultFlags);
        }

        public static MemberInfo GetField(this Type type, string member, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetDeclaredField(member);
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type, BindingFlags bindingFlags)
        {
            IList<PropertyInfo> properties = (bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
                ? type.GetTypeInfo().DeclaredProperties.ToList()
                : type.GetTypeInfo().GetPropertiesRecursive();

            return properties.Where(p => TestAccessibility(p, bindingFlags));
        }

        private static IList<MemberInfo> GetMembersRecursive(this TypeInfo type)
        {
            TypeInfo t = type;
            IList<MemberInfo> members = new List<MemberInfo>();
            while (t != null)
            {
                foreach (MemberInfo member in t.DeclaredMembers)
                {
                    if (!members.Any(p => p.Name == member.Name))
                    {
                        members.Add(member);
                    }
                }
                t = (t.BaseType != null) ? t.BaseType.GetTypeInfo() : null;
            }

            return members;
        }

        private static IList<PropertyInfo> GetPropertiesRecursive(this TypeInfo type)
        {
            TypeInfo t = type;
            IList<PropertyInfo> properties = new List<PropertyInfo>();
            while (t != null)
            {
                foreach (PropertyInfo member in t.DeclaredProperties)
                {
                    if (!properties.Any(p => p.Name == member.Name))
                    {
                        properties.Add(member);
                    }
                }
                t = (t.BaseType != null) ? t.BaseType.GetTypeInfo() : null;
            }

            return properties;
        }

        private static IList<FieldInfo> GetFieldsRecursive(this TypeInfo type)
        {
            TypeInfo t = type;
            IList<FieldInfo> fields = new List<FieldInfo>();
            while (t != null)
            {
                foreach (FieldInfo member in t.DeclaredFields)
                {
                    if (!fields.Any(p => p.Name == member.Name))
                    {
                        fields.Add(member);
                    }
                }
                t = (t.BaseType != null) ? t.BaseType.GetTypeInfo() : null;
            }

            return fields;
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().DeclaredMethods;
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetProperty(name, DefaultFlags);
        }

        public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetDeclaredProperty(name);
        }

        public static IEnumerable<FieldInfo> GetFields(this Type type)
        {
            return type.GetFields(DefaultFlags);
        }

        public static IEnumerable<FieldInfo> GetFields(this Type type, BindingFlags bindingFlags)
        {
            IList<FieldInfo> fields = (bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
                ? type.GetTypeInfo().DeclaredFields.ToList()
                : type.GetTypeInfo().GetFieldsRecursive();

            return fields.Where(f => TestAccessibility(f, bindingFlags)).ToList();
        }

        private static bool TestAccessibility(PropertyInfo member, BindingFlags bindingFlags)
        {
            if (member.GetMethod != null && TestAccessibility(member.GetMethod, bindingFlags))
            {
                return true;
            }

            if (member.SetMethod != null && TestAccessibility(member.SetMethod, bindingFlags))
            {
                return true;
            }

            return false;
        }

        private static bool TestAccessibility(MemberInfo member, BindingFlags bindingFlags)
        {
            if (member is FieldInfo)
            {
                return TestAccessibility((FieldInfo)member, bindingFlags);
            }
            else if (member is MethodBase)
            {
                return TestAccessibility((MethodBase)member, bindingFlags);
            }
            else if (member is PropertyInfo)
            {
                return TestAccessibility((PropertyInfo)member, bindingFlags);
            }

            throw new Exception("Unexpected member type.");
        }

        private static bool TestAccessibility(FieldInfo member, BindingFlags bindingFlags)
        {
            bool visibility = (member.IsPublic && bindingFlags.HasFlag(BindingFlags.Public)) ||
                              (!member.IsPublic && bindingFlags.HasFlag(BindingFlags.NonPublic));

            bool instance = (member.IsStatic && bindingFlags.HasFlag(BindingFlags.Static)) ||
                            (!member.IsStatic && bindingFlags.HasFlag(BindingFlags.Instance));

            return visibility && instance;
        }

        private static bool TestAccessibility(MethodBase member, BindingFlags bindingFlags)
        {
            bool visibility = (member.IsPublic && bindingFlags.HasFlag(BindingFlags.Public)) ||
                              (!member.IsPublic && bindingFlags.HasFlag(BindingFlags.NonPublic));

            bool instance = (member.IsStatic && bindingFlags.HasFlag(BindingFlags.Static)) ||
                            (!member.IsStatic && bindingFlags.HasFlag(BindingFlags.Instance));

            return visibility && instance;
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

        public static IEnumerable<Type> GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type)
        {
            return type.GetTypeInfo().DeclaredMethods;
        }
#endif
#endif

        public static bool IsAbstract(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsAbstract;
#else
            return type.GetTypeInfo().IsAbstract;
#endif
        }

        public static bool IsVisible(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsVisible;
#else
            return type.GetTypeInfo().IsVisible;
#endif
        }

        public static bool IsValueType(this Type type)
        {
#if !(DOTNET || (UNITY_WSA || UNITY_WINRT))
            return type.IsValueType;
#else
            return type.GetTypeInfo().IsValueType;
#endif
        }

        public static bool AssignableToTypeName(this Type type, string fullTypeName, out Type match)
        {
            Type current = type;

            while (current != null)
            {
                if (string.Equals(current.FullName, fullTypeName, StringComparison.Ordinal))
                {
                    match = current;
                    return true;
                }

                current = current.BaseType();
            }

            foreach (Type i in type.GetInterfaces())
            {
                if (string.Equals(i.Name, fullTypeName, StringComparison.Ordinal))
                {
                    match = type;
                    return true;
                }
            }

            match = null;
            return false;
        }

        public static bool AssignableToTypeName(this Type type, string fullTypeName)
        {
            Type match;
            return type.AssignableToTypeName(fullTypeName, out match);
        }

        public static bool ImplementInterface(this Type type, Type interfaceType)
        {
            for (Type currentType = type; currentType != null; currentType = currentType.BaseType())
            {
                IEnumerable<Type> interfaces = currentType.GetInterfaces();
                foreach (Type i in interfaces)
                {
                    if (i == interfaceType || (i != null && i.ImplementInterface(interfaceType)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static object Default(this Type type)
        {
            if (type.IsReferenceType())
            {
                return null;
            }

            object defaultPrimitive = null;
            if (!TypeUtility.DefaultPrimitives.TryGetValue(type, out defaultPrimitive))
            {
                defaultPrimitive = Activator.CreateInstance(type);
            }

            return defaultPrimitive;
        }

#if UNITY_WSA && !UNITY_EDITOR
        public static string GetFriendlyName(this Type type)
        {
            TypeInfo typeInfo = type.GetTypeInfo();
            string name = type.FullName;
            if (typeInfo.IsGenericType)
            {
                name = type.FullName.Split('`')[0] + "<" + string.Join(
                    ", ",
                    type.GetGenericArguments().Select(x => x.GetFriendlyName()).ToArray()) + ">";
            }
            else
            {
                name = type.FullName;
            }
            name = name.Replace("+", ".");
            return name;
        }
#else
        public static string GetFriendlyName(this Type type)
        {
            if (type.IsGenericParameter)
            {
                return type.Name;
            }
            string name = type.FullName;
            if (type.IsGenericType)
            {
                name = type.FullName.Split('`')[0] + "<" + string.Join(
                    ", ",
                    type.GetGenericArguments().Select(x => x.GetFriendlyName()).ToArray()) + ">";
            }
            else
            {
                name = type.FullName;
            }
            name = name.Replace("+", ".");
            return name;
        }
#endif

    }

}