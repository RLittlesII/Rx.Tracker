using Nuke.Common.Tooling;
using System.ComponentModel;

[TypeConverter(typeof(TypeConverter<Configuration>))]
public class Configuration : Enumeration
{
    public static implicit operator string(Configuration configuration) => configuration.Value;
    public static Configuration Debug = new() { Value = nameof(Debug) };
    public static Configuration Release = new() { Value = nameof(Release) };
}