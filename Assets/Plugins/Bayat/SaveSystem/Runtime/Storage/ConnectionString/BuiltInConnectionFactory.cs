﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.SaveSystem.Storage
{

    /// <summary>
    /// The built-in connection factory.
    /// </summary>
    public class BuiltInConnectionFactory : IConnectionFactory
    {

        public IStorage CreateStorage(StorageConnectionString connectionString)
        {
            if (connectionString.Prefix == "disk")
            {
                string path = connectionString.Get("path", Application.persistentDataPath, "base-path", "folder", "directory", "dir");

                return new LocalDiskStorage(path);
            }

            if (connectionString.Prefix == "playerprefs")
            {
                string encodingName = connectionString.Get("encoding", StorageBase.DefaultTextEncodingName, "text-encoding", "encoding-name");
                bool useBase64 = connectionString.GetBoolean("usebase64", true, "use-base64");

                return new PlayerPrefsStorage(encodingName, useBase64);
            }

            return null;
        }

    }

}