namespace FSharp.ArangoDB

module ArangoDB =
    open System.Net.Http

    type CollectionKeyOptions = { Type: string }

    type CollectionOptions =
        { Name: string option
          KeyOptions: CollectionKeyOptions
          Type: int }

    type DatabaseOptions = { Name: string option }

    type IndexOptions =
        { Fields: string list
          Type: string
          Unique: bool }

    type Query<'T> =
        { BatchSize: int
          BindVars: Map<string, 'T>
          Query: string option }

    type QueryResult<'T> = { Id: string option; Result: 'T list }

    type SearchLinkOptions = { IncludeAllFields: bool }

    type SearchOptions =
        { Links: Map<string, SearchLinkOptions>
          Name: string option
          Type: string }

    type Client =
        { Authorization: string
          Client: HttpClient
          Database: string
          Debug: bool
          Host: string
          Target: string }

    (* Options *)

    let KeyTypeAutoIncrement = "AutoIncrement".ToLower()

    let KeyTypePadded = "Padded".ToLower()
    let KeyTypeTraditional = "Traditional".ToLower()
    let KeyTypeUUID = "UUID".ToLower()

    let DocumentCollection = 2
    let EdgeCollection = 3

    let PersistentIndex = "Persistent".ToLower()

    let SearchView = "ArangoSearch".ToLower()

    (* Default options *)

    let CollectionKeyOptions = { Type = KeyTypeUUID }

    let CollectionOptions =
        { KeyOptions = CollectionKeyOptions
          Name = None
          Type = DocumentCollection }

    let IndexOptions =
        { Fields = []
          Type = PersistentIndex
          Unique = true }

    let QueryOptions =
        { BatchSize = 32
          BindVars = Map.empty<string, _>
          Query = None }

    let SearchLinkOptions = { IncludeAllFields = false }

    let SearchOptions =
        { Links = Map.empty<string, _>
          Name = None
          Type = SearchView }

    (* Status *)

    let (|OK|Error|) (status, response) =
        if status = 200 then OK response
        elif status = 201 then OK response
        else Error

    [<Literal>]
    let BadRequest = 400

    [<Literal>]
    let NotFound = 404

    [<Literal>]
    let MethodNotAllowed = 405

    [<Literal>]
    let Conflict = 409

    [<Literal>]
    let LengthRequired = 411

    [<Literal>]
    let PayloadTooLarge = 413

    [<Literal>]
    let URITooLong = 414

    [<Literal>]
    let RequestHeaderFieldsTooLarge = 431

    [<Literal>]
    let HTTPVersionNotSupported = 505
