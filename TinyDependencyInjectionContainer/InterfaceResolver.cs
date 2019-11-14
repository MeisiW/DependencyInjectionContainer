using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace TinyDependencyInjectionContainer
{
    public class InterfaceResolver
    {
        private Dictionary<Type, Type> KnownTypes { get; }
        public InterfaceResolver(string configFileName)
        {
            string[] configLines;
            try
            {
                configLines = File.ReadAllLines(configFileName);
            }
            catch(Exception e)
            {
                throw new ArgumentException($"Impossible to read file {configFileName}", e);
            }
            KnownTypes = new Dictionary<Type, Type>();
            foreach(var line in configLines)
            {
                if(line.Length != 0 && '#' != line[0])  //se la linea non è vuota e non è un commento
                {
                    var parts = line.Split('*');       //la linea viene divisa in sottostringhe ogni volta che incontra il carattere '*'
                    if (4 != parts.Length)             //se le sottostringhe create non sono lunghe 4 
                        throw new ArgumentException($"Config file with not well-formed line ({line}");  //lancia eccezione
                    GetTypeFromConfig(parts[0], parts[1], out var interfaceType);   //prendo la I e la II sottostringa e metto in interfaceType la prima parte della II sottostringa (che è il tipo della I)
                    GetTypeFromConfig(parts[2], parts[3], out var implementationType); //prendo la III e la IV e metto in implementationType il tipo di implementazione della III (prendendolo dalla IV)
                    if (!interfaceType.IsAssignableFrom(implementationType)) //se l'interfaceType trovato non è assegnabile al tipo di implementazione trovato
                    {
                        throw new ArgumentException($"Type {implementationType.Name} does not implement {interfaceType.Name}");  //lancia eccezione
                    }
                    try
                    {
                        KnownTypes.Add(interfaceType, implementationType);  //aggiungo al dizionario l'interfaccia e l'implementazione trovati
                    }
                    catch(Exception e)
                    {
                        throw new ArgumentException($"Duplicated association for interface type {interfaceType.Name}", e);
                    }
                }
            }

            void GetTypeFromConfig(string assemblyFile, string typeName, out Type type)
            {
                Assembly assembly;
                try
                {
                    assembly = Assembly.LoadFrom(assemblyFile);
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                    throw new ArgumentException($"Impossible to load {assemblyFile}", e);
                }
                try
                {
                    type = assembly.GetType(typeName, true);
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                    throw new ArgumentException($"{assemblyFile} does not contain accessible type {typeName}", e);
                }
            }
        }
    }
}
