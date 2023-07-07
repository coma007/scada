namespace scada_back.Infrastructure.Database;

public interface IScadaDatabaseSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
    string UsersCollectionName { get; set; }
    string DriversStateCollectionName { get; set; }
    string TagsCollectionName { get; set; }
    string TagsDeletedCollectionName { get; set; }
    string TagsHistoryCollectionName { get; set; }
    string AlarmsCollectionName { get; set; }
    string AlarmsDeletedCollectionName { get; set; }
    string AlarmsHistoryCollectionName { get; set; }
}