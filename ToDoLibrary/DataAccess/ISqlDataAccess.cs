namespace ToDoLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string storedProcedure, U parameter, string connectionStringName);
        Task SaveData<T>(string storedProcedure, T parameter, string connectionStringName);
    }
}