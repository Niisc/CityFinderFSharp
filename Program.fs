open System
open System.Text.Json
open System.Text.Json.Serialization
open FSharp.Data

type  Zippotam = {
    [<JsonPropertyName("post code")>]
    PostCode: String
    [<JsonPropertyName("country")>]
    Country: String
    [<JsonPropertyName("places")>]
    Places: Place[]
}
and Place = {
    [<JsonPropertyName("place name")>]
    PlaceName: String
    [<JsonPropertyName("state")>]
    State: String
}

let HttpReqThingy country postalOrZip=
    Http.RequestString($"http://api.zippopotam.us/{country}/{postalOrZip}")
     
[<EntryPoint>]
let main argv =
    if argv.Length < 2 then 
        printfn "You need at least 2 arguments" 
        exit(1)

    let  response = HttpReqThingy argv.[0] argv.[1]

    let  deserializedStuff = JsonSerializer.Deserialize<Zippotam> response

    for city in deserializedStuff.Places do
        printfn $"City {city.PlaceName} Country {deserializedStuff.Country}" 
    0