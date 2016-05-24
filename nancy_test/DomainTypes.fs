namespace Mahamudra.System.Models
open System
open System.Data

type Supplier = { SupplierID:Int64;CompanyName:string;ContactName:string; ContactTitle:string; Address:string}

type NumberOfOrdersByEmployee= { NumberOfOrders:Int64;  EmployeeName:string}

module Binding =
    let bindSuppliers (reader: IDataReader) =
        { 
            SupplierID =  unbox(reader.["SupplierID"])
            CompanyName = unbox(reader.["CompanyName"]) 
            ContactTitle = if DBNull.Value.Equals(reader.["ContactTitle"]) then String.Empty else unbox(reader.["ContactTitle"]) 
            ContactName= if DBNull.Value.Equals(reader.["ContactName"]) then String.Empty else unbox(reader.["ContactName"]) 
            Address = if DBNull.Value.Equals(reader.["Address"]) then String.Empty else unbox(reader.["Address"]) 
        }  

    let bindCountOrders(reader: IDataReader) =
        { 
            NumberOfOrders =  unbox(reader.["NumberOfOrders"])
            EmployeeName = unbox(reader.["LastName"]) + " " + unbox(reader.["FirstName"]) 
        }
