// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System
open Nancy.Hosting.Self
open Nancy
open mahamudra.system.web

[<EntryPoint>]
let main args = 
////Namespace Reservations
////Windows requires applications to register the part of the HTTP URL namespace, that they will be, listening on, or for the application to run with administrator privileges. This is how the MSDN documentation describes the requirement
////Namespace reservation assigns the rights for a portion of the HTTP URL namespace to a particular group of users. A reservation gives those users the right to create services that listen on that portion of the namespace. Reservations are URL prefixes, meaning that the reservation covers all sub-paths of the reservation path.
////Failing to do so is going to result in an access denied, from the operating system, when the application tries to open and listen on a certain url and port.
//    let nancy = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:" + "8100"))
    let hostConfigs = new HostConfiguration()
    hostConfigs.UrlReservations.CreateAutomatically <- true 
    StaticConfiguration.DisableErrorTraces <- false
////    'use' bindings are not permitted in modules and are treated as 'let' bindings
//    use nancy = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:" + "8101/nancy/"),
//                                                 new DefaultNancyBootstrapper(), 
//                                                 hostConfigs)
//    let nancy = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:" + "8101/"),
//                                                 new DefaultNancyBootstrapper(), 
//                                                 hostConfigs)
    let uri =new Uri("http://localhost:" + "8101/");
    let nancy = new Nancy.Hosting.Self.NancyHost(uri)
    //netsh http add urlacl url="http://+:8101/" user="Everyone"
    //netsh http delete urlacl url=http://+:8101/
    nancy.Start()
    Console.WriteLine("Server started at:" + System.DateTime.Now.ToString() + " on url: " + uri.ToString())
    while true do Console.ReadLine() |> ignore
    nancy.Stop()
    0