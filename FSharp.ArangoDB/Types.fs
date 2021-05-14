namespace FSharp.ArangoDB

module Types =
    open FSharp.Json
    open System.Net.Http

    type Collection =
        { id: string
          name: string
          status: int
          [<JsonField "type">]
          type': int
          isSystem: bool }

    type CollectionKeyOptions =
        { [<JsonField "type">]
          type': string }

    type CollectionOptions =
        { name: string
          keyOptions: CollectionKeyOptions
          [<JsonField "type">]
          type': int }

    type IndexOptions =
        { [<JsonField "type">]
          type': string
          unique: bool
          fields: string list }

    type Query<'T> =
        { query: string
          bindVars: Map<string, 'T> }

    type QueryResult<'T> = { id: string option; result: 'T }

    type ViewLinks = { includeAllFields: bool }

    type ViewOptions =
        { links: Map<string, ViewLinks>
          name: string
          [<JsonField "type">]
          type': string }

    type Client =
        { authorization: string
          client: HttpClient
          database: string
          debug: bool (* TODO: Debug mode *)
          host: string
          target: string }
