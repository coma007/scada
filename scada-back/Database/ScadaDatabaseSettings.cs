using static System.String;

namespace scada_back.Database;

public class ScadaDatabaseSettings : IScadaDatabaseSettings
{
    public string DatabaseName { get; set; } = Empty;
    public string ConnectionString { get; set; } = Empty;
    public string UsersCollectionName { get; set; } = Empty;
    public string DriversStateCollectionName { get; set; } = Empty;
    public string TagsCollectionName { get; set; } = Empty;
    public string TagsDeletedCollectionName { get; set; }= Empty;
    public string TagsHistoryCollectionName { get; set; } = Empty;
    public string AlarmsCollectionName { get; set; } = Empty;
    public string AlarmsDeletedCollectionName { get; set; }= Empty;
    public string AlarmsHistoryCollectionName { get; set; } = Empty;
}