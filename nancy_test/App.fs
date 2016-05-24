//only for interactive
//#r Will get things out of the GAC, but fully qualifying reference paths is a pain. 
//You can use #I however to add a ‘search path’ to indicate where to look for references.
#if INTERACTIVE
#I @"../packages/"
#r "Nancy.1.4.1/lib/net40/Nancy.dll"
#r "Nancy.Hosting.Self.1.4.1/lib/net40/Nancy.Hosting.Self.dll"
#endif

namespace mahamudra.system.web

open Nancy

// :> obj Up-casting operator. Get function has to return an object
type App() as this =
    inherit NancyModule()
    do
        this.Get.["/"] <- fun _ -> "Hello World!" :> obj
        this.Get.["/Fsharp"] <- fun _ -> "I can into F#" :> obj
        this.Get.["/Json"] <- fun _ -> this.Response.AsJson([ "Test" ]) :> obj
        this.Get.["/"] <- fun _ -> "Rewrite Hello World!" :> obj
        this.Get.["/Complex"] <- fun _ -> 
             let response = this.Response.AsJson(["This is my Response"])
             response.ContentType <- "application/json"
             response.Headers.Add("Funky-Header", "Funky-Header-Value")
             response :> obj
        this.Post.["/Post/{test}"] <- fun parameters -> 
              let value = (parameters :?> Nancy.DynamicDictionary).["test"]
              let response = this.Response.AsJson([ sprintf "test %O" value ])
              response :> obj
        this.Get.["/foo", true] <- fun ctx ct ->
          async {
              return "bar" :> obj
          }
          |> Async.StartAsTask
