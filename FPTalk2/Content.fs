module Content

open System.Threading.Tasks
open System
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Builder
open System.Runtime.Serialization.Json
open System.IO
open System.Text
open Newtonsoft.Json

type RequestMethod =
    | Trace
    | Put
    | Post
    | Patch
    | Options
    | Head
    | Get
    | Delete
    | Connect

type Skip = Func<Task>

type Filter = 
    HttpContext-> bool

type RequestHandler = 
    HttpContext -> Task

type Middleware = 
    Filter -> RequestHandler -> HttpContext -> Skip -> Task

type Adapter<'a, 'b> = ('a -> 'b) -> (HttpContext -> Task)

/// Object to Json 
let internal json<'t> (ms:Stream) (myObj:'t) =
    use writer = new StreamWriter(ms)
    writer.Write(JsonConvert.SerializeObject(myObj))
    ms

/// Object from Json 
let internal unjson<'t> (jsonStream:Stream)  : 't =
    use reader = new StreamReader(jsonStream)
    JsonConvert.DeserializeObject<'t>(reader.ReadToEnd())

let useEx app f = UseExtensions.Use(app, new Func<HttpContext, Func<Task>, Task>(f)) |> ignore

let Middleware:Middleware = fun filter b context continuewith -> 
    if filter context then b context else continuewith.Invoke()

let Jsonify:Adapter<'a, 'b> = (fun f a -> 
    json a.Response.Body (f (unjson a.Request.Body)) |> ignore
    Task.CompletedTask)

let PathFilter (path:String) (h:HttpContext) =  h.Request.Path = new PathString(path)

let MethodFilter (method:RequestMethod) (h:HttpContext) = //true
    match method with
        | RequestMethod.Trace -> h.Request.Method = "TRACE"
        | RequestMethod.Put -> h.Request.Method = "PUT"
        | RequestMethod.Post -> h.Request.Method = "POST"
        | RequestMethod.Patch -> h.Request.Method = "PATCH"
        | RequestMethod.Options -> h.Request.Method = "OPTIONS"
        | RequestMethod.Head -> h.Request.Method = "HEAD"
        | RequestMethod.Get -> h.Request.Method = "GET"
        | RequestMethod.Delete -> h.Request.Method = "DELETE"
        | RequestMethod.Connect -> h.Request.Method = "CONNECT"

//method.ToString()

let ContentFilter (content:string) (h:HttpContext) = 
    h.Request.ContentType = content

