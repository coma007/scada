namespace scada_back.Database;

public class ScadaDatabaseSettings : IScadaDatabaseSettings
{
    public string DatabaseName { get; set; } = String.Empty;
    public string ConnectionString { get; set; } = String.Empty;
    public string UsersCollectionName { get; set; } = String.Empty;
    public string DriversStateCollectionName { get; set; } = String.Empty;
    public string TagsCollectionName { get; set; } = String.Empty;
    public string TagsHistoryCollectionName { get; set; } = String.Empty;
    public string AlarmsCollectionName { get; set; } = String.Empty;
    public string AlarmsHistoryCollectionName { get; set; } = String.Empty;
}