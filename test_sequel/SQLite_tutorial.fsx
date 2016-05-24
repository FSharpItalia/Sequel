//Display grid ---
#load "DisplayData.fs"

open Mahamudra.System.Drawing

// types and binding functions
#load "DomainTypes.fs"

open Mahamudra.System.Models.Binding
open System
open System.IO
open System.Data

//Data ---
#r @"..\packages\System.Data.SQLite.Core.1.0.101.0\lib\net451\System.Data.SQLite.dll"
#r @"..\Sequel\bin\Debug\Sequel.dll"

open System.Data.SQLite
//Railway pattern
open Mahamudra.System.Railway
// sequel library
open Mahamudra.System.Data.Sql
open System.Runtime.InteropServices

module Kernel = 
    [<DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)>]
    extern IntPtr LoadLibrary(string lpFileName)

Kernel.LoadLibrary(Path.Combine(@"..\packages\System.Data.SQLite.Core.1.0.101.0\lib\net451\", "System.Data.SQLite.dll"))
Kernel.LoadLibrary
    (Path.Combine(@"..\packages\System.Data.SQLite.Core.1.0.101.0\build\net451\x64\", "SQLite.Interop.dll"))
Kernel.LoadLibrary(Path.Combine(@"..\SQLAccess\bin\Debug\", "SQLData.dll"))

//In SQLite, table rows normally have a 64-bit signed integer ROWID which is unique among all rows in the same table. 
//(WITHOUT ROWID tables are the exception.)
//The rowid is always available as an undeclared column named ROWID, OID, or _ROWID_ as long as those names are not also used by explicitly declared columns.
// If the table has a column of type INTEGER PRIMARY KEY then that column is another alias for the rowid.
[<Literal>]
let DB_PATH = __SOURCE_DIRECTORY__ + @"\data\northwind.db"

//let [<Literal>] DBPATH = __SOURCE_DIRECTORY__ + @"\data\northwind3.db" 
// connection string
//    //Basic
//    //Data Source=c:\mydb.db;Version=3;
//    //Version 2 is not supported by this class library.	SQLite
//    //In-Memory Database
//    //An SQLite database is normally stored on disk but the database can also be stored in memory. Read more about SQLite in-memory databases here.
//    //Data Source=:memory:;Version=3;New=True;
//    //SQLite
//    //Using UTF16
//    //Data Source=c:\mydb.db;Version=3;UseUTF16Encoding=True;
//    //SQLite9
//    //With password
//    //Data Source=c:\mydb.db;Version=3;Password=myPassword;
let CN_STRING = sprintf @"Data Source=%s;Version=3;" DB_PATH
// let's currying
let executeSQL (bind : IDataReader -> 'Result) (sql : string) = Sequel.uQuery SQLite CN_STRING CommandType.Text Seq.empty
let executeSQLParam (prm : seq<string * 'paramValue>) (bind : IDataReader -> 'Result) (sql : string) = 
    Sequel.uQuery SQLite CN_STRING CommandType.Text

[<Literal>]
let SELECT_SUPPLIERS = @"Select * from [Suppliers]"
let SELECT_TOP_SUPPLIERS = @"Select * from [Suppliers] limit 5"
let SELECT_WHERE_SUPPLIERS = @"Select * from [Suppliers] WHERE SupplierID>10"


//let a = Client.uQuery SQLite
//let b = a CN_STRING 
//let c = b CommandType.Text 
//let d = c Seq.empty
//let executeSQL = d bindSuppliers


//unsafe query without parameters
let suppliers = Sequel.uQuery SQLite CN_STRING CommandType.Text Seq.empty bindSuppliers SELECT_SUPPLIERS
let suppliers2 =  Sequel.uQuery SQLite CN_STRING CommandType.Text Seq.empty bindSuppliers  SELECT_TOP_SUPPLIERS 
let suppliers3 =  Sequel.uQuery SQLite CN_STRING CommandType.Text Seq.empty bindSuppliers SELECT_WHERE_SUPPLIERS 

try 
    //---------
    δ.draw (suppliers|> Seq.toList) "Display a list of suppliers" //print in grid windows form
    δ.draw (suppliers2 |> Seq.toList) "Display a list of suppliers" //print in grid windows form
    δ.draw (suppliers3 |> Seq.toList) "Display a list of suppliers" //print in grid windows form
with ex -> ex.Message |> ignore
