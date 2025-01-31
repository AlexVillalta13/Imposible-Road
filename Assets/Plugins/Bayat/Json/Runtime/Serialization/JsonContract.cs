﻿#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using Bayat.Core.Reflection;
using Bayat.Json.Shims;
using Bayat.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Bayat.Json.Serialization
{
    [Preserve]
    public enum JsonContractType
    {
        None = 0,
        Object = 1,
        Array = 2,
        Primitive = 3,
        String = 4,
        Dictionary = 5,
        Dynamic = 6,
        Serializable = 7,
        Linq = 8
    }

    /// <summary>
    /// Handles <see cref="JsonSerializer"/> serialization callback events.
    /// </summary>
    /// <param name="o">The object that raised the callback event.</param>
    /// <param name="context">The streaming context.</param>
    [Preserve]
    public delegate void SerializationCallback(object o, StreamingContext context);

    /// <summary>
    /// Handles <see cref="JsonSerializer"/> serialization error callback events.
    /// </summary>
    /// <param name="o">The object that raised the callback event.</param>
    /// <param name="context">The streaming context.</param>
    /// <param name="errorContext">The error context.</param>
    [Preserve]
    public delegate void SerializationErrorCallback(object o, StreamingContext context, ErrorContext errorContext);

    /// <summary>
    /// Sets extension data for an object during deserialization.
    /// </summary>
    /// <param name="o">The object to set extension data on.</param>
    /// <param name="key">The extension data key.</param>
    /// <param name="value">The extension data value.</param>
    [Preserve]
    public delegate void ExtensionDataSetter(object o, string key, object value);

    /// <summary>
    /// Gets extension data for an object during serialization.
    /// </summary>
    /// <param name="o">The object to set extension data on.</param>
    [Preserve]
    public delegate IEnumerable<KeyValuePair<object, object>> ExtensionDataGetter(object o);

    /// <summary>
    /// Contract details for a <see cref="System.Type"/> used by the <see cref="JsonSerializer"/>.
    /// </summary>
    [Preserve]
    public abstract class JsonContract
    {
        public bool IsNullable;
        public bool IsConvertable;
        public bool IsEnum;
        public Type NonNullableUnderlyingType;
        public ReadType InternalReadType;
        public JsonContractType ContractType;
        public bool IsReadOnlyOrFixedSize;
        public bool IsSealed;
        public bool IsInstantiable;

        private List<SerializationCallback> _onDeserializedCallbacks;
        private IList<SerializationCallback> _onDeserializingCallbacks;
        private IList<SerializationCallback> _onSerializedCallbacks;
        private IList<SerializationCallback> _onSerializingCallbacks;
        private IList<SerializationErrorCallback> _onErrorCallbacks;
        private Type _createdType;

        /// <summary>
        /// Gets the underlying type for the contract.
        /// </summary>
        /// <value>The underlying type for the contract.</value>
        public Type UnderlyingType { get; private set; }

        /// <summary>
        /// Gets or sets the type created during deserialization.
        /// </summary>
        /// <value>The type created during deserialization.</value>
        public Type CreatedType
        {
            get { return _createdType; }
            set
            {
                _createdType = value;

                IsSealed = _createdType.IsSealed();
                IsInstantiable = !(_createdType.IsInterface() || _createdType.IsAbstract());
            }
        }

        /// <summary>
        /// Gets or sets whether this type contract is serialized as a reference.
        /// </summary>
        /// <value>Whether this type contract is serialized as a reference.</value>
        public bool? IsReference { get; set; }

        /// <summary>
        /// Gets or sets the default <see cref="JsonConverter" /> for this contract.
        /// </summary>
        /// <value>The converter.</value>
        public JsonConverter Converter { get; set; }

        // internally specified JsonConverter's to override default behavour
        // checked for after passed in converters and attribute specified converters
        public JsonConverter InternalConverter { get; set; }

        /// <summary>
        /// Gets or sets all methods called immediately after deserialization of the object.
        /// </summary>
        /// <value>The methods called immediately after deserialization of the object.</value>
        public IList<SerializationCallback> OnDeserializedCallbacks
        {
            get
            {
                if (_onDeserializedCallbacks == null)
                {
                    _onDeserializedCallbacks = new List<SerializationCallback>();
                }

                return _onDeserializedCallbacks;
            }
        }

        /// <summary>
        /// Gets or sets all methods called during deserialization of the object.
        /// </summary>
        /// <value>The methods called during deserialization of the object.</value>
        public IList<SerializationCallback> OnDeserializingCallbacks
        {
            get
            {
                if (_onDeserializingCallbacks == null)
                {
                    _onDeserializingCallbacks = new List<SerializationCallback>();
                }

                return _onDeserializingCallbacks;
            }
        }

        /// <summary>
        /// Gets or sets all methods called after serialization of the object graph.
        /// </summary>
        /// <value>The methods called after serialization of the object graph.</value>
        public IList<SerializationCallback> OnSerializedCallbacks
        {
            get
            {
                if (_onSerializedCallbacks == null)
                {
                    _onSerializedCallbacks = new List<SerializationCallback>();
                }

                return _onSerializedCallbacks;
            }
        }

        /// <summary>
        /// Gets or sets all methods called before serialization of the object.
        /// </summary>
        /// <value>The methods called before serialization of the object.</value>
        public IList<SerializationCallback> OnSerializingCallbacks
        {
            get
            {
                if (_onSerializingCallbacks == null)
                {
                    _onSerializingCallbacks = new List<SerializationCallback>();
                }

                return _onSerializingCallbacks;
            }
        }

        /// <summary>
        /// Gets or sets all method called when an error is thrown during the serialization of the object.
        /// </summary>
        /// <value>The methods called when an error is thrown during the serialization of the object.</value>
        public IList<SerializationErrorCallback> OnErrorCallbacks
        {
            get
            {
                if (_onErrorCallbacks == null)
                {
                    _onErrorCallbacks = new List<SerializationErrorCallback>();
                }

                return _onErrorCallbacks;
            }
        }

        /// <summary>
        /// Gets or sets the method called immediately after deserialization of the object.
        /// </summary>
        /// <value>The method called immediately after deserialization of the object.</value>
        [Obsolete("This property is obsolete and has been replaced by the OnDeserializedCallbacks collection.")]
        public MethodInfo OnDeserialized
        {
            get { return (OnDeserializedCallbacks.Count > 0) ? OnDeserializedCallbacks[0].Method() : null; }
            set
            {
                OnDeserializedCallbacks.Clear();
                OnDeserializedCallbacks.Add(CreateSerializationCallback(value));
            }
        }

        /// <summary>
        /// Gets or sets the method called during deserialization of the object.
        /// </summary>
        /// <value>The method called during deserialization of the object.</value>
        [Obsolete("This property is obsolete and has been replaced by the OnDeserializingCallbacks collection.")]
        public MethodInfo OnDeserializing
        {
            get { return (OnDeserializingCallbacks.Count > 0) ? OnDeserializingCallbacks[0].Method() : null; }
            set
            {
                OnDeserializingCallbacks.Clear();
                OnDeserializingCallbacks.Add(CreateSerializationCallback(value));
            }
        }

        /// <summary>
        /// Gets or sets the method called after serialization of the object graph.
        /// </summary>
        /// <value>The method called after serialization of the object graph.</value>
        [Obsolete("This property is obsolete and has been replaced by the OnSerializedCallbacks collection.")]
        public MethodInfo OnSerialized
        {
            get { return (OnSerializedCallbacks.Count > 0) ? OnSerializedCallbacks[0].Method() : null; }
            set
            {
                OnSerializedCallbacks.Clear();
                OnSerializedCallbacks.Add(CreateSerializationCallback(value));
            }
        }

        /// <summary>
        /// Gets or sets the method called before serialization of the object.
        /// </summary>
        /// <value>The method called before serialization of the object.</value>
        [Obsolete("This property is obsolete and has been replaced by the OnSerializingCallbacks collection.")]
        public MethodInfo OnSerializing
        {
            get { return (OnSerializingCallbacks.Count > 0) ? OnSerializingCallbacks[0].Method() : null; }
            set
            {
                OnSerializingCallbacks.Clear();
                OnSerializingCallbacks.Add(CreateSerializationCallback(value));
            }
        }

        /// <summary>
        /// Gets or sets the method called when an error is thrown during the serialization of the object.
        /// </summary>
        /// <value>The method called when an error is thrown during the serialization of the object.</value>
        [Obsolete("This property is obsolete and has been replaced by the OnErrorCallbacks collection.")]
        public MethodInfo OnError
        {
            get { return (OnErrorCallbacks.Count > 0) ? OnErrorCallbacks[0].Method() : null; }
            set
            {
                OnErrorCallbacks.Clear();
                OnErrorCallbacks.Add(CreateSerializationErrorCallback(value));
            }
        }

        /// <summary>
        /// Gets or sets the default creator method used to create the object.
        /// </summary>
        /// <value>The default creator method used to create the object.</value>
        public Func<object> DefaultCreator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the default creator is non public.
        /// </summary>
        /// <value><c>true</c> if the default object creator is non-public; otherwise, <c>false</c>.</value>
        public bool DefaultCreatorNonPublic { get; set; }

        public JsonContract(Type underlyingType)
        {
            ValidationUtils.ArgumentNotNull(underlyingType, nameof(underlyingType));

            UnderlyingType = underlyingType;

            IsNullable = ReflectionUtils.IsNullable(underlyingType);
            NonNullableUnderlyingType = (IsNullable && ReflectionUtils.IsNullableType(underlyingType)) ? Nullable.GetUnderlyingType(underlyingType) : underlyingType;

            CreatedType = NonNullableUnderlyingType;

            IsConvertable = ConvertUtils.IsConvertible(NonNullableUnderlyingType);
            IsEnum = NonNullableUnderlyingType.IsEnum();

            InternalReadType = ReadType.Read;
        }

        public void InvokeOnSerializing(object o, StreamingContext context)
        {
            if (_onSerializingCallbacks != null)
            {
                foreach (SerializationCallback callback in _onSerializingCallbacks)
                {
                    callback(o, context);
                }
            }
        }

        public void InvokeOnSerialized(object o, StreamingContext context)
        {
            if (_onSerializedCallbacks != null)
            {
                foreach (SerializationCallback callback in _onSerializedCallbacks)
                {
                    callback(o, context);
                }
            }
        }

        public void InvokeOnDeserializing(object o, StreamingContext context)
        {
            if (_onDeserializingCallbacks != null)
            {
                foreach (SerializationCallback callback in _onDeserializingCallbacks)
                {
                    callback(o, context);
                }
            }
        }

        public void InvokeOnDeserialized(object o, StreamingContext context)
        {
            if (_onDeserializedCallbacks != null)
            {
                foreach (SerializationCallback callback in _onDeserializedCallbacks)
                {
                    callback(o, context);
                }
            }
        }

        public void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
        {
            if (_onErrorCallbacks != null)
            {
                foreach (SerializationErrorCallback callback in _onErrorCallbacks)
                {
                    callback(o, context, errorContext);
                }
            }
        }

        public static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
        {
            return (o, context) => callbackMethodInfo.Invoke(o, new object[] { context });
        }

        public static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
        {
            return (o, context, econtext) => callbackMethodInfo.Invoke(o, new object[] { context, econtext });
        }
    }
}
