namespace scada_back.Database;

public interface IScadaDatabaseSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
    string UsersCollectionName { get; set; }
    string DriversStateCollectionName { get; set; }
    string TagsCollectionName { get; set; }
    string TagsHistoryCollectionName { get; set; }
    string AlarmsCollectionName { get; set; }
    string AlarmsHistoryCollectionName { get; set; }
}