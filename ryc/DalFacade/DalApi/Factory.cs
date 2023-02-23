using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Reflection;
using static DalApi.DalConfig;

namespace DalApi;

public static class Factory
{
    public static IDal? Get()
    {
        string dalType = s_dalName
            ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
        string dal = s_dalPackages[dalType]
           ?? throw new DalConfigException($"Package for {dalType} is not found in packages list");
        string dalNamespace = s_dalNamespaces[dalType]
            ?? throw new DalConfigException($"namespace for {dalType} is not found in namespaces list");
        string dalClass = s_dalClasses[dalType]
            ?? throw new DalConfigException($"class for {dalType} is not found in classes list");

        try
        {
            Assembly.Load(dal ?? throw new DalConfigException($"Package {dal} is null"));
        }
        catch (Exception)
        {
            throw new DalConfigException("Failed to load {dal}.dll package");
        }

        Type? type = Type.GetType($"{dalNamespace}.{dalClass}, {dal}")
            ?? throw new DalConfigException($"Class {dalNamespace}.{dalClass} was not found in {dal}.dll");

        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                   .GetValue(null) as IDal
            ?? throw new DalConfigException($"Class {dalNamespace}.{dalClass} is not singleton or Instance property not found");
    }
}

