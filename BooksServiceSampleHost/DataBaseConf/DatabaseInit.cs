//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using BooksServiceSampleHost.Models;
//using SQLite;

//namespace BooksServiceSampleHost.DataBaseConf
//{
//    public class DatabaseInit
//    {
//        public DatabaseInit()
//        {
//            InitializeAsync().SafeFireAndForget(false);
//        }
//        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(()=>
//        {
//            return new SQLiteAsyncConnection(Contants.DatabasePath,Contants.Flags);
//        });
//        public static SQLiteAsyncConnection DatabaseConnection => lazyInitializer.Value;
//        static bool initialized = false;
//        async Task InitializeAsync()
//        {
//            if (!initialized)
//            {
//                if (!DatabaseConnection.TableMappings.Any(m=>m.MappedType.Name == nameof(BookChapter)))
//                {
//                    await DatabaseConnection.CreateTablesAsync(CreateFlags.None,typeof(BookChapter));
//                }
//                initialized = true;
//            }
//        }
//    }
//    public static class TaskExtensions
//    {
//        public static async void SafeFireAndForget(this Task task,bool returnToCallingContext,Action<Exception> onException = null)
//        {
//            try
//            {
//                await task.ConfigureAwait(returnToCallingContext);
//            }
//            catch (Exception ex) when (onException != null)
//            {
//                onException(ex);
//            }
//        }
//    }
//}
