namespace FPTalk2


open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Http
open Content

//type Middleware = (HttpContext-> bool) -> (HttpContext -> Task) -> HttpContext -> Func<Task> -> Task
type Startup private () =
    let ( <&> ) a b = Middleware a b
    let ( <+> ) (a:Filter) (b:Filter) = fun h -> (a h) && (b h)

    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration


    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddMvc() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        let register = useEx app
        
        register ((MethodFilter RequestMethod.Post <+> PathFilter "/") <&> (Jsonify (fun c -> "Hello " + c)))
        
        register ((fun c -> true) <&> (Jsonify (fun c -> "Goodbye" + c)))

        app.UseMvc() |> ignore

    member val Configuration : IConfiguration = null with get, set