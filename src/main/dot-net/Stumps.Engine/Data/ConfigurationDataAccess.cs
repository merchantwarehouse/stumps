﻿namespace Stumps.Data
{

    using System;
    using Stumps.Utility;

    /// <summary>
    ///     A class that provides an implementation of <see cref="T:Stumps.Data.IConfigurationDataAccess"/>
    ///     that uses a JSON file to configuration information.
    /// </summary>
    public class ConfigurationDataAccess : IConfigurationDataAccess
    {

        private readonly string _configurationFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Stumps.Data.ConfigurationDataAccess"/> class.
        /// </summary>
        /// <param name="configurationFile">The path to the file containing configuration information in JSON format.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="configurationFile"/> is <c>null</c>.</exception>
        public ConfigurationDataAccess(string configurationFile)
        {

            if (configurationFile == null)
            {
                throw new ArgumentNullException("configurationFile");
            }

            _configurationFile = configurationFile;

        }

        /// <summary>
        /// Loads the <see cref="T:Stumps.Data.ConfigurationEntity" /> from the data store.
        /// </summary>
        /// <returns>
        /// A <see cref="T:Stumps.Data.ConfigurationEntity" /> containing the configuration information for the application.
        /// </returns>
        public ConfigurationEntity LoadConfiguration()
        {

            var loadedConfiguration = JsonUtility.DeserializeFromFile<ConfigurationEntity>(_configurationFile);
            return loadedConfiguration;

        }

        /// <summary>
        /// Persists the specified <see cref="T:Stumps.Data.ConfigurationEntity" /> to the data store.
        /// </summary>
        /// <param name="value">The <see cref="T:Stumps.Data.ConfigurationEntity" /> to persist in the store.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public void SaveConfiguration(ConfigurationEntity value)
        {

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            JsonUtility.SerializeToFile(value, _configurationFile);

        }

    }

}