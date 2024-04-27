namespace DemoAppOne.Helper
{
    public static class Constants
    {
        public static SQLiteAsyncConnection Database;
        //local datebase file name
        public const string DatabaseFilename = "DemoAppOneSQLite.db6";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        // Method to initialize the database
        public async static Task Init()
        {
            try
            {
                //// Check if the database has already been initialized
                if (Database is not null)
                    return;

                // Initialize the database connection
                Database = new SQLiteAsyncConnection(Helper.Constants.DatabasePath, Helper.Constants.Flags);
                // Create the Product table if it doesn't exist
                await Database.CreateTableAsync<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
